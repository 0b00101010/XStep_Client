using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TouchType {MainScene, InGame}

public class TouchManager : MonoBehaviour
{
    private bool isTouch;
    private bool isSwipe;
    private bool isHolding;

    private int keepTouchFrame = 0;
    private float keepTouchTimer = 0.0f;

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

    public TouchType touchType {get; set;}

    private void Awake(){
        minSwipeDistance = Screen.width / 3;
        touchType = TouchType.MainScene;
    }

    private void Update(){
        if(!(touchObservers.Count > 0))
            return;

        ProcessTouch();
            
        #if UNITY_EDITOR
        ProcessClick();
        #endif
    }

    private void ProcessClick(){
        if(Input.GetMouseButtonDown(0)){
            touchDownNotScreenPosition = Input.mousePosition;
            
            if(touchType.Equals(TouchType.InGame)){
                touchDownNotScreenPosition = Input.mousePosition;
                touchDownPosition = Camera.main.ScreenToWorldPoint(touchDownNotScreenPosition);
                isTouch = true;
                TouchDownNotify();
            }
        }
        else if(Input.GetMouseButton(0)){
            Vector2 currentPosition = Input.mousePosition;
            keepTouchTimer += Time.deltaTime;
            if((currentPosition - touchDownNotScreenPosition).magnitude > minSwipeDistance){
                swipeDirection = (currentPosition - touchDownNotScreenPosition).normalized;
                isSwipe = true;
            }

            if(keepTouchTimer > 0.15f && !isTouch && !isSwipe){
                touchDownNotScreenPosition = Input.mousePosition;
                touchDownPosition = Camera.main.ScreenToWorldPoint(touchDownNotScreenPosition);
                isTouch = true;
                TouchDownNotify();
            }

            if(keepTouchTimer > 1.5f){
                isHolding = true;
                touchHoldingPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else if(Input.GetMouseButtonUp(0)){
            isHolding = false;
            isSwipe = false;
            isTouch = false;
            keepTouchTimer = 0.0f;
            touchUpPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TouchUpNotify();
        }
    }

    private void ProcessTouch(){
        if(Input.touchCount > 0){
            Touch tempTouch = Input.touches[0];

            if(tempTouch.phase.Equals(TouchPhase.Began)){
                touchDownNotScreenPosition = tempTouch.position;

                if(touchType.Equals(TouchType.InGame)){
                    isTouch = true;
                    touchDownNotScreenPosition = tempTouch.position;
                    touchDownPosition = Camera.main.ScreenToWorldPoint(touchDownNotScreenPosition);
                    TouchDownNotify();
                }

            }
            else if(tempTouch.phase.Equals(TouchPhase.Moved)){
                Vector2 currentPosition = tempTouch.position;
                if((currentPosition - touchDownNotScreenPosition).magnitude > minSwipeDistance){
                    swipeDirection = (currentPosition - touchDownNotScreenPosition).normalized;
                    isSwipe = true;
                }
            }
            else if (tempTouch.phase.Equals(TouchPhase.Stationary)){
                keepTouchTimer += Time.deltaTime;
                if(!isTouch && !isSwipe){
                    isTouch = true;
                    touchDownNotScreenPosition = tempTouch.position;
                    touchDownPosition = Camera.main.ScreenToWorldPoint(touchDownNotScreenPosition);
                    TouchDownNotify();
                }
                if(keepTouchTimer > 1.5f){
                    isHolding = true;
                    touchHoldingPosition = Camera.main.ScreenToWorldPoint(tempTouch.position);
                }
            }
            else if(tempTouch.phase.Equals(TouchPhase.Ended)){
                isTouch = false;
                isSwipe = false;
                isHolding = false;
                keepTouchTimer = 0.0f;
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
