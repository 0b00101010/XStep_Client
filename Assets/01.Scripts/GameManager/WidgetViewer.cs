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
        var sizeUpTween = parentWidget.gameObject.transform.DOScaleY(1.0f, 1.0f);
        yield return sizeUpTween.WaitForCompletion();

        canvasGroup.DOFade(1.0f, 1.0f);
    }

    private IEnumerator WidgetsCloseCoroutine(Image parentWidget, Action closeAction, CanvasGroup canvasGroup){
        var fadeTween = canvasGroup.DOFade(0.0f, 1.0f);
        yield return fadeTween.WaitForCompletion();
        
        var sizeDownTwee  = parentWidget.gameObject.transform.DOScaleY(0.0f, 1.0f);
        yield return sizeDownTwee.WaitForCompletion();
            
        closeAction();
    }

}
