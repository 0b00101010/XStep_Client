using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : Singleton<MainSceneManager>, ITouchObserver
{
    
    [HideInInspector]
    public UIController uiController;

    [SerializeField]
    private Image blackBackground;
    
    private Ray touchRay = new Ray();

    private void Awake(){
        uiController = gameObject.GetComponent<UIController>();
    }

    private void Start(){
        GameManager.instance.touchManager.AddTouchObserver(this);
        GameManager.instance.touchManager.touchType = TouchType.MainScene;

        if (GameManager.instance.gameResult != null) {
            uiController.ResultOpen();
        }
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
    
    public void GameStart() {
        blackBackground.gameObject.SetActive(true);
        var fadeTween = blackBackground.DOFade(1.0f, 0.5f);
        fadeTween.OnComplete(() => SceneManager.LoadScene("01.InGameScene"));
    }
}
