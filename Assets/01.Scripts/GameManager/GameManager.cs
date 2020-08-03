using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance {
        get{
            if(_instance == null){
                var obj = GameObject.FindObjectOfType<GameManager>();

                if(obj == null){
                    var newGameManager = new GameObject(nameof(GameManager));
                    obj = newGameManager.AddComponent<GameManager>();
                }
                
                _instance = obj;
                DontDestroyOnLoad(obj.gameObject);
            }

            return _instance;
        }
    }

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
