using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBackground : MonoBehaviour
{
    private Color topColor;
    private Color bottomColor;


    [SerializeField]
    private LineRenderer lineRenderer;

    private void Awake(){
        topColor = lineRenderer.startColor;
        bottomColor = lineRenderer.endColor;
    }

    
    public void ChangeBackgroundColor(){
        StartCoroutine(ChnageBackgroundColorCoroutine());
    }

    private IEnumerator ChnageBackgroundColorCoroutine(){
        
        Color beforeTopColor = topColor;
        Color beforeBottomColor = bottomColor;

        topColor = GetRandomColor();
        bottomColor = GetRandomColor();

        for(int i = 0; i < 60; i++){

            lineRenderer.startColor = Color.Lerp(beforeTopColor, topColor, i / 60.0f);
            lineRenderer.endColor = Color.Lerp(beforeBottomColor, bottomColor, i / 60.0f);
            
            yield return YieldInstructionCache.WaitFrame;
        }
    }
    
    private Color GetRandomColor(){
        return Color.HSVToRGB(Random.value, 1, 1);
    }

}
