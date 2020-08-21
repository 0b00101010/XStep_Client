using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileImageSettingView : ProfileSettingView
{
    [Header("Objects")]
    [SerializeField]
    private Image profileImage;

    private string _titleString = "Change Player Icon";

    private void Start(){
        ChangeProfileImage(GameManager.instance.PlayerSetting.profileSprite ?? profileImage.sprite);
        titleString = _titleString;
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
