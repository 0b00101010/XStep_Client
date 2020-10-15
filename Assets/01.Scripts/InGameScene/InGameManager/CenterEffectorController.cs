using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class CenterEffectorController : MonoBehaviour
{

    [SerializeField]
    private Image[] effectorImages;

    [SerializeField]
    private float duration;

    private IEnumerator effectorCoroutine;
    
    public void EffectOneShot() {
        effectorCoroutine?.Stop(this);
        effectorCoroutine = EffectCoroutine().Start(this);
    }

    private IEnumerator EffectCoroutine() {
        yield return effectorImages[1].gameObject.transform.DOScale(1.2f, duration).WaitForCompletion();
        yield return effectorImages[1].gameObject.transform.DOScale(1.0f, duration).WaitForCompletion();
        
        for(int i = 0; i < effectorImages.Length - 1; i++){
            effectorImages[i].gameObject.transform.DOScale(1.2f, duration);
        }
        
        yield return effectorImages[effectorImages.Length - 1].gameObject.transform.DOScale(1.2f, duration);
        
        for(int i = 0; i < effectorImages.Length - 1; i++){
            effectorImages[i].gameObject.transform.DOScale(1.0f, duration);
        }
        
        yield return effectorImages[effectorImages.Length - 1].gameObject.transform.DOScale(1.0f, duration);
    }
}
