using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NormalNode : Node
{
    private Vector2 targetPosition;
    private Vector2 startPosition;
    
    private Vector3 defaultScale;

    [Header("Events")]
    [SerializeField]
    private Event<Node, int> generateEvent;

    [SerializeField]
    private Event<Node, int> destroyEvent;

    private int positionValue;
    private bool isInteraction;
    
    private Tween executeTween;
    private Tween resetTween;
    private Tween moveTween;
    private Tween scaleTween;
    
    private new void Awake(){
        base.Awake();
        
        defaultScale = Vector3.one / 5;
        gameObject.transform.localScale = defaultScale;
    }
    
    public override void Execute(Vector2 startPosition,Vector2 targetPosition,double generateTime){
        gameObject.SetActive(true);

        this.startPosition = startPosition;
        this.targetPosition = targetPosition;
        this.generateTime = generateTime;
        this.perfectSample = generateTime + arriveTimeToSample;

        gameObject.transform.position = startPosition;

        SetSpriteDirection();
        ExecuteCoroutine().Start(this);
        
        generateEvent.Invoke(this, positionValue);
    }

    private IEnumerator ExecuteCoroutine(){
        executeTween = spriteRenderer.DOFade(1.0f, 0.2f);
        yield return executeTween.WaitForCompletion();

        moveTween = gameObject.transform.DOMove(Vector2.Lerp(startPosition,targetPosition, 0.75f), arriveTime - 0.2f).SetEase(Ease.Linear);
        scaleTween = gameObject.transform.DOScale(Vector3.one, arriveTime - 0.2f).SetEase(Ease.Linear);

        yield return moveTween.WaitForCompletion();
        yield return new WaitWhile( () => perfectSample + judgeBad > GetCurrentTimeSample());

        if (isInteraction == false) {
            FailedInteraction();
        }
    }

    public override void Interaction(double interactionTime){
        if (moveTween == null) {
            return;
        }        
        
        int judgeLevel = 0;
        double processLevel = Math.Abs(perfectSample - interactionTime);
        
        switch(processLevel){
            case var k when processLevel < judgePerfect:
                judgeLevel = 4;
                InGameManager.instance.scoreManager.NormalNodeExecuteEffect(positionValue);
                break; 
            case var k when processLevel < judgeGreat:
                judgeLevel = 3;
                break; 
            case var k when processLevel < judgeGood:
                judgeLevel = 2;
                break; 
            default:
                judgeLevel = 1;
                break; 
        }

        isInteraction = true;

        InGameManager.instance.scoreManager.AddScore(judgeLevel);
        ResetCoroutine().Start(this);
    }

    public override void FailedInteraction(){
        InGameManager.instance.scoreManager.AddScore(0);
        ResetCoroutine().Start(this);
    }

    private IEnumerator ResetCoroutine(){
        destroyEvent.Invoke(this, positionValue);

        moveTween?.Kill();
        moveTween = gameObject.transform.DOMove(targetPosition, 0.2f).SetRelative().SetSpeedBased();
        resetTween = spriteRenderer.DOFade(0.0f, 0.2f);
        yield return resetTween.WaitForCompletion();
        
        ObjectReset();
    }

    public override void ObjectReset(){
        positionValue = 0;

        moveTween.Kill();
        scaleTween.Kill();
        resetTween.Kill();
        
        gameObject.transform.position = startPosition;
        gameObject.transform.localScale = defaultScale;     

        moveTween = null;
        scaleTween = null;
        executeTween = null;

        isInteraction = false;

        base.ObjectReset();
    }

    public void SetSpriteDirection(){
        spriteRenderer.flipX = targetPosition.x < 0 ? false : true;
        spriteRenderer.flipY = targetPosition.y < 0 ? true : false;
        
        positionValue += targetPosition.x < 0 ? 1 : 2; 
        positionValue += targetPosition.y < 0 ? 1 : -1; 
        
    }
}
