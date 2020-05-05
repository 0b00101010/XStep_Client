using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    
    [SerializeField]
    private VOIDEvent backgroundColorChangeEvent;
    
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
