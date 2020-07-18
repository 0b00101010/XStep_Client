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

    private bool isInteraction;
    private bool isFailedInteraction;

    private new void Awake(){
        base.Awake();

        startPosition = gameObject.transform.localPosition;

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, startPosition);

        gameObject.transform.position = startPosition;
    }

    public override void Execute(Vector2 targetPosition){
        this.targetPosition = targetPosition;

        headVector = startPosition;
        tailVector = startPosition;

        HeadStart();
    }

    private void HeadStart(){
        headTween?.Kill();

        headTween = DOTween.To(() => headVector, x => headVector = x, targetPosition, arriveTime / 2);

        headTween.OnUpdate(() => {
            lineRenderer.SetPosition(0, headVector);
        });

        headTween.OnComplete(() => {
            if(!isInteraction){
                FailedInteraction();
            }
        });
    }

    private void TailStart(){
        tailTween?.Kill();

        tailTween = DOTween.To(() => tailVector, x => tailVector = x, targetPosition, arriveTime / 2);

        tailTween.OnUpdate(() => {
            lineRenderer.SetPosition(1, tailVector);
        });

        tailTween.OnComplete(() => {
            ObjectReset();
        });
    }

    public override void Interaction(){
        if(isFailedInteraction){
            return;
        }

        if(isInteraction){

        } else {

        }
    }

    public override void FailedInteraction(){
        isFailedInteraction = true;
    }

    public override void ObjectReset(){
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, startPosition);

        gameObject.SetActive(false);
    }
}
