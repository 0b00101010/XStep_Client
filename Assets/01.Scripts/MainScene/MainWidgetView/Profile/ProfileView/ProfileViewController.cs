using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileViewController : MonoBehaviour
{
    [Header("Viewer")]
    [SerializeField]
    private GameObject viewerObject;

    [SerializeField]
    private Image background;

    [SerializeField]
    private CanvasGroup canvasGroup;

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

    private bool isOpen;
    private bool isClosing;

    private void Start(){
        var setting = GameManager.instance.PlayerSetting;
    
        userProfileImage.sprite = setting.profileSprite ?? userProfileImage.sprite;
        userName.text = setting.userName;
        userTitle.text = setting.title.ToString();
    
        levelText.text = $"Lv. {setting.currentLevel.ToString()}";
        expBar.fillAmount = ((float)setting.currentExp / (float)setting.levelUpExp);
        expText.text = $"{setting.currentExp} / {setting.levelUpExp}";

        totalScore.text = setting.totalScore.ToString("D12");
        highClearScore.text = setting.highClearScore.ToString("D2");

        perfectPlay.text = setting.perfectPlay.ToString("D2");
        challengeClear.text = setting.challengeClear.ToString("D2");
        freeStyleClear.text = setting.freeStyleClera.ToString("D2");
    }

    [ContextMenu("Open")]
    public void OpenWidget(){
        if(!isOpen && !GameManager.instance.SomeUIInteraction){
            viewerObject.gameObject.SetActive(true);

            GameManager.instance.widgetViewer.WidgetsOpen(background, canvasGroup);
            GameManager.instance.SomeUIInteraction = true;
            isOpen = true;
        }
    }

    public void CloseWidget(){
        if(!isClosing){
            GameManager.instance.widgetViewer.WidgetsClose(background, () => {
                GameManager.instance.SomeUIInteraction = false;
                isClosing = false;
                isOpen = false;

                viewerObject.gameObject.SetActive(false);
            }, canvasGroup);

            isClosing = true;
        }
    }
    
}
