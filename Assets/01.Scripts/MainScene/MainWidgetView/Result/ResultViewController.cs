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
        var songInformation = GameManager.Instance.selectSongItem;
        var songData = songInformation.MapFile;
        var result = GameManager.Instance.gameResult;
        
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
        
        rankText.text = GameManager.Instance.gameResult.Rank;
        accuracyText.text = $"{GameManager.Instance.gameResult.Accuracy.ToString("F2")}%";
        scoreText.text = GameManager.Instance.gameResult.Score.ToString("D11");
    }

    public void Open() {    
        gameObject.SetActive(true);
        Setting();
        GameManager.Instance.widgetViewer.WidgetsOpen(background, canvasGroup);
        GameManager.Instance.SomeUIInteraction = true;
    }

    public void Close() {
        GameManager.Instance.widgetViewer.WidgetsClose(background, () => {
            GameManager.Instance.gameResult = null;
            gameObject.SetActive(false);
            MainSceneManager.Instance.uiController.FreeStyleViewOpen();
            GameManager.Instance.SomeUIInteraction = false;
        }, canvasGroup);
    }
}
