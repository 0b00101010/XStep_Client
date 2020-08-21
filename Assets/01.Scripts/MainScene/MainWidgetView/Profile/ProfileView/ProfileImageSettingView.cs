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
    private List<GameObject> iconRows = new List<GameObject>();

    private List<Image> iconImages = new List<Image>();
    private ProfileIconHandler iconHandler;

    private void Awake(){
        iconHandler = gameObject.GetComponent<ProfileIconHandler>();

        iconRows.ForEach((icon) => {
            var iconArray = icon.GetComponentsInChildren<Image>(true);
            foreach(var i in iconArray){
                iconImages.Add(i);
            }
        });
    }

    private void Start(){
        ChangeProfileImage(GameManager.instance.PlayerSetting.profileSprite ?? profileImage.sprite);

        var iconData = iconHandler.iconData;

        for(int i = 0; i < iconData.iconCount; i++){
            iconImages[i].sprite = iconData.GetIcon(i).icon;
        }
    }

    private void ChangeProfileImage(Sprite sprite){
        profileImage.sprite = sprite;
    }

    public override void Execute(){
        ChangeProfileImage(GameManager.instance.PlayerSetting.profileSprite);
        gameObject.SetActive(true);
    }    

    public override void Exit(){
        gameObject.SetActive(false);
    }
}
