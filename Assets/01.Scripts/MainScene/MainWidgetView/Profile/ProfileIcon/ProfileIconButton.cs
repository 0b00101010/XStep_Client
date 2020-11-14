using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventTools.Event;

public class ProfileIconButton : MainUIObject
{
    private Image buttonImage;
    public Image ButtonImage => buttonImage;
    private bool isUnlock;

    [SerializeField]
    private UniEvent<Sprite> spriteChangeEvent;

    private Sprite sprite;

    private void Awake(){
        buttonImage = gameObject.GetComponent<Image>(); 
    }

    public void ImageSetting(Sprite sprite, bool isUnlock){
        buttonImage.sprite = sprite;
        this.sprite = sprite;
        this.isUnlock = isUnlock;
    }   
    
    public override void Execute(){
        if(isUnlock){
            spriteChangeEvent.Invoke(sprite);
            GameManager.Instance.PlayerSetting.profileSprite = sprite;
        }
    }
}
