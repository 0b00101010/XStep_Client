using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigatorButton : MainUIObject
{
    [SerializeField]
    private string tapName;

    private INavigatorButtonObserver observer;

    public string TapName => tapName;

    public override void Execute(){
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
}
