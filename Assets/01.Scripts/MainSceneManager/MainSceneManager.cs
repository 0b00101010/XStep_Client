using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour, ITouchObserver
{
    private Ray touchRay = new Ray();

    private void Start(){
        GameManager.instance.touchManager.AddTouchObserver(this);
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
