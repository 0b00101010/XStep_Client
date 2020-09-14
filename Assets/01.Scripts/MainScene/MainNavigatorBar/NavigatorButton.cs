using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigatorButton : MainUIObject
{
    [SerializeField]
    private string tapName;

    [SerializeField]
    private GameObject tapObject;
    
    private INavigatorButtonObserver observer;

    public string TapName => tapName;

    public override void Execute() {
        SelectNotify();
    }

    public void SettingObserver(INavigatorButtonObserver observer){
        this.observer = observer;
    }

    public void SelectNotify(){
        observer?.SelectNotify(this);
    }

    public void RemoveObserver(){
        this.observer = null;
    }

    public void Enter() {
        tapObject.gameObject.SetActive(true);
    }
    
    public void Exit() {
        tapObject.gameObject.SetActive(false);
    }
}
