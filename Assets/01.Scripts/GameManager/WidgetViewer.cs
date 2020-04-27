using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WidgetViewer : MonoBehaviour
{
    // FIXME : 배열 형식으로 만들어서 값 복사가 계속 일어남

    public void WidgetsOpen(Image parentWidget, params Image[] childWidgets){
        StartCoroutine(WidgetsOpenCoroutine(parentWidget, childWidgets));
    } 

    public void WidgetsClose(Image parentWidget, params Image[] childWidgets){
        StartCoroutine(WidgetsCloseCoroutine(parentWidget, childWidgets));
    }

    private IEnumerator WidgetsOpenCoroutine(Image parentWidget, params Image[] childWidgets){
        yield return StartCoroutine(ScaleUpParentWidget(parentWidget));
        FadeInChildWidgets(childWidgets);
    }

    private IEnumerator WidgetsCloseCoroutine(Image parentWidget, params Image[] childWidgets){
        yield return StartCoroutine(FadeOutChildWidgets(childWidgets));
        StartCoroutine(ScaleDownParenWidget(parentWidget));
    }

    private IEnumerator ScaleUpParentWidget(Image parentWidget){
        Vector3 upperVector = Vector3.up / 60;
        for(int i = 0; i < 60; i++){
            parentWidget.gameObject.transform.localScale += upperVector;
            yield return YieldInstructionCache.WaitFrame;
        }
    }

    private IEnumerator ScaleDownParenWidget(Image parentWidget){
        Vector3 lowerVector = Vector3.up / 60;
        for(int i = 0; i < 60; i++){
            parentWidget.gameObject.transform.localScale -= lowerVector;
            yield return YieldInstructionCache.WaitFrame;
        }
    }

    private void FadeInChildWidgets(params Image[] childWidgets){
        for(int i = 0; i < childWidgets.Length; i++){
            StartCoroutine(GameManager.instance.fadeManager.ImageFadeIn(childWidgets[i], 0.5f));
        }
    }


    // FIXME : 영 코드가 안 이쁨
    private IEnumerator FadeOutChildWidgets(params Image[] childWidgets){
        for(int i = 0; i < childWidgets.Length - 1; i++){
            StartCoroutine(GameManager.instance.fadeManager.ImageFadeOut(childWidgets[i], 0.5f));
        }
        yield return StartCoroutine(GameManager.instance.fadeManager.ImageFadeOut(childWidgets[childWidgets.Length - 1], 0.5f));
    }
}
