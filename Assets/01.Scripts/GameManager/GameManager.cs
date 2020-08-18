using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : DontDestroySingleton<GameManager>
{
    [HideInInspector]
    public TouchManager touchManager;
    
    [HideInInspector]
    public WidgetViewer widgetViewer;

    [Header("Setting")]
    [SerializeField]
    private InGameResources _inGameResources;

    [SerializeField]
    private MapFile _songData;

    public InGameResources inGameResources {
        get {
            return _inGameResources;
        }

        set {
            _inGameResources = value;
        }
    }

    public MapFile songData{
        get {
            return _songData; 
        }

        set {
            _songData = value;
        }        
    }

    private void Awake(){ 
        touchManager = gameObject.GetComponent<TouchManager>();
        widgetViewer = gameObject.GetComponent<WidgetViewer>();        
    }
}
