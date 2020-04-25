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


    private IEnumerator WidgetsOpenCoroutine(Image parentWidget, params Image[] childWidgets){
        yield return StartCoroutine(ScaleUpParentWidget(parentWidget));
        FadeInChildWidgets(childWidgets);
    }


    private IEnumerator ScaleUpParentWidget(Image parentWidget){
        for(int i = 0; i < 60; i++){
            parentWidget.gameObject.transform.localScale += Vector3.one / 60;
            yield return YieldInstructionCache.WaitFrame;
        }
    }


    private bool FadeInChildWidgets(params Image[] childWidgets){
    private void FadeInChildWidgets(params Image[] childWidgets){
        for(int i = 0; i < childWidgets.Length; i++){
            GameManager.instance.fadeManager.ImageFadeIn(childWidgets[i], 0.5f);
        }
    }

    }
}
