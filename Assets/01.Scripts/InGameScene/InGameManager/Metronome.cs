using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    private AudioSource audioSource;
    private MapFile songData;
    
    private Dictionary<string, double> settingDictionary = new Dictionary<string, double>();
    private List<SongProcessAction> songProcessActions = new List<SongProcessAction>();
    
    private int split;
    private double bpm;
    
    private double offset;

    private double currentSample;

    private double oneBeatTime; 
    private double nextStep;

    private void Start(){
        audioSource = gameObject.GetComponent<AudioSource>();
        songData = GameManager.instance.songData;

        audioSource.clip = songData.clip;

        ReadFile();

        bpm = settingDictionary["BPM"];
        split = (int)settingDictionary["Split"];
        offset = settingDictionary["Delay"] / 1000;

        var offsetForSample = offset * audioSource.clip.frequency;
        
        oneBeatTime = (60.0 / bpm) / split;
        oneBeatTime *= audioSource.clip.frequency;

        nextStep = offsetForSample;

        audioSource.Play();
    }

    private void Update(){
        if(audioSource.timeSamples >= nextStep){
            NodeGenerate().Start(this);
            nextStep += oneBeatTime;
        }
    }

    private IEnumerator NodeGenerate(){
        int beforePosition = 0;
        
        do{
            songProcessActions[0].currentProgressAction();
            beforePosition = songProcessActions[0].positionValue;
            songProcessActions.RemoveAt(0);
        }while(beforePosition != 0 && songProcessActions[0].positionValue.Equals(beforePosition));

        yield return YieldInstructionCache.WaitFrame;
    }

    private void ReadFile(){
        var mapTexts = songData.mapFile.text.Split('\n');
        var position = 0;

        for(int i = 0; i < mapTexts.Length; i++){
            if(mapTexts[i].StartsWith(":")){
                var settingKey = mapTexts[i].Split(':')[1].Split('=')[0];
                var settingValue = double.Parse(mapTexts[i].Split('=')[1]);
                
                MapSystemSetting(settingKey, settingValue);
            }else{
                // Normal Node & Long Node
                var frontText = mapTexts[i].Substring(0, 4);
                
                // Slide Node
                var backText = mapTexts[i].Substring(4,4); 

                var normalNodePositions = frontText.IndexOfMany('X');
                var longStartNodePositions = frontText.IndexOfMany('M');
                var longEndNodePositions = frontText.IndexOfMany('W');

                int nodeGenerateCount = normalNodePositions.Length +
                longStartNodePositions.Length + 
                longEndNodePositions.Length;


                if(nodeGenerateCount > 0){
                    // Normal Node & Long Node
                    void frontNodeGenerateFunction(int[] nodePositions, Func<int, Action> nodeGenerateAction){
                        for(int j = 0; j < nodePositions.Length; j++){
                            var newProcessAction = new SongProcessAction();

                            newProcessAction.currentProgressAction 
                            = nodeGenerateAction(nodePositions[j]);

                            newProcessAction.positionValue = position;

                            songProcessActions.Add(newProcessAction);
                        }
                    }  

                    frontNodeGenerateFunction(normalNodePositions, NormalNodeGenerateAction);
                    frontNodeGenerateFunction(longStartNodePositions, LongNodeGenerateAction);
                    frontNodeGenerateFunction(longEndNodePositions, LongNodeEndAction);
                } else {
                    var voidProcessAction = new SongProcessAction();
                    
                    voidProcessAction.currentProgressAction = () => {};
                    voidProcessAction.positionValue = 0;
                    
                    songProcessActions.Add(voidProcessAction);
                }

                position++;
            }
        }

    }

    private void MapSystemSetting(string key, double value){
        if(!settingDictionary.ContainsKey(key)){
            settingDictionary.Add(key, value);
        } else {
            var newProcessAction = new SongProcessAction();
            
            newProcessAction.currentProgressAction = MakeSystemAction(key, value);
            newProcessAction.positionValue = -1;

            songProcessActions.Add(newProcessAction);
        }
    }

    private Action MakeSystemAction(string key, double value){
        return () => { settingDictionary[key] = value; };
    }

    private Action NormalNodeGenerateAction(int position){
        return () => {
            InGameManager.instance.nodeCreator.NormalNodeGenerate(position);
        };
    }

    private Action LongNodeGenerateAction(int position){
        return () => {
            InGameManager.instance.nodeCreator.LongNodeGenerate(position);
        };
    }

    private Action LongNodeEndAction(int position){
        return () => {
            InGameManager.instance.nodeCreator.LongNodeStop(position);
        };
    }
}
