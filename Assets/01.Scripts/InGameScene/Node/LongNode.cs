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

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        

    }

    public override void Execute(Vector2 startPosition, Vector2 targetPosition){
        gameObject.SetActive(true);

        this.startPosition = startPosition;
        this.targetPosition = Vector2.Lerp(startPosition, targetPosition, 0.9f);

        gameObject.transform.position = startPosition;

        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, startPosition);

        headVector = startPosition;
        tailVector = startPosition;

        HeadStart();

        IEnumerator tailCoroutine(){
            yield return YieldInstructionCache.WaitingSeconds(1.0f);
            TailStart();
        }

        tailCoroutine().Start(this);
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

    public bool TailStart(){
        if(tailTween != null){
            return true;
        }

        tailTween?.Kill();

        tailTween = DOTween.To(() => tailVector, x => tailVector = x, targetPosition, arriveTime / 2);

        tailTween.OnUpdate(() => {
            lineRenderer.SetPosition(1, tailVector);
        });

        tailTween.OnComplete(() => {
            ObjectReset();
        });

        return false;
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
        gameObject.SetActive(false);
    }
}
