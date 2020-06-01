using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NormalNode : Node
{
    private Vector2 targetPosition;
    private Vector2 startPosition;
    private Vector2 moveDirection;
    
    private Vector3 defaultScale;

    [Header("Value")]
    [SerializeField]
    private float arriveTime;

    [Header("Functions")]
    [SerializeField]
    private NODEEvent generateEvnet;

    [SerializeField]
    private NODEEvent destoryEvent;

    private float progressLevel;

    private int positionValue;

    private Tween executeTween;

    private new void Awake(){
        base.Awake();
        startPosition.x = 0;
        startPosition.y = -0.645f;
        
        defaultScale = Vector3.one / 5;

        gameObject.transform.localScale = defaultScale;
        gameObject.transform.position = startPosition;

    }

    public override void Execute(Vector2 targetPosition){
        this.targetPosition = targetPosition;
        moveDirection = (targetPosition - startPosition).normalized;
        gameObject.SetActive(true);
        SetSpriteDirection();
        StartCoroutine(ExecuteCoroutine());
        generateEvnet.Invoke(this, positionValue);
    }

    private IEnumerator ExecuteCoroutine(){
        executeTween = spriteRenderer.DOFade(1.0f, 0.2f);
        yield return executeTween.WaitForCompletion();

        executeTween = gameObject.transform.DOMove(targetPosition, arriveTime);
        gameObject.transform.DOScale(Vector3.one, arriveTime);
        yield return executeTween.WaitForCompletion();
        FailedInteraction();
    }

    public override void Interaction(){
        int judgeLevel;

        switch(progressLevel){
            case var p when progressLevel > 0.95f:
            judgeLevel = 4;
            break;
            
            case var p when progressLevel > 0.90f:
            judgeLevel = 3;
            break;
            
            case var p when progressLevel > 0.80f:
            judgeLevel = 2;            
            break;
            
            case var p when progressLevel > 0.70f:
            judgeLevel = 1;            
            break;

            default:
            judgeLevel = 0;           
            break;
        }

        InGameManager.instance.scoreManager.AddScore(judgeLevel);
        ObjectReset();
    }

    public override void FailedInteraction(){
        base.FailedInteraction();
        destoryEvent.Invoke(this, positionValue);
        StartCoroutine(FailedInteractionCoroutine());
    }

    private IEnumerator FailedInteractionCoroutine(){
        executeTween = spriteRenderer.DOFade(0.0f, 0.1f);

        for(int i = 0; i < 20; i++){
            gameObject.transform.Translate(moveDirection * Time.deltaTime);
            yield return YieldInstructionCache.WaitFrame;
        }

        ObjectReset();
    }

    public override void ObjectReset(){
        base.ObjectReset();
        gameObject.transform.position = startPosition;
        gameObject.transform.localScale = defaultScale;
    }

    public void SetSpriteDirection(){
        spriteRenderer.flipX = targetPosition.x < 0 ? false : true;
        spriteRenderer.flipY = targetPosition.y < 0 ? true : false;
        
        if(targetPosition.x < 0 && targetPosition.y > 0){
            positionValue = 0;
        }   
        else if(targetPosition.x > 0 && targetPosition.y > 0){
            positionValue = 1;
        }      
        else if(targetPosition.x < 0 && targetPosition.y < 0){
            positionValue = 2;
        }      
        else if(targetPosition.x > 0 && targetPosition.y < 0){
            positionValue = 3;
        }   

    } 
}
