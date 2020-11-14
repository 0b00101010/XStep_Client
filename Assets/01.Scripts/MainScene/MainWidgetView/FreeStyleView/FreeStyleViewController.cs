using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeStyleViewController : MonoBehaviour {
    [SerializeField]
    private GameObject songItemsParent;
    
    private SongItem[] songItems;

    [SerializeField]
    private Transform sortPivotObject;

    private ViewSongInformation songInformationViewer;

    private bool isPannerOpen = false;

    private Vector2 moveVector;


    private void Awake(){
        songInformationViewer = gameObject.GetComponent<ViewSongInformation>();
        songInformationViewer.SettingPannerActiveCheckFunction((value) => {
            isPannerOpen = value;
            GameManager.Instance.SomeUIInteraction = value;
        });

        songItems = songItemsParent.GetComponentsInChildren<SongItem>(true);
        moveVector = Vector2.up / 3;
        for (int i = 0; i < songItems.Length; i++) {
            songItems[i].gameObject.transform.position = sortPivotObject.transform.position - (Vector3.up * (i * 1.5f));
        }
    }

    private void Update(){
        if(GameManager.Instance.touchManager.IsSwipe && !isPannerOpen && !GameManager.Instance.SomeUIInteraction){
            if(GameManager.Instance.touchManager.SwipeDirection.y > 0.8f){
                if(songItems[songItems.Length - 1].gameObject.transform.position.y < sortPivotObject.position.y){
                    MoveObjects(moveVector);
                }
            }
            else if(GameManager.Instance.touchManager.SwipeDirection.y < -0.8f){
                if(songItems[0].gameObject.transform.position.y > sortPivotObject.position.y){
                    MoveObjects(-moveVector);
                }
            }
        }
    }

    private void MoveObjects(Vector2 direction){
        foreach (SongItem item in songItems)
        {
            item.transform.Translate(direction * 0.5f);
        }
    }

    // FIXME : 진짜 존나 비효율적
    public void OpenPanner(SongItemInformation information){
        if(!isPannerOpen && !GameManager.Instance.SomeUIInteraction){
            songInformationViewer.SettingInformation(information);
            songInformationViewer.OpenSongInformation();
        }
    }

    public void ClosePanner(){
        songInformationViewer.CloseSongInformation();
    }
}
