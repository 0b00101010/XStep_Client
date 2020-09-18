using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {
    [SerializeField]
    private GameObject[] staticUiObjects;
    
    [SerializeField]
    private Text informationBoxText;
    
    [SerializeField]
    private FreeStyleViewController freeStyleViewController;
    
    [SerializeField]
    private ResultViewController resultViewController;
    
    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI userNameText;

    [SerializeField]
    private Image userProfileImage;

    public void TitleViewClose() {
        for (int i = 0; i < staticUiObjects.Length; i++) {
            staticUiObjects[i].SetActive(true);
        }
    }
    
    public void FreeStyleViewOpen() {
        freeStyleViewController.gameObject.SetActive(true);
    }

    public void FreeStyleViewClose() {
        freeStyleViewController.gameObject.SetActive(false);
    }
    
    public void ResultOpen() {
        FreeStyleViewClose();
        resultViewController.Open();
    }
    
    public void SetInformationBoxText(string value){
        informationBoxText.text = value;
    }

    public void OpenSongInformationPanner(SongItemInformation information){
        freeStyleViewController.OpenPanner(information);
    }
    // FIXME : 진짜 존나 비효율적
    public void CloseSongInformationPaner(){
        freeStyleViewController.ClosePanner();
    }

    public void TitleSetting(string value){
        titleText.text = value;
    }

    public void UserNameSetting(string value){
        userNameText.text = value;
    }

    public void ProfileSetting(Sprite sprite){
        userProfileImage.sprite = sprite;
    }
}
