using System;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour {
    private Dictionary<string, double> settingDictionary = new Dictionary<string, double>();
    private List<SongProcessAction> songProcessActions = new List<SongProcessAction>();
    
    private AudioSource audioSource;
    private double bpm;

    private double currentSample;
    private double nextStep;

    private double offset;

    private double oneBeatTime;
    private MapFile songData;

    private int split;

    private void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
        songData = GameManager.instance.songData;

        audioSource.clip = songData.clip;

        ReadFile();

        bpm = settingDictionary["BPM"];
        split = (int) settingDictionary["Split"];
        offset = settingDictionary["Delay"] / 1000;

        var offsetForSample = offset * audioSource.clip.frequency;

        oneBeatTime = 60.0 / bpm / split;
        oneBeatTime *= audioSource.clip.frequency;

        nextStep = offsetForSample;

        audioSource.Play();
    }

    private void Update() {
        if (audioSource.timeSamples >= nextStep) {
            NodeGenerate();
        }
    }

    private void NodeGenerate() {
        var beforePosition = 0;
        if (songProcessActions[0].positionValue != -1) {
            do {
                songProcessActions[0].currentProgressAction();
                beforePosition = songProcessActions[0].positionValue;
                songProcessActions.RemoveAt(0);
            } while (beforePosition != 0 && songProcessActions[0].positionValue.Equals(beforePosition));

            nextStep += oneBeatTime;
        }
        else {
            do {
                songProcessActions[0].currentProgressAction();
                songProcessActions.RemoveAt(0);
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

                    var currentItemPosition = generatePosition;
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
            ColorUtility.TryParseHtmlString(topColor, out hexToColorBottom);

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
            InGameManager.instance.nodeCreator.NormalNodeGenerate(position);
        };
    }

    private Action LongNodeGenerateAction(int position) {
        return () => {
            InGameManager.instance.nodeCreator.LongNodeGenerate(position);
        };
    }

    private Action LongNodeEndAction(int position) {
        return () => {
            InGameManager.instance.nodeCreator.LongNodeStop(position);
        };
    }

    private Action SlideNodeGenerateAction(int position) {
        return () => {
            InGameManager.instance.nodeCreator.SlideNodeGenerate(position);
        };
    }
}