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

        Vector2 convertViewpointToWorldPoint(Vector2 viewPoint) {
            return Camera.main.ViewportToWorldPoint(viewPoint);
        }

        var screenWidth 
            = Vector2.Distance(convertViewpointToWorldPoint(Vector2.zero),
                convertViewpointToWorldPoint(Vector2.right));
        
        lineRenderer.startWidth = screenWidth;

        var screenHeight 
            = Vector2.Distance(convertViewpointToWorldPoint(Vector2.up),
                convertViewpointToWorldPoint(Vector2.zero)) * 0.5f;
        
        lineRenderer.SetPosition(0, new Vector2(0, screenHeight));
        lineRenderer.SetPosition(1, new Vector2(0, -screenHeight));
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
