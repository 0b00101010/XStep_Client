using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileImageSettingView : ProfileSettingView
{
    [Header("Objects")]
    [SerializeField]
    private Image profileImage;

    [SerializeField]
    private Image headerProfileImage;

    [SerializeField]
    private Image informationViewImage;

    private List<ProfileIconButton> iconImages = new List<ProfileIconButton>();
    private ProfileIconHandler iconHandler;

    private void Awake(){
        iconHandler = gameObject.GetComponent<ProfileIconHandler>();

        var iconArray = gameObject.GetComponentsInChildren<ProfileIconButton>();

        foreach(var icon in iconArray){
            iconImages.Add(icon);
        }
    }

    private void Start(){
        ChangeProfileImage(GameManager.instance.PlayerSetting.profileSprite ?? profileImage.sprite);

        var iconData = iconHandler.iconData;

        // FIXME : GetIcon이 bool 이미 확인 하는데 이거 어떻게 못 고치나
        for(int i = 0; i < iconData.iconCount; i++){
            iconImages[i].ImageSetting(iconData.GetIcon(i).icon, iconData.GetIcon(i).isUnlock);
        }
    }

    // FIXME : 이거 내가 봤을 때 바꿀 필요 있음, 배열로라던지
    public void ChangeProfileImage(Sprite sprite){
        profileImage.sprite = sprite;
        headerProfileImage.sprite = sprite;
        informationViewImage.sprite = sprite;
    }

    public override void Execute(){
        ChangeProfileImage(GameManager.instance.PlayerSetting.profileSprite);
        gameObject.SetActive(true);
    }    

    public override void Exit(){
        gameObject.SetActive(false);
    }
}
