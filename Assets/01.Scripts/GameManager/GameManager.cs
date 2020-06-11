using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public TouchManager touchManager;
    
    [HideInInspector]
    public WidgetViewer widgetViewer;

    private void Awake(){ 
        if(instance is null){
            instance = this;
        }
        
        touchManager = gameObject.GetComponent<TouchManager>();
        widgetViewer = gameObject.GetComponent<WidgetViewer>();
        
        DontDestroyOnLoad(this);
    }
}
