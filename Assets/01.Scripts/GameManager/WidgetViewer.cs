using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WidgetViewer : MonoBehaviour
{   
    public void WidgetsOpen(Image parentWidget, CanvasGroup canvasGroup){
        WidgetsOpenCoroutine(parentWidget, canvasGroup).Start(this);
    } 

    public void WidgetsClose(Image parentWidget, Action closeAction, CanvasGroup canvasGroup){
        WidgetsCloseCoroutine(parentWidget, closeAction, canvasGroup).Start(this);
    }

    private IEnumerator WidgetsOpenCoroutine(Image parentWidget, CanvasGroup canvasGroup){
        // Background 커지는 시간
        var sizeUpTween = parentWidget.gameObject.transform.DOScaleY(1.0f, 1.0f);
        yield return sizeUpTween.WaitForCompletion();

        // Fade in 시간
        canvasGroup.DOFade(1.0f, 1.0f);
    }

    private IEnumerator WidgetsCloseCoroutine(Image parentWidget, Action closeAction, CanvasGroup canvasGroup){
        // Fade out 시간
        var fadeTween = canvasGroup.DOFade(0.0f, 1.0f);
        yield return fadeTween.WaitForCompletion();
        
        // Background 작아지는 시간
        var sizeDownTwee  = parentWidget.gameObject.transform.DOScaleY(0.0f, 1.0f);
        yield return sizeDownTwee.WaitForCompletion();
            
        closeAction();
    }

}
