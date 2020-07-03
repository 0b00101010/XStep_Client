using System.Collections.Specialized;
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
    private NODEEvent generateEvnet;

    [SerializeField]
    private NODEEvent destoryEvent;

    private int positionValue;

    private Tween executeTween;
    private Tween resetTween;
    private Tween moveTween;
    private Tween scaleTween;
    
    private new void Awake(){
        base.Awake();
        
        defaultScale = Vector3.one / 5;
        gameObject.transform.localScale = defaultScale;
    }

    public override void Execute(Vector2 startPosition,Vector2 targetPosition){
        this.startPosition = startPosition;
        this.targetPosition = targetPosition;

        gameObject.transform.position = startPosition;

        gameObject.SetActive(true);

        SetSpriteDirection();
        StartCoroutine(ExecuteCoroutine());
        
        generateEvnet.Invoke(this, positionValue);
    }

    private IEnumerator ExecuteCoroutine(){
        executeTween = spriteRenderer.DOFade(1.0f, 0.2f);
        yield return executeTween.WaitForCompletion();

        moveTween = gameObject.transform.DOMove(targetPosition, arriveTime);
        scaleTween = gameObject.transform.DOScale(Vector3.one, arriveTime);
    }

    public override void Interaction(){
        int judgeLevel = 0;
        float processLevel = moveTween.position;
        
        switch(processLevel){
            case var k when (judgePerfect - processLevel) < 0.01f:
            judgeLevel = 4;
            InGameManager.instance.scoreManager.NormalNodeExecuteEffect(positionValue);
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

        destoryEvent.Invoke(this, positionValue);
        InGameManager.instance.scoreManager.AddScore(judgeLevel);
        ObjectReset();
    }

    public override void FailedInteraction(){
        base.FailedInteraction();
        destoryEvent.Invoke(this, positionValue);
        
        InGameManager.instance.scoreManager.AddScore(0);

        StartCoroutine(FailedInteractionCoroutine());
    }

    private IEnumerator FailedInteractionCoroutine(){
        resetTween = spriteRenderer.DOFade(0.0f, 0.3f);
        yield return resetTween.WaitForCompletion();
        ObjectReset();
    }

    public override void ObjectReset(){
        positionValue = 0;

        moveTween.Kill();
        scaleTween.Kill();

        gameObject.transform.position = startPosition;
        gameObject.transform.localScale = defaultScale;     

        moveTween = null;
        scaleTween = null;
        executeTween = null;

        base.ObjectReset();
    }

    public void SetSpriteDirection(){
        spriteRenderer.flipX = targetPosition.x < 0 ? false : true;
        spriteRenderer.flipY = targetPosition.y < 0 ? true : false;
        
        positionValue += targetPosition.x < 0 ? 1 : 2; 
        positionValue += targetPosition.y < 0 ? 1 : -1; 
        
    } 

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("NodeJudge")){
            FailedInteraction();
        }
    }
}
