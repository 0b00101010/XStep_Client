using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalNode : Node
{
    private Vector2 targetPosition;
    private Vector2 startPosition;
    private Vector2 moveDirection;
    
    [Header("Value")]
    [SerializeField]
    private int arriveFrame;

    [Header("Functions")]
    [SerializeField]
    private NODEEvent generateEvnet;

    [SerializeField]
    private NODEEvent destoryEvent;

    private float progressLevel;

    private int positionValue;


    private new void Awake(){
        base.Awake();
        startPosition.x = 0;
        startPosition.y = -0.645f;
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
        for(int i = 0; i <= arriveFrame; i++){
            gameObject.transform.position = Vector2.Lerp(startPosition, targetPosition, i / (float)arriveFrame);
            gameObject.transform.localScale = Vector2.Lerp(Vector3.zero, Vector3.one, i / (float)arriveFrame);
            progressLevel = i / (float)arriveFrame;
            yield return YieldInstructionCache.WaitFrame;
        }

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
        ObjectReset();
    }

    public override void ObjectReset(){
        base.ObjectReset();
        destoryEvent.Invoke(this, positionValue);
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
