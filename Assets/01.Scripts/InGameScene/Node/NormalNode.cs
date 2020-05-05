using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalNode : MonoBehaviour
{
    private Vector2 targetPosition;
    private Vector2 startPosition;
    private Vector2 moveDirection;
    
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float moveFrame;

    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        startPosition.x = 0;
        startPosition.y = -0.645f;

    }

    public void Execute(Vector2 targetPosition){
        this.targetPosition = targetPosition;
        moveDirection = (targetPosition - startPosition).normalized;
        SetSpriteDirection();
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        for(int repeatFrame = 1; repeatFrame <= moveFrame; repeatFrame++){
            gameObject.transform.position = Vector2.Lerp(startPosition, targetPosition, repeatFrame / moveFrame);
            gameObject.transform.localScale = Vector2.Lerp(Vector3.zero, Vector3.one, repeatFrame / moveFrame);
            yield return YieldInstructionCache.WaitFrame;
        }
    }

    private void ObjectReset(){
        gameObject.SetActive(false);
        gameObject.transform.position = startPosition;
    }

    public void SetSpriteDirection(){
        spriteRenderer.flipX = targetPosition.x < 0 ? false : true;
        spriteRenderer.flipY = targetPosition.y < 0 ? true : false;
    } 
}
