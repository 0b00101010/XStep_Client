using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBackground : MonoBehaviour
{
    private Color topColor;
    private Color bottomColor;


    [SerializeField]
    private LineRenderer lineRenderer;

    private Color GetRandomColor(){
        return Color.HSVToRGB(Random.value, 1, 1);
    }

    [ContextMenu("DD")]
    private void ChangeBackgroundColor(){
        topColor = GetRandomColor();
        bottomColor = GetRandomColor();

        lineRenderer.startColor = topColor;
        lineRenderer.endColor = bottomColor;
    }

    private void ChangeTopColor(){

    }

    private void ChangeBottomColor(){

    }
}
