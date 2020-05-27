using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    
    public static InGameManager instance;
    
    [HideInInspector]
    public ScoreManager scoreManager;

    [SerializeField]
    private VOIDEvent backgroundColorChangeEvent;
    
    private void Awake(){
        if(instance is null){
            instance = this;
        }

        scoreManager = gameObject.GetComponent<ScoreManager>();
    }

    private void Start(){
        StartCoroutine(BackgroundChangeCoroutine());
        GameManager.instance.touchManager.touchType = TouchType.InGame;
    }

    private IEnumerator BackgroundChangeCoroutine(){
        while(true){
            backgroundColorChangeEvent.Invoke();
            yield return YieldInstructionCache.WaitingSeconds(5.0f);
        }
    }



}
