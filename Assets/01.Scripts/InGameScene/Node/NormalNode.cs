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

    [SerializeField]
    private float arriveTime;

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
        for(int i = 0; i <= 60; i++){
            gameObject.transform.position = Vector2.Lerp(startPosition, targetPosition, i / 60.0f);
            gameObject.transform.localScale = Vector2.Lerp(Vector3.zero, Vector3.one, i / 60.0f);
            yield return YieldInstructionCache.WaitFrame;
        }

        ObjectReset();
    }

    public override void ObjectReset(){
        gameObject.SetActive(false);
        gameObject.transform.position = startPosition;
    }

    public void SetSpriteDirection(){
        spriteRenderer.flipX = targetPosition.x < 0 ? false : true;
        spriteRenderer.flipY = targetPosition.y < 0 ? true : false;
    } 
}
