using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private bool isTouch;
    private bool isSwipe;
    private bool isHolding;

    private float minSwipeDistance;

    private Vector2 touchDownNotScreenPosition;
    private Vector2 touchDownPosition;
    private Vector2 touchHoldingPosition;
    private Vector2 touchUpPosition;

    private Vector2 swipeDirection;

    private List<ITouchObserver> touchObservers = new List<ITouchObserver>();

    public Vector2 TouchDownPosition => touchDownPosition;
    public Vector2 TouchUpPosition => touchUpPosition;
    public Vector2 TouchHoldingPosition => touchHoldingPosition;
    public Vector2 SwipeDirection => swipeDirection;

    public bool IsTouch => isTouch;
    public bool IsSwipe => isSwipe;
    public bool IsHolding => isHolding;

    private void Awake(){
        minSwipeDistance = Screen.width / 2;
    }

    private void Update(){
        ProcessTouch();
    }

    private void ProcessTouch(){
        if(Input.touchCount > 0){
            Touch tempTouch = Input.touches[0];

            if(tempTouch.phase.Equals(TouchPhase.Began)){
                isTouch = true;
                touchDownNotScreenPosition = tempTouch.position;
                touchDownPosition = Camera.main.ScreenToWorldPoint(touchDownNotScreenPosition);
                TouchDownNotify();
            }
            else if(tempTouch.phase.Equals(TouchPhase.Moved)){
                Vector2 currentPosition = tempTouch.position;
                if((currentPosition - touchDownNotScreenPosition).magnitude > minSwipeDistance){
                    swipeDirection = (currentPosition - touchDownNotScreenPosition).normalized;
                    isSwipe = true;
                }
            }
            else if (tempTouch.phase.Equals(TouchPhase.Stationary)){
                isHolding = true;
                isSwipe = false;
                touchHoldingPosition = Camera.main.ScreenToWorldPoint(tempTouch.position);
            }
            else if(tempTouch.phase.Equals(TouchPhase.Ended)){
                isTouch = false;
                isSwipe = false;
                isHolding = false;
                touchUpPosition = Camera.main.ScreenToWorldPoint(tempTouch.position);
                TouchUpNotify();
            }
        }
    }

    public void AddTouchObserver(ITouchObserver observer){
        touchObservers.Add(observer);
    }

    public void RemoveTouchObserver(ITouchObserver observer){
        touchObservers.Remove(observer);
    }

    private void TouchDownNotify(){
        touchObservers.ForEach((observer) => {
            observer.TouchDownNotify();
        });
    }

    private void TouchUpNotify(){
        touchObservers.ForEach((observer) => {
            observer.TouchUpNotify();
        });
    }

}
