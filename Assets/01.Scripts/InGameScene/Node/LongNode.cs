using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LongNode : Node
{  
    private Vector2 startPosition;
    private Vector2 targetPosition;

    private Tween headTween;
    private Tween tailTween;

    private LineRenderer lineRenderer;

    private Vector2 headVector;
    private Vector2 tailVector;

    private new void Awake(){
        base.Awake();

        startPosition.x = 0;
        startPosition.y = -0.645f;

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, startPosition);

        gameObject.transform.position = startPosition;
    }

    public override void Execute(Vector2 targetPosition){
        this.targetPosition = targetPosition;
    }

    private void HeadStart(){
        headTween?.Kill();

        headTween = DOTween.To(() => headVector, x => headVector = x, targetPosition, arriveTime);

        headTween.OnUpdate(() => {
            lineRenderer.SetPosition(0, headVector);
        });

        headTween.OnComplete(() => {
            
        });
    }

    private void TailStart(){

    }

    public override void Interaction(){

    }

    private void InteractionStart(){
}

    public void InteractionEnd(){

    }

    public override void FailedInteraction(){

    }
}
