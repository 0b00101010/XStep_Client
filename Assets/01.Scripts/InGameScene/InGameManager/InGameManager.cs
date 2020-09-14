using System.Collections;
using System.Collections.Generic;
using EventTools.Event;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    
    public static InGameManager instance;
    
    [HideInInspector]
    public ScoreManager scoreManager;

    [HideInInspector]
    public NodeCreator nodeCreator;

    [HideInInspector]
    public CenterEffectorController centerEffectorController;

    [HideInInspector]
    public NodeInteractionController nodeInteractionController;

    [HideInInspector]
    public Metronome metronome;
    
    
    [Header("Events")]
    [SerializeField]
    private UniEvent<Color,Color,float> backgroundColorChangeEvent;
    
    
    private void Awake(){
        if(instance is null){
            instance = this;
        }

        scoreManager = gameObject.GetComponent<ScoreManager>();
        nodeCreator = gameObject.GetComponent<NodeCreator>();
        metronome = gameObject.GetComponent<Metronome>();
        centerEffectorController = gameObject.GetComponent<CenterEffectorController>();
        nodeInteractionController = gameObject.GetComponent<NodeInteractionController>();
    }

    private void Start(){
        GameManager.instance.touchManager.touchType = TouchType.InGame;
        GameManager.instance.gameResult = new GameResult();
    }

    public void ChangeBackgroundColor(Color topColor, Color bottomColor, float duration) {
        backgroundColorChangeEvent.Invoke(topColor,bottomColor,duration);
    }    

    public void GameEnd() {
        instance = null;

        GameManager.instance.gameResult.Score = scoreManager.TotalScore;
        GameManager.instance.PlayerSetting.totalScore += (ulong)scoreManager.TotalScore;
        // TODO : 정확도 계산하는 기능 추가
        SceneManager.LoadScene("00.MainScene");
    }


}
