using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SlideNode : Node
{
    [Header("Values")]
    [SerializeField]
    private float arriveSecond; 

    private int positionValue;
    private int directionValue;

    private Tween slideTween;
    private Tween resetTween;
    
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private Vector2 slideDirection;

    [Header("Events")]
    [SerializeField]
    private NODEEvent generateEvent;

    [SerializeField]
    private NODEEvent destoryEvent;

    public Vector2 SlideDirection => slideDirection;

    private new void Awake(){
        base.Awake();
    }
    
    public override void Execute(Vector2 startPosition, Vector2 targetPosition, int index){
        gameObject.SetActive(true);

        SetSpriteFlip(index);
        SetObjectRotate(index);
        SetSlideDirection(index);

        positionValue = SlideNodeProcess(index);

        this.startPosition = startPosition;
        this.targetPosition = targetPosition;

        gameObject.transform.position = startPosition;

        generateEvent.Invoke(this, positionValue);
        ExecuteCoroutine().Start(this);
    }   

    private IEnumerator ExecuteCoroutine(){        
        slideTween = spriteRenderer.DOFade(1,1.0f);
        yield return slideTween.WaitForCompletion();

        slideTween = gameObject.transform.DOMove(targetPosition, arriveSecond);
        yield return slideTween.WaitForCompletion();

        FailedInteraction();
    }

    public override void Interaction(){
        int judgeLevel = 0;
        float processLevel = slideTween.position;
        
        switch(processLevel){
            case var k when (judgePerfect - processLevel) < 0.01f:
            judgeLevel = 4;
            InGameManager.instance.scoreManager.SlideNodeExecuteEffect(positionValue, directionValue);
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
        resetTween = spriteRenderer.DOFade(0, 0.25f);
        InGameManager.instance.scoreManager.AddScore(0);
        FailedInteractionCoroutine().Start(this);

        // InGameManager.instance.scoreManager.AddScore(4);
        // InGameManager.instance.scoreManager.SlideNodeExecuteEffect(positionValue, directionValue);
        // ObjectReset();
    }

    public IEnumerator FailedInteractionCoroutine(){
        destoryEvent.Invoke(this, positionValue);
        yield return resetTween.WaitForCompletion();
        ObjectReset();
    }

    public override void ObjectReset(){
        resetTween.Kill();
        slideTween.Kill();
        
        gameObject.transform.position = Vector2.zero;
        base.ObjectReset();
    }
    
    // 0,1,2,3 : Rotate z = 0
    // 4,5,6,7 : Rotate z = 90
    public void SetObjectRotate(int index){
        if(index < 4){
            gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
        }
        else{
            gameObject.transform.localRotation = Quaternion.Euler(0,0,90);
        }
    }

    // Arrive Position Swipe Direction
    // 0 : top left    1 : top right
    // 2 : bottom left 3 : bottom right
    // 4 : left up     5 : left down
    // 6 : right up    7 : right down
    public void SetSpriteFlip(int index){
        switch(index){
            case 0:
            case 5:
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = false;
            break;
            
            case 1:
            case 4:
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
            break;
            
            case 2:
            case 7:
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = true;
            break;
            
            case 3:
            case 6:
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = true;
            break;

            default:
            break;            
        }
    }

    private void SetSlideDirection(int index){
        switch(index){
            case 0:
            case 2:
            slideDirection = Vector2.left;
            directionValue = 1;
            break;

            case 1:
            case 3:
            slideDirection = Vector2.right;
            directionValue = 0;
            break;

            case 4:
            case 6:
            slideDirection = Vector2.up;
            directionValue = 1;
            break;

            case 5:
            case 7:
            slideDirection = Vector2.down;
            directionValue = 0;
            break;
        }
    }

    // Arrive position
    // Top(0) Bottom(1) Left(2) Right(3)
    private int SlideNodeProcess(int index){
        switch(index){
            case 0:
            case 1:
            return 0;
            
            case 2:
            case 3:
            return 1;            
            
            case 4:
            case 5:
            return 2;
            
            case 6:
            case 7:
            return 3;
        }
        return -1;
    }

}
