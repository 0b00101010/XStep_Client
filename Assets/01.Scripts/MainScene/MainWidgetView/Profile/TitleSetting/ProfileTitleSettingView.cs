using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ProfileTitleSettingView : ProfileSettingView
{
    [SerializeField]
    private TitleItem[] titleItems;
    private List<TitleItem> activeItems = new List<TitleItem>();
    private ProfileTitleData titleData;

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [SerializeField]
    private Transform topPosition;

    private void Awake(){
        titleData = Resources.Load<ProfileTitleData>("Player Setting/ProfileTitleData");
        titleItems = gameObject.GetComponentsInChildren<TitleItem>(true);

        for(int i = 0; i < titleData.TitleResources.Length; i++){
            titleItems[i].gameObject.SetActive(true);
            titleItems[i].SettingTitle(titleData.TitleResources[i]);
            titleItems[i].gameObject.transform.SetPositionY(topPosition.position.y - (i / 1.5f));
            activeItems.Add(titleItems[i]);
        }
    }

    private void Update(){
        if(GameManager.instance.touchManager.IsSwipe){
            if(GameManager.instance.touchManager.SwipeDirection.y > 0.8f){
                "a".Log();
                if(activeItems[activeItems.Count - 1].gameObject.transform.position.y < topPosition.position.y){
                    MoveObject(Vector2.up);
                    "b".Log();
                }
            }else if(GameManager.instance.touchManager.SwipeDirection.y < -0.8f){
                if(activeItems[0].gameObject.transform.position.y > topPosition.position.y){
                    MoveObject(Vector2.down);
                }
            }
        }
    }

    private void MoveObject(Vector2 direction){
        foreach(var item in activeItems){
            item.transform.Translate(direction * 0.25f);
        }
    }

    public void TitleChange(Title title){

    }

    public override void Execute(){
        gameObject.SetActive(true);
        titleText.text = GameManager.instance.PlayerSetting.title.title;
        descriptionText.text = GameManager.instance.PlayerSetting.title.description;
    }

    public override void Exit(){
        gameObject.SetActive(false);
    }
}
