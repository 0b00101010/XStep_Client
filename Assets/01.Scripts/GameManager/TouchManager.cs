using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TouchType {MainScene, InGame}

public class TouchManager : MonoBehaviour
{
    private bool isTouch;
    private bool isSwipe;
    private bool isHolding;
    
    private float keepTouchTimer = 0.0f;

    private float minSwipeDistance;

    private Camera mainCamera;
    
    private Vector2 touchDownNotScreenPosition;
    private Vector2 touchDownPosition;
    private Vector2 touchHoldingPosition;
    private Vector2[] touchUpPosition = new Vector2[2];

    private Vector2 swipeDirection;

    private List<ITouchObserver> touchObservers = new List<ITouchObserver>();

    public Vector2 TouchDownPosition => touchDownPosition;
    public Vector2[] TouchUpPosition => touchUpPosition;
    public Vector2 TouchHoldingPosition => touchHoldingPosition;
    public Vector2 SwipeDirection => swipeDirection;

    public bool IsTouch => isTouch;
    public bool IsSwipe => isSwipe;
    public bool IsHolding => isHolding;

    public TouchType touchType {get; set;}

    private void Awake() {
        mainCamera = Camera.main;
        
        minSwipeDistance = Screen.width / 3;
        touchType = TouchType.MainScene;

        SceneManager.sceneLoaded +=  (scene, mode) => {
            touchObservers.Clear();
            mainCamera = Camera.main;
        };

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
        swipeDirection.y = Input.GetAxis("Mouse ScrollWheel") * 10.0f;
        if(swipeDirection.y > 0){
            isSwipe = true;
        }

        if (Input.GetMouseButtonDown(0)) {
            isTouch = true;
            touchDownNotScreenPosition = Input.mousePosition;
            touchDownPosition = mainCamera.ScreenToWorldPoint(touchDownNotScreenPosition);
            TouchDownNotify(0);

        }
        else if(Input.GetMouseButtonUp(0)){
            isHolding = false;
            isSwipe = false;
            isTouch = false;
            keepTouchTimer = 0.0f;
            touchUpPosition[0] = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            TouchUpNotify(0);
        }
    }

    private void ProcessTouch(){
        if(Input.touchCount > 0){
            var tempTouch = Input.touches;

            for (int i = 0; i < 2; i++) {
                if (tempTouch[i].phase.Equals(TouchPhase.Began)) {
                    isTouch = true;
                    touchDownNotScreenPosition = tempTouch[i].position;
                    touchDownPosition = mainCamera.ScreenToWorldPoint(touchDownNotScreenPosition);
                    TouchDownNotify(i);
                }
                else if (tempTouch[i].phase.Equals(TouchPhase.Moved)) {
                    Vector2 currentPosition = tempTouch[i].position;
                    if ((currentPosition - touchDownNotScreenPosition).magnitude > minSwipeDistance) {
                        swipeDirection = (currentPosition - touchDownNotScreenPosition).normalized;
                        isSwipe = true;
                    }
                }
                else if (tempTouch[i].phase.Equals(TouchPhase.Ended)) {
                    isTouch = false;
                    isSwipe = false;
                    isHolding = false;
                    keepTouchTimer = 0.0f;
                    touchUpPosition[i] = mainCamera.ScreenToWorldPoint(tempTouch[i].position);
                    TouchUpNotify(i);
                }
            }
        }
    }

    public void AddTouchObserver(ITouchObserver observer){
        if(!touchObservers.Contains(observer)){
            touchObservers.Add(observer);
        }
    }

    public void RemoveTouchObserver(ITouchObserver observer){
        if(touchObservers.Contains(observer)){
            touchObservers.Remove(observer);
        }
    }

    private void TouchDownNotify(int touchIndex){
        touchObservers.ForEach((observer) => {
            observer.TouchDownNotify(touchIndex);
        });
    }

    private void TouchUpNotify(int touchIndex){
        touchObservers.ForEach((observer) => {
            observer.TouchUpNotify(touchIndex);
        });
    }

}
