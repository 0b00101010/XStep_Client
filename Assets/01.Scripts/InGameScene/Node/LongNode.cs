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

    private int index;

    [Header("Events")]
    [SerializeField]
    private Event<Node, int> generateEvent;

    [SerializeField]
    private Event<Node, int> inactiveEvent;

    private new void Awake(){
        base.Awake();

        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    public override void Execute(Vector2 startPosition, Vector2 targetPosition, int index){
        gameObject.SetActive(true);
        
        this.index = index;
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
        generateEvent.Invoke(this, index);

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
            tailTween = null;
        }); 
    }

    public bool TailStart(){
        if (tailCoroutine != null) {
            return true;
        }

        tailCoroutine = TailStartCoroutine().Start(this);

        return false;
    }

    private IEnumerator TailStartCoroutine() {
        var firstTailVector = tailVector;
        var waitingTime = new WaitForSeconds(arriveTime / 60.0f);

        for (int i = 0; i < 60; i++) {
            tailVector = Vector2.Lerp(firstTailVector, targetPosition, (i / 60.0f));
            lineRenderer.SetPosition(1, tailVector);
            yield return waitingTime;
        }
        
        ObjectReset();
        tailCoroutine = null;
    }

    public override void Interaction(){
        if(isFailedInteraction){
            return;
        }

        if(isInteraction){

        } else {

        }
    }

    public override void FailedInteraction(){
        isFailedInteraction = true;
    }

    public override void ObjectReset(){
        gameObject.SetActive(false);
        inactiveEvent.Invoke(this, index);
    }
}
