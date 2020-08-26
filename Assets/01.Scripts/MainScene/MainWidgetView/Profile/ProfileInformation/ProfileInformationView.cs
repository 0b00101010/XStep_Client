using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileInformationView : ProfileSettingView
{   
    [Header("Setting Object")]
    [SerializeField]
    private Image userProfileImage;
    
    [SerializeField]
    private TextMeshProUGUI userName;

    [SerializeField]
    private TextMeshProUGUI userTitle;

    [Space(10)]
    [SerializeField]
    private TextMeshProUGUI levelText;

    [SerializeField]
    private Image expBar;

    [SerializeField]
    private TextMeshProUGUI expText;
    
    [Space(10)]
    [SerializeField]
    private TextMeshProUGUI totalScore;

    [SerializeField]
    private TextMeshProUGUI highClearScore;

    [SerializeField]
    private TextMeshProUGUI perfectPlay;

    [SerializeField]
    private TextMeshProUGUI challengeClear;

    [SerializeField]
    private TextMeshProUGUI freeStyleClear;

    public override void Execute(){
        var setting = GameManager.instance.PlayerSetting;
        var title = setting.title.title;

        MainSceneManager.instance.uiController.ProfileSetting(setting.profileSprite);
        MainSceneManager.instance.uiController.UserNameSetting(setting.userName);
        MainSceneManager.instance.uiController.TitleSetting(title);

        userProfileImage.sprite = setting.profileSprite ?? userProfileImage.sprite;
        userName.text = setting.userName;
        userTitle.text = title;
    
        levelText.text = $"Lv. {setting.currentLevel.ToString()}";
        expBar.fillAmount = ((float)setting.currentExp / (float)setting.levelUpExp);
        expText.text = $"{setting.currentExp} / {setting.levelUpExp}";

        totalScore.text = setting.totalScore.ToString("D12");
        highClearScore.text = setting.highClearLevel.ToString("D2");

        perfectPlay.text = setting.perfectPlay.ToString("D2");
        challengeClear.text = setting.challengeClear.ToString("D2");
        freeStyleClear.text = setting.freeStyleClera.ToString("D2");
    }

    public override void Exit(){
        gameObject.SetActive(false);
    }
}
