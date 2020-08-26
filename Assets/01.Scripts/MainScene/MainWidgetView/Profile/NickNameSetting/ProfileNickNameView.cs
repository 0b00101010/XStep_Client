using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
public class ProfileNickNameView : ProfileSettingView
{
    [Header("Objects")]
    [SerializeField]
    private InputField userNameField;

    [SerializeField]
    private Text characterCountText;

    [SerializeField]
    private TextMeshProUGUI canChangeText;

    [SerializeField]
    private Image canChangeImage;

    [Header("Resources")]
    [SerializeField]
    private Sprite[] canChangeUserNameResource;

    private int characterCount = 10;
    private bool canChange;

    [Button("Reset Change chance")]
    public void RestChangeChance(){
        PlayerPrefs.SetString("UserNameChange", "true");
        canChange = true;
        SettingOption(canChange);
    }
    
    public override void Execute(){
        gameObject.SetActive(true);
        bool.TryParse(PlayerPrefs.GetString("UserNameChange", "true"), out canChange);
        SettingOption(canChange);
    }    

    private void SettingOption(bool value){
        canChangeImage.sprite = 
        value ? canChangeUserNameResource[0] : canChangeUserNameResource[1];
        
        canChangeText.text = 
        value ? "닉네임 변경 가능" : "닉네임 변경 불가능";   
    }

    public void ChangeNickName(){
        if(!canChange){
            return;
        }

        canChange = false;

        PlayerPrefs.SetString("UserNameChange", "false");
        SettingOption(false);

        GameManager.instance.PlayerSetting.userName = userNameField.text;
        MainSceneManager.instance.uiController.UserNameSetting(userNameField.text);
    }

    public void ChangeValue(){
        characterCountText.text = $"{userNameField.text.Length} / {characterCount}";
    }

    public override void Exit(){
        gameObject.SetActive(false);
    }

    
}
