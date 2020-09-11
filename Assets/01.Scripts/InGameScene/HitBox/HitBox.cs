using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HitBox : MonoBehaviour
{
    [SerializeField]
    private int index;
    public int Index => index;

    private Image fadeImage;
    private Tween fadeTween;

    private IEnumerator executeCoroutine;
    
    private void Awake() {
        fadeImage = gameObject.GetComponent<Image>();
    }
    
    public void Execute() {
        executeCoroutine?.Stop(this);
        executeCoroutine = ExecuteCoroutine().Start(this);
    }

    private IEnumerator ExecuteCoroutine() {
        fadeTween?.Kill();
        fadeImage.SetAlpha(0.0f);
        
        fadeTween = fadeImage.DOFade(0.25f, 0.15f);
        yield return fadeTween.WaitForCompletion();
        fadeTween = fadeImage.DOFade(0.0f, 0.15f);
    }
}
