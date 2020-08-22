using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileIconButton : MonoBehaviour
{
    private Image buttonImage;
    public Image ButtonImage => buttonImage;
    private bool isUnlock;

    private void Awake(){
        buttonImage = gameObject.GetComponent<Image>(); 
    }

    public void ImageSetting(Sprite sprite, bool isUnlock){
        buttonImage.sprite = sprite;
        this.isUnlock = isUnlock;
    }   
}
