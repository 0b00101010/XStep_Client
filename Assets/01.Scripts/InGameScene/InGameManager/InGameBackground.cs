using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class InGameBackground : MonoBehaviour
{
    private Color topColor;
    private Color bottomColor;

    private Tween changeTween;
    
    [SerializeField]
    private LineRenderer lineRenderer;
    
    private void Awake(){
        topColor = lineRenderer.startColor;
        bottomColor = lineRenderer.endColor;
    }

    
    public void ChangeBackgroundColor(Color topColor, Color bottomColor, float duration) {
        var beforeColor = new Color2(this.topColor, this.bottomColor);
        var afterColor = new Color2(topColor, bottomColor);

        this.topColor = topColor;
        this.bottomColor = bottomColor;
        
        changeTween?.Kill();
        changeTween = lineRenderer.DOColor(beforeColor, afterColor, duration);
    }
}
