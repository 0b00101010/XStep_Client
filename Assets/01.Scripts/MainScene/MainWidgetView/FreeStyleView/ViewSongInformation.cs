﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ViewSongInformation : MonoBehaviour
{

    [Header("View Panner")]
    
    [SerializeField]
    private GameObject songInformationView;

    [Header("View Panner Items")]
    
    [SerializeField]
    private Image backgroundImage;
    
    [SerializeField]
    private Image[] childWidgetsImage;

    [SerializeField]
    private Text[] childWidgetsText;

    [Header("Detail Items")]
    
    [SerializeField]
    private Image eyeCatchImage;

    [SerializeField]
    private Text songName;

    [SerializeField]
    private Text composerName;

    [SerializeField]
    private GameObject[] stepTags;

    [SerializeField]
    private Text[] difficultyTexts;

    [SerializeField]
    private Text highScoreText;

    private Action<bool> pannerActiveCheckFunction;

    [SerializeField]
    private CanvasGroup songInformationCanvas;

    private bool isClosing = false;
    public void SettingPannerActiveCheckFunction(Action<bool> pannerActiveCheckFunction){
        this.pannerActiveCheckFunction = pannerActiveCheckFunction;
    }

    public void SettingInformation(SongItemInformation information) {
        
        this.eyeCatchImage.sprite = information.EyeCatch;
        this.songName.text = information.SongName;
        this.composerName.text = information.ComposerName;

        for(int i = 0; i < information.StepTags.Length; i++){
            this.stepTags[information.StepTags[i]].SetActive(true);
        }

        for(int i = 0; i < information.Difficultys.Length; i++){
            this.difficultyTexts[i].text = information.Difficultys[i].ToString();
        }

        for (int i = 0; i < information.MapFile.maps.Length; i++) {
            this.difficultyTexts[i].transform.parent.gameObject.SetActive( information.MapFile.maps[i].map == null ? false : true );
        }

        highScoreText.text = 0.ToString("D11");
    }
    
    public void OpenSongInformation(){
        songInformationView.SetActive(true);
        GameManager.Instance.widgetViewer.WidgetsOpen(backgroundImage, songInformationCanvas);
        pannerActiveCheckFunction(true);
    }

    public void CloseSongInformation(){
        if(!isClosing){
            isClosing = true;
            GameManager.Instance.widgetViewer.WidgetsClose(backgroundImage, ResetPanner, songInformationCanvas);
        }
    }

    private void ResetPanner(){
        songInformationView.SetActive(false);
        for(int i = 0; i < stepTags.Length; i++){
            stepTags[i].SetActive(false);
        }
        
        isClosing = false;
        pannerActiveCheckFunction(false);
    }

}
