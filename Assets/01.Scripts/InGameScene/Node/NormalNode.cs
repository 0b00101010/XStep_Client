using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalNode : Node
{
    private Vector2 targetPosition;
    private Vector2 startPosition;
    private Vector2 moveDirection;
    
    private SpriteRenderer spriteRenderer;

    [Header("Value")]
    [SerializeField]
    private int arriveFrame;

    private float progressLevel;

    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        startPosition.x = 0;
        startPosition.y = -0.645f;

    }

    public override void Execute(Vector2 targetPosition){
        this.targetPosition = targetPosition;
        moveDirection = (targetPosition - startPosition).normalized;
        gameObject.SetActive(true);
        SetSpriteDirection();
        StartCoroutine(ExecuteCoroutine());
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
        int addScoreValue;
        int judgeLevel;

        switch(progressLevel){
            case var p when progressLevel > 95:
            addScoreValue = (int)(BasicScore * 2.0f);
            judgeLevel = 4;
            break;
            
            case var p when progressLevel > 90:
            addScoreValue = (int)(BasicScore * 1.0f);
            judgeLevel = 3;
            break;
            
            case var p when progressLevel > 80:
            addScoreValue = (int)(BasicScore * 0.75f);
            judgeLevel = 2;            
            break;
            
            case var p when progressLevel > 70:
            addScoreValue = (int)(BasicScore * 0.5f);
            judgeLevel = 1;            
            break;

            default:
            addScoreValue = (int)(BasicScore * 0.0f);
            judgeLevel = 0;           
            break;
        }

        InGameManager.instance.scoreManager.AddScore(addScoreValue, judgeLevel);

    }

    public override void FailedInteraction(){
        base.FailedInteraction();
        ObjectReset();
    }

    public void SetSpriteDirection(){
        spriteRenderer.flipX = targetPosition.x < 0 ? false : true;
        spriteRenderer.flipY = targetPosition.y < 0 ? true : false;
    } 
}
