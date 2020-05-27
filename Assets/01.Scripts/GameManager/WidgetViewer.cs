using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WidgetViewer : MonoBehaviour
{   
    private Tween myTween;

    public void WidgetsOpen(Image parentWidget, params object[] childWidgets){
        StartCoroutine(WidgetsOpenCoroutine(parentWidget, childWidgets));
    } 

    public void WidgetsClose(Image parentWidget, Action closeAction, params object[] childWidgets){
        StartCoroutine(WidgetsCloseCoroutine(parentWidget, closeAction, childWidgets));
    }

    private IEnumerator WidgetsOpenCoroutine(Image parentWidget, params object[] childWidgets){
        yield return StartCoroutine(ScaleUpParentWidget(parentWidget));
        FadeInChildWidgets(childWidgets);
    }

    private IEnumerator WidgetsCloseCoroutine(Image parentWidget, Action closeAction, params object[] childWidgets){
        yield return StartCoroutine(FadeOutChildWidgets(childWidgets));
        yield return StartCoroutine(ScaleDownParenWidget(parentWidget));
        closeAction();
    }

    private IEnumerator ScaleUpParentWidget(Image parentWidget){
        myTween = parentWidget.gameObject.transform.DOScaleY(1, 1.0f);
        yield return new WaitForTween(myTween);
    }

    private IEnumerator ScaleDownParenWidget(Image parentWidget){
        myTween = parentWidget.gameObject.transform.DOScaleY(0, 1.0f);
        yield return new WaitForTween(myTween);
    }

    private void FadeInChildWidgets(params object[] childWidgets){
        for(int i = 0; i < childWidgets.Length; i++){
            if(childWidgets[i] is Image image){
                image.DOFade(1,0.5f);
            }
            else if(childWidgets[i] is Text text){
                text.DOFade(1,0.5f);
            }
        }
    }


    // FIXME : 영 코드가 안 이쁨
    private IEnumerator FadeOutChildWidgets(params object[] childWidgets){
        Image image;
        Text text;

        for(int i = 0; i < childWidgets.Length - 1; i++){
            if(image = childWidgets[i] as Image){
                image.DOFade(0,0.5f);
            }
            else if(text = childWidgets[i] as Text){
                text.DOFade(0,0.5f);
            }
        }

        if(image = childWidgets[childWidgets.Length - 1] as Image){
            myTween = image.DOFade(0,0.5f);
            yield return new WaitForTween(myTween);
        }
        else if(text = childWidgets[childWidgets.Length - 1] as Text){
            myTween = text.DOFade(0,0.5f);
            yield return new WaitForTween(myTween);
        }

    }
}
