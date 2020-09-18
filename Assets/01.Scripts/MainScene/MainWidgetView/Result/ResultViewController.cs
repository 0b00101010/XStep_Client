using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class ResultViewController : MonoBehaviour {
    [Header("Objects")]

    [SerializeField]
    private Image background;
    [SerializeField]
    private CanvasGroup canvasGroup;

    [Space(10)]
    [SerializeField]
    private Image eyecatchImage;

    [SerializeField]
    private Image[] stepImages;

    [Space(10)]
    [SerializeField]
    private TextMeshProUGUI songName;

    [SerializeField]
    private TextMeshProUGUI composerName;

    [Space(10)]
    [SerializeField]
    private Image difficultyImage;

    [SerializeField]
    private TextMeshProUGUI difficultyText;
    
    [Space(10)]
    [SerializeField]
    private TextMeshProUGUI[] judgeCountTexts;
    
    [Space(10)]
    [SerializeField]
    private TextMeshProUGUI rankText;

    [SerializeField]
    private TextMeshProUGUI accuracyText;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    
    [Header("Resources")]
    [SerializeField]
    private Sprite[] difficultySprites;
    
    [Button("Setting")]
    public void Setting() {
        var songInformation = GameManager.instance.selectSongItem;
        var songData = songInformation.MapFile;
        var result = GameManager.instance.gameResult;
        
        eyecatchImage.sprite = songInformation.EyeCatch;
        
        songName.text = songInformation.SongName;
        composerName.text = songInformation.ComposerName;
        
        for (int i = 0; i < songInformation.StepTags.Length; i++) {
            stepImages[i].gameObject.SetActive(true);
        }
        
        difficultyImage.sprite = difficultySprites[songData.currentSelectDifficulty];
        difficultyText.text = $"Lv. {songInformation.Difficultys[songData.currentSelectDifficulty].ToString("D2")}";

        for (int i = 0; i < result.JudgeCounts.Length; i++) {
            judgeCountTexts[i].text = result.JudgeCounts[i].ToString("D4");
        }
        
        rankText.text = GameManager.instance.gameResult.Rank;
        accuracyText.text = $"{GameManager.instance.gameResult.Accuracy.ToString("F2")}%";
        scoreText.text = GameManager.instance.gameResult.Score.ToString("D11");
    }

    public void Open() {    
        gameObject.SetActive(true);
        Setting();
        GameManager.instance.widgetViewer.WidgetsOpen(background, canvasGroup);
        GameManager.instance.SomeUIInteraction = true;
    }

    public void Close() {
        GameManager.instance.widgetViewer.WidgetsClose(background, () => {
            GameManager.instance.gameResult = null;
            gameObject.SetActive(false);
            MainSceneManager.instance.uiController.FreeStyleViewOpen();
            GameManager.instance.SomeUIInteraction = false;
        }, canvasGroup);
    }
}
