using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SlideNode : Node
{
    [Header("Values")]
    [SerializeField]
    private float arriveSecond; 

    private Tween slideTween;

    private Vector2 startPosition;
    private Vector2 targetPosition;

    private Color transparent;

    private new void Awake(){
        base.Awake();
        
        transparent = new Color(1,1,1,0);
        spriteRenderer.color = transparent;
    }
    
    public override void Execute(Vector2 startPosition, Vector2 targetPosition){
        gameObject.SetActive(true);

        this.startPosition = startPosition;
        this.targetPosition = targetPosition;

        gameObject.transform.position = startPosition;

        StartCoroutine(ExecuteCoroutine());
    }   

    private IEnumerator ExecuteCoroutine(){        
        slideTween = spriteRenderer.DOFade(1,1.0f);
        yield return new WaitForTween(slideTween);

        slideTween = gameObject.transform.DOMove(targetPosition, arriveSecond);
        yield return slideTween.WaitForCompletion();

        ObjectReset();
    }

    public override void Interaction(){
        
    }

    public override void FailedInteraction(){

    }

    public override void ObjectReset(){
        base.ObjectReset();
        spriteRenderer.color = transparent;
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

    /// 0 : top left    1 : top right
    /// 2 : bottom left 3 : bottom right
    /// 4 : left up     5 : left down
    /// 6 : right up    7 : right down
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
}
