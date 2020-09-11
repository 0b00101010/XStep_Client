using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CenterEffectorController : MonoBehaviour
{

    [SerializeField]
    private Image[] effectorImages;

    [SerializeField]
    private float changeScaleValue;

    [SerializeField]
    private int repeatFrame;
    
    private int realRepeatFrame;
    private Vector3 changeScale;

    private bool isEffect;

    private void Awake(){
        changeScale.x = changeScaleValue;
        changeScale.y = changeScaleValue;
        changeScale.z = changeScaleValue;

        realRepeatFrame = repeatFrame / 2;

        changeScale /= realRepeatFrame;

    }

    public void EffectOneShot(){
        if(isEffect){
            return;
        }
        StartCoroutine(EffectCoroutine());
        isEffect = true;
    }

    private IEnumerator EffectCoroutine(){
        for(int i = 0; i < realRepeatFrame; i++){
            effectorImages[1].gameObject.transform.localScale += changeScale;
            yield return YieldInstructionCache.WaitFrame;
        }

        for(int i = 0; i < realRepeatFrame; i++){
            effectorImages[1].gameObject.transform.localScale -= changeScale;
            yield return YieldInstructionCache.WaitFrame;
        }


        for(int i = 0; i < realRepeatFrame; i++){
            for(int j = 0; j < effectorImages.Length; j++){
                effectorImages[j].gameObject.transform.localScale += changeScale;
            }
            yield return YieldInstructionCache.WaitFrame;
        }

        for(int i = 0; i < realRepeatFrame; i++){
            for(int j = 0; j < effectorImages.Length; j++){
                effectorImages[j].gameObject.transform.localScale -= changeScale;
            }
            yield return YieldInstructionCache.WaitFrame;
        }

        isEffect = false;
    }
}
