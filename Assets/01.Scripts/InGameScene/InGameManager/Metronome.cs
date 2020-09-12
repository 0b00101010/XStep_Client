using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Metronome : MonoBehaviour {
    private Dictionary<string, double> settingDictionary = new Dictionary<string, double>();
    private List<SongProcessAction> songProcessActions = new List<SongProcessAction>();
    private List<SongProcessAction> backgroundActions = new List<SongProcessAction>();
    
    private AudioSource audioSource;
    private double bpm;

    private double currentSample;
    private double nextStep;

    private double offset;

    private double oneBeatTime;
    private MapFile songData;

    private int split;
    private int beatCount;

    private int effectSplit;
    
    private void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
        songData = GameManager.instance.songData;

        audioSource.clip = songData.clip;

        ReadFile();
        ReadBackgorundAction();
        
        bpm = settingDictionary["BPM"];
        split = (int) settingDictionary["Split"];
        offset = settingDictionary["Delay"] / 1000;
        effectSplit = (split * 2) - 1;

        var offsetForSample = offset * audioSource.clip.frequency;

        oneBeatTime = 60.0 / bpm / split;
        oneBeatTime *= audioSource.clip.frequency;

        nextStep = offsetForSample;

        audioSource.Play();
    }

    private void Update() {
        InGameManager.instance.scoreManager.SongProgressChange(nextStep / audioSource.clip.samples);

        if (audioSource.timeSamples >= nextStep) {
            NodeGenerate();
        }

        if (audioSource.time.Equals(audioSource.clip.length)) {
            InGameManager.instance.GameEnd();
        }
    }

    public double GetFrequency() {
        return audioSource.clip.frequency;
    }

    public double GetCurrentSample() {
        return audioSource.timeSamples;
    }

    public double GetCurrentTimeSample() {
        return audioSource.timeSamples;
    }

    private void NodeGenerate() {
        var beforePosition = 0;
        
        if (songProcessActions.Count <= 1 ) {
            return;
        }
        
        if (songProcessActions[0].positionValue != -1) {
            do {
                songProcessActions[0].currentProgressAction();

                if (backgroundActions.Count > 0) {
                    backgroundActions[0]?.currentProgressAction();
                    backgroundActions.RemoveAt(0);
                }
                
                beforePosition = songProcessActions[0].positionValue;
                songProcessActions.RemoveAt(0);
            } while (beforePosition != 0 && songProcessActions[0].positionValue.Equals(beforePosition));

            nextStep += oneBeatTime;
            beatCount++;
            
            if ((beatCount & effectSplit) == 0) {
                InGameManager.instance.centerEffectorController.EffectOneShot();
            }
        }
        else {
            do {
                songProcessActions[0].currentProgressAction();
                songProcessActions.RemoveAt(0);
                
                backgroundActions[0]?.currentProgressAction();
                backgroundActions.RemoveAt(0);
            } while (songProcessActions[0].positionValue == -1);
        }
    }
    
    private void ReadFile() {
        var mapTexts = songData.maps[songData.currentSelectDifficulty].map.text.Split('\n');
        var position = 0;

        for (var i = 0; i < mapTexts.Length; i++) {
            bool addedAction = false;

            if (mapTexts[i].Equals("") || mapTexts[i].Equals(" ")) {
                continue;
            }

            if (mapTexts[i].StartsWith(":")) {
                var settingKey = mapTexts[i].Split(':')[1].Split('=')[0];
                var settingValue = double.Parse(mapTexts[i].Split('=')[1]);

                MapSystemSetting(settingKey, settingValue);

                continue;
            }

            // Normal Node & Long Node
            var frontText = mapTexts[i].Substring(0, 4);

            // Slide Node
            var backText = mapTexts[i].Substring(4, 4);

            void addNodeGenerateAction(int[] generatePositions, Func<int, Action> nodeGenerateAction) {
                foreach (var generatePosition in generatePositions) {
                    var newProcessAction = new SongProcessAction();

                    newProcessAction.currentProgressAction = nodeGenerateAction(generatePosition);
                    newProcessAction.positionValue = position;

                    songProcessActions.Add(newProcessAction);

                    addedAction = true;
                }
            }

            addNodeGenerateAction(frontText.IndexOfMany('X'), NormalNodeGenerateAction);
            addNodeGenerateAction(frontText.IndexOfMany('M'), LongNodeGenerateAction);
            addNodeGenerateAction(frontText.IndexOfMany('W'), LongNodeEndAction);

            void addSlideNodeGenerateAction(int index, int[] generatePositions) {
                foreach (var generatePosition in generatePositions) {
                    var newProcessAction = new SongProcessAction();

                    var currentItemPosition = generatePosition * 2;
                    currentItemPosition = index == 0 ? currentItemPosition : currentItemPosition + 1;

                    newProcessAction.currentProgressAction = SlideNodeGenerateAction(currentItemPosition);
                    newProcessAction.positionValue = position;

                    songProcessActions.Add(newProcessAction);
                    
                    addedAction = true;
                }
            }
            
            addSlideNodeGenerateAction(0, backText.IndexOfMany('X'));
            addSlideNodeGenerateAction(1, backText.IndexOfMany('M'));
            
            if (addedAction == false) {
                var newProcessAction = new SongProcessAction();

                newProcessAction.currentProgressAction = () => { };
                newProcessAction.positionValue = 0;

                songProcessActions.Add(newProcessAction);
            }

            position++;
        }
    }

    private void ReadBackgorundAction() {
        var mapFile = songData.backgroundFile.text.Split('\n');
        int position = 0;
        
        for (int i = 0; i < mapFile.Length; i++) {
            if (mapFile[i].StartsWith(":")) {
                continue;
            }
            
            try {
                if (mapFile[i].StartsWith("@")) {
                    var newProcessAction = new SongProcessAction();

                    var data = mapFile[i].Split('@')[1].Split(',');

                    var topColor = data[0];
                    var bottomColor = data[1];
                    float duration = float.Parse(data[2]);

                    newProcessAction.currentProgressAction = BackgroundControlAction(topColor, bottomColor, duration);
                    newProcessAction.positionValue = position;

                    backgroundActions.Add(newProcessAction);
                }
                else if (mapFile[i].StartsWith(".")) {
                    var newProcessAction = new SongProcessAction();

                    newProcessAction.currentProgressAction = () => {
                    };
                    newProcessAction.positionValue = 0;

                    backgroundActions.Add(newProcessAction);
                }
            }
            catch (Exception e) {
                (e.ToString() + i).Log();
            }

            position++;
        }
    }

    private void MapSystemSetting(string key, double value) {
        if (!settingDictionary.ContainsKey(key)) {
            settingDictionary.Add(key, value);
        }
        else {
            var newProcessAction = new SongProcessAction();

            newProcessAction.currentProgressAction = MakeSystemAction(key, value);
            newProcessAction.positionValue = -1;

            songProcessActions.Add(newProcessAction);
        }
    }

    private Action BackgroundControlAction(string topColor, string bottomColor, float duration) {
        return () => {
            Color hexToColorTop;
            Color hexToColorBottom;

            ColorUtility.TryParseHtmlString(topColor, out hexToColorTop);
            ColorUtility.TryParseHtmlString(bottomColor, out hexToColorBottom);

            InGameManager.instance.ChangeBackgroundColor(hexToColorTop, hexToColorBottom, duration);
        };
    }

    private Action MakeSystemAction(string key, double value) {
        return () => {
            settingDictionary[key] = value;
        };
    }

    private Action NormalNodeGenerateAction(int position) {
        return () => {
            InGameManager.instance.nodeCreator.NormalNodeGenerate(position, audioSource.timeSamples);
        };
    }

    private Action LongNodeGenerateAction(int position) {
        return () => {
            InGameManager.instance.nodeCreator.LongNodeGenerate(position, audioSource.timeSamples);
        };
    }

    private Action LongNodeEndAction(int position) {
        return () => {
            InGameManager.instance.nodeCreator.LongNodeStop(position);
        };
    }

    private Action SlideNodeGenerateAction(int position) {
        return () => {
            InGameManager.instance.nodeCreator.SlideNodeGenerate(position, audioSource.timeSamples);
        }; 
    }
}