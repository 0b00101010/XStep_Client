using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    
    public static InGameManager instance;

    [SerializeField]
    private VOIDEvent backgroundColorChangeEvent;
    
    private void Awake(){
        if(instance is null){
            instance = this;
        }
    }

    private void Start(){
        StartCoroutine(BackgroundChangeCoroutine());
    }

    private IEnumerator BackgroundChangeCoroutine(){
        while(true){
            backgroundColorChangeEvent.Invoke();
            yield return YieldInstructionCache.WaitingSeconds(5.0f);
        }
    }

    

}
