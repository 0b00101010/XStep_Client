using System.Collections;
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

    private Action<bool> pannerActiveCheckFunction;

    private List<object> childWidgets = new List<object>();

    private void Awake(){
        childWidgets.AddRange(childWidgetsImage);
        childWidgets.AddRange(childWidgetsText);
    }

    public void SettingPannerActiveCheckFunction(Action<bool> pannerActiveCheckFunction){
        this.pannerActiveCheckFunction = pannerActiveCheckFunction;
    }

    public void SettingInformations(SongItemInformation information){
        this.eyeCatchImage.sprite = information.EyeCatch;
        this.songName.text = information.SongName;
        this.composerName.text = information.ComposerName;

        for(int i = 0; i < information.Difficultys.Length; i++){
            this.stepTags[information.Difficultys[i]].SetActive(true);
        }
    }
    
    public void OpenSongInformation(){
        songInformationView.SetActive(true);
        GameManager.instance.widgetViewer.WidgetsOpen(backgroundImage, childWidgets.ToArray());
        pannerActiveCheckFunction(true);
    }

    public void CloseSongInformation(){
        GameManager.instance.widgetViewer.WidgetsClose(backgroundImage, ResetPanner, childWidgets.ToArray());
    }

    private void ResetPanner(){
        songInformationView.SetActive(false);
        for(int i = 0; i < stepTags.Length; i++){
            stepTags[i].SetActive(false);
        }
        
        pannerActiveCheckFunction(false);
    }

}
