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


    private void Awake(){
        songInformationViewer = gameObject.GetComponent<ViewSongInformation>();
        songInformationViewer.SettingPannerActiveCheckFunction((value) => {
            isPannerOpen = value;
            GameManager.instance.SomeUIInteraction = value;
        });

        songItems = songItemsParent.GetComponentsInChildren<SongItem>(true);

        for (int i = 0; i < songItems.Length; i++) {
            songItems[i].gameObject.transform.position = sortPivotObject.transform.position - (Vector3.up * (i * 1.5f));
        }
    }

    private void Update(){
        if(GameManager.instance.touchManager.IsSwipe && !isPannerOpen && !GameManager.instance.SomeUIInteraction){
            if(GameManager.instance.touchManager.SwipeDirection.y > 0.8f){
                if(songItems[songItems.Length - 1].gameObject.transform.position.y < sortPivotObject.position.y){
                    MoveObjects(Vector2.up);
                }
            }
            else if(GameManager.instance.touchManager.SwipeDirection.y < -0.8f){
                if(songItems[0].gameObject.transform.position.y > sortPivotObject.position.y){
                    MoveObjects(Vector2.down);
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
        if(!isPannerOpen && !GameManager.instance.SomeUIInteraction){
            songInformationViewer.SettingInformations(information);
            songInformationViewer.OpenSongInformation();
        }
    }

    public void ClosePanner(){
        songInformationViewer.CloseSongInformation();
    }
}
