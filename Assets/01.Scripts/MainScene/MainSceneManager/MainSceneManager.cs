using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : SingletonObject<MainSceneManager>, ITouchObserver
{
    
    [HideInInspector]
    public UIController uiController;

    [SerializeField]
    private Image blackBackground;

    private SongItemInformation[] songItems; 
    
    private Ray touchRay = new Ray();

    private void Awake(){
        uiController = gameObject.GetComponent<UIController>();
        songItems = Resources.LoadAll<SongItemInformation>("Map Data");
    }

    private void Start(){
        GameManager.Instance.touchManager.AddTouchObserver(this);
        GameManager.Instance.touchManager.touchType = TouchType.MainScene;
        GameManager.Instance.CurrentSceneType = SceneType.MAIN;
        if (GameManager.Instance.gameResult != null) {
            uiController.ResultOpen();
        }
    }

    public void TouchDownNotify(int touchIndex){
    }

    public void TouchUpNotify(int touchIndex){
        if (GameManager.Instance.touchManager.IsSwiped == false) {
            GetMainUIObject()?.Execute();
        }
    }

    private MainUIObject GetMainUIObject(){
        RaycastHit2D hit2D;
        
        touchRay.origin = GameManager.Instance.touchManager.TouchDownPosition;
        touchRay.direction = Vector2.zero;
        
        hit2D = Physics2D.Raycast(touchRay.origin, touchRay.direction, Mathf.Infinity, LayerMask.GetMask("MainUIObject"));

        return hit2D.collider?.GetComponent<MainUIObject>();
    }
    
    public void GameStart() {
        if (GameManager.Instance.selectSongItem.MapFile.currentSelectDifficulty == -1) {
            return;
        }
        
        GameManager.Instance.UnlockAchieves.Clear();
        
        blackBackground.gameObject.SetActive(true);
        var fadeTween = blackBackground.DOFade(1.0f, 0.5f);
        fadeTween.OnComplete(() => {
            SceneManager.LoadScene("01.InGameScene");
            GameManager.Instance.SomeUIInteraction = false;
        });
    }
    
    [Button("Song Items Reset")]
    public void SongItemsReset() {
        for (int i = 0; i < songItems.Length; i++) {
            for (int j = 0; j < songItems[i].HighScore.Length; j++) {
                songItems[i].HighScore[j] = 0.0f;
            }
        }
    }
}
