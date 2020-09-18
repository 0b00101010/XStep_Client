using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultSelectButton : MainUIObject {
    [SerializeField]
    private int difficulty;

    [SerializeField]
    private Text highScoreText;
    
    private Image backgroundImage;
    public Image BackgroundImage => backgroundImage;
    
    private static DifficultSelectButton _selectDifficultyButton;
    public static DifficultSelectButton SelectDifficultyButton {
        get => _selectDifficultyButton;
        set => _selectDifficultyButton = value;
    }

    private void Awake() {
        backgroundImage = gameObject.transform.GetChild(1).GetComponent<Image>();
        DifficultSelectButton.SelectDifficultyButton = null;

    }
    
    public override void Execute() {
        GameManager.instance.songData.currentSelectDifficulty = difficulty;
        highScoreText.text = GameManager.instance.selectSongItem.HighScore[difficulty].ToString("D11");
        
        DifficultSelectButton.SelectDifficultyButton?.BackgroundImage.gameObject.SetActive(false);
        DifficultSelectButton.SelectDifficultyButton = this;
        
        backgroundImage.gameObject.SetActive(true);
    }
    
}
