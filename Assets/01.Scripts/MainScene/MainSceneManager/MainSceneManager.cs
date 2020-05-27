using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour, ITouchObserver
{
    public static MainSceneManager instance;
    
    [HideInInspector]
    public UIController uiController;
    
    private Ray touchRay = new Ray();

    private void Awake(){
        if(instance is null){
            instance = this;
        }

        uiController = gameObject.GetComponent<UIController>();
    }

    private void Start(){
        GameManager.instance.touchManager.AddTouchObserver(this);
        GameManager.instance.touchManager.touchType = TouchType.MainScene;
    }

    public void TouchDownNotify(){
        GetMainUIObject()?.Execute();
    }

    public void TouchUpNotify(){

    }

    private MainUIObject GetMainUIObject(){
        RaycastHit2D hit2D;
        
        touchRay.origin = GameManager.instance.touchManager.TouchDownPosition;
        touchRay.direction = Vector2.zero;
        
        hit2D = Physics2D.Raycast(touchRay.origin, touchRay.direction, Mathf.Infinity, LayerMask.GetMask("MainUIObject"));

        return hit2D.collider?.GetComponent<MainUIObject>();
    }

    private void OnDestroy() {
        GameManager.instance.touchManager.RemoveTouchObserver(this);
    }
}
