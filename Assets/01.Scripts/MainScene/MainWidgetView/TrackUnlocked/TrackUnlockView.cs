using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TrackUnlockView :MonoBehaviour {
    private CanvasGroup canvasGroup;

    [SerializeField] 
    private Image backgroundImage;
    
    [SerializeField] 
    private Image eyeCatch;

    [SerializeField]
    private TextMeshProUGUI songNameText;

    [SerializeField]
    private TextMeshProUGUI composerNameText;

    [SerializeField]
    private Text[] difficultyTexts;
    
    private void Awake() {
        canvasGroup = gameObject.GetComponentInChildren<CanvasGroup>();
    }
    
    public void Execute(SongItemInformation item) {
        gameObject.SetActive(true);
        
        eyeCatch.sprite = item.EyeCatch;
        for (int i = 0; i < item.Difficultys.Length; i++) {
            difficultyTexts[i].text = item.Difficultys[i].ToString("D2");
        }

        songNameText.text = item.SongName;
        composerNameText.text = item.ComposerName;
        
        GameManager.Instance.widgetViewer.WidgetsOpen(backgroundImage, canvasGroup);
        GameManager.Instance.SomeUIInteraction = true;
    }

    public void Exit() {
        GameManager.Instance.widgetViewer.WidgetsClose(backgroundImage, () => {
            GameManager.Instance.SomeUIInteraction = false;
            gameObject.SetActive(false);
        }, canvasGroup);
    }
}
