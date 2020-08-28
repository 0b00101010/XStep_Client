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

    private int index;

    [Header("Events")]
    [SerializeField]
    private Event<Node, int> generateEvent;

    [SerializeField]
    private Event<Node, int> inactiveEvent;

    private new void Awake(){
        base.Awake();

        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    public override void Execute(Vector2 startPosition, Vector2 targetPosition, int index){
        gameObject.SetActive(true);
        
        this.index = index;
        this.startPosition = startPosition;
        this.targetPosition = Vector2.Lerp(startPosition, targetPosition, 0.9f);

        gameObject.transform.position = startPosition;

        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, startPosition);

        headVector = startPosition;
        tailVector = startPosition;

        generateEvent.Invoke(this, index);

        HeadStart();
    }

    private void HeadStart(){
        headTween?.Kill();

        headTween = DOTween.To(() => headVector, x => headVector = x, targetPosition, arriveTime);

        headTween.OnUpdate(() => {
            lineRenderer.SetPosition(0, headVector);
        });

        headTween.OnComplete(() => {
            if(!isInteraction){
                FailedInteraction();
            }
        });

        headTween.OnKill(() => {
            tailTween = null;
        }); 
    }

    public bool TailStart(){
        if(tailTween != null && tailTween.IsPlaying()){
            return true;
        }

        tailTween?.Kill();

        tailTween = DOTween.To(() => tailVector, x => tailVector = x, targetPosition, arriveTime);

        tailTween.OnUpdate(() => {
            lineRenderer.SetPosition(1, tailVector);
        });

        tailTween.OnComplete(() => {
            ObjectReset();
        });

        tailTween.OnKill(() => {
            tailTween = null;
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
        inactiveEvent.Invoke(this, index);
        gameObject.SetActive(false);
    }
}
