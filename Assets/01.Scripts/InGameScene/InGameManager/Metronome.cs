using System;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour {
    private readonly Dictionary<string, double> settingDictionary = new Dictionary<string, double>();
    private readonly List<SongProcessAction> songProcessActions = new List<SongProcessAction>();
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
            if (mapTexts[i].Equals("") || mapTexts[i].Equals(" ")) continue;

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

            var normalNodePositions = frontText.IndexOfMany('X');
            var longStartNodePositions = frontText.IndexOfMany('M');
            var longEndNodePositions = frontText.IndexOfMany('W');

            var leftSlideNodePositions = backText.IndexOfMany('X');
            var rightSlideNodePositions = backText.IndexOfMany('M');

            var nodeGenerateCount = normalNodePositions.Length +
                                    longStartNodePositions.Length +
                                    longEndNodePositions.Length +
                                    leftSlideNodePositions.Length +
                                    rightSlideNodePositions.Length;

            if (nodeGenerateCount > 0) {
                // Normal Node & Long Node
                void frontNodeGenerateFunction(int[] nodePositions, Func<int, Action> nodeGenerateAction) {
                    for (var j = 0; j < nodePositions.Length; j++) {
                        var newProcessAction = new SongProcessAction();

                        newProcessAction.currentProgressAction
                            = nodeGenerateAction(nodePositions[j]);

                        songProcessActions.Add(newProcessAction);
                    }
                }

                void backNodeGenerateFunction(int index, int[] nodePositions) {
                    for (var j = 0; j < nodePositions.Length; j++) {
                        var newProcessAction = new SongProcessAction();

                        var currentItemPosition = nodePositions[j];
                        currentItemPosition = index.Equals(0) ? currentItemPosition : currentItemPosition + 1;

                        newProcessAction.currentProgressAction
                            = SlideNodeGenerateAction(currentItemPosition);

                        newProcessAction.positionValue = position;

                        songProcessActions.Add(newProcessAction);
                    }
                }

                frontNodeGenerateFunction(normalNodePositions, NormalNodeGenerateAction);
                frontNodeGenerateFunction(longStartNodePositions, LongNodeGenerateAction);
                frontNodeGenerateFunction(longEndNodePositions, LongNodeEndAction);

                backNodeGenerateFunction(0, leftSlideNodePositions);
                backNodeGenerateFunction(1, rightSlideNodePositions);
            }
            else {
                var voidProcessAction = new SongProcessAction();

                voidProcessAction.currentProgressAction = () => {
                };
                voidProcessAction.positionValue = 0;

                songProcessActions.Add(voidProcessAction);
            }

            position++;


            // if (mapTexts[i].Contains("@")) {
            //     var mapText = mapTexts[i].Split('@')[1];
            //     var settingValue = mapText.Split(',');
            //
            //     var topColorHex = settingValue[0];
            //     var bottomColorHex = settingValue[1];
            //     float duration = float.Parse(settingValue[2]);
            //     
            //     var newProcessAction = new SongProcessAction();
            //
            //     newProcessAction.positionValue = -1;
            //     newProcessAction.currentProgressAction = BackgroundControlAction(topColorHex, bottomColorHex, duration);
            //     
            //     songProcessActions.Add(newProcessAction);
            //
            //     position++;
            // }
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