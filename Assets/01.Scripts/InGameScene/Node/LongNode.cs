using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Vector2 = UnityEngine.Vector2;

public class LongNode : Node
{  
    private Vector2 startPosition;
    private Vector2 targetPosition;

    private Tween headTween;
    private Tween tailTween;
    private IEnumerator tailCoroutine;
    
    private LineRenderer lineRenderer;

    private Vector2 headVector;
    private Vector2 tailVector;

    private bool isInteraction;
    private bool isFailedInteraction;

    private int interactionFrame;
    private int judgeLevel;
    
    private int position;

    [Header("Events")]
    [SerializeField]
    private Event<Node, int> generateEvent;

    [SerializeField]
    private Event<Node, int> inactiveEvent;

    private new void Awake(){
        base.Awake();

        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    public override void Execute(Vector2 startPosition, Vector2 targetPosition, int position){
        gameObject.SetActive(true);
        
        this.position = position;
        this.startPosition = startPosition;
        this.targetPosition = Vector2.Lerp(startPosition, targetPosition, 0.9f);

        gameObject.transform.position = startPosition;

        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, startPosition);

        headVector = startPosition;
        tailVector = startPosition;

        tailCoroutine = null;
        
        HeadStart();
    }

    private void HeadStart(){
        generateEvent.Invoke(this, position);

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
            headTween = null;
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
        if(isFailedInteraction) {
            return;
        }

        if(isInteraction) {
            interactionFrame++;

            if ((interactionFrame & 9) == 0) {
                if (judgeLevel == 4) {
                    InGameManager.instance.scoreManager.NormalNodeExecuteEffect(position);
                }
                InGameManager.instance.scoreManager.AddScore(judgeLevel);

            }
        } else {
            judgeLevel = 0;
            float processLevel = headTween.ElapsedPercentage();
        
            switch(processLevel){
                case var k when (judgePerfect - processLevel) < 0.01f:
                    judgeLevel = 4;
                    InGameManager.instance.scoreManager.NormalNodeExecuteEffect(position);
                    break; 
                case var k when processLevel > judgeGreat:
                    judgeLevel = 3;
                    break; 
                case var k when processLevel > judgeGood:
                    judgeLevel = 2;
                    break; 
                case var k when processLevel < judgeGood:
                    judgeLevel = 1;
                    break; 
            }

            isInteraction = true;
            InGameManager.instance.scoreManager.AddScore(judgeLevel);
        }
    }

    public override void FailedInteraction(){
        InGameManager.instance.scoreManager.AddScore(0);
        isFailedInteraction = true;
    }

    public override void ObjectReset() {
        interactionFrame = 0;
        gameObject.SetActive(false);
        inactiveEvent.Invoke(this, position);
    }
}
