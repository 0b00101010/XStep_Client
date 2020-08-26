using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventTools.Event;
using TMPro;
public class TitleItem : MainUIObject
{
    private Title item;

    [SerializeField]
    private TextMeshProUGUI titleName;

    [SerializeField]
    private Image unlockImage;

    [SerializeField]
    private Sprite[] unlockResources;
    
    [SerializeField]
    private UniEvent<Title> executeEvent;

    public override void Execute(){
        executeEvent.Invoke(item);
    }

    public void SettingTitle(Title title){
        item = title;
        titleName.text = title.title;
        unlockImage.sprite = title.isUnLock ? unlockResources[0] : unlockResources[1];
    }
}
