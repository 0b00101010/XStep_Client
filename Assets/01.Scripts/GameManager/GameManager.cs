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

    [SerializeField]
    private SongItemInformation _selectSongItem;

    private GameResult _gameResult;

    public GameResult gameResult {
        get => _gameResult;
        set => _gameResult = value;
    }
    
    private PlayerSetting playerSetting;
    public PlayerSetting PlayerSetting => playerSetting;
    
    private bool someUIInteraction;
    public bool SomeUIInteraction {get => someUIInteraction; set => someUIInteraction = value;}
    
    public InGameResources inGameResources {
        get => _inGameResources;
        set => _inGameResources = value;
    }

    public MapFile songData {
        get => _songData;
        set => _songData = value;
    }
    
    public SongItemInformation selectSongItem {
        get => _selectSongItem;
        set => _selectSongItem = value;
    }
    

    private void Awake(){
        if (instance != this && instance != null) {
            Destroy(gameObject);
        }
        
        touchManager = gameObject.GetComponent<TouchManager>();
        widgetViewer = gameObject.GetComponent<WidgetViewer>();   

        playerSetting = Resources.Load<PlayerSetting>("Player Setting/PlayerSetting");
    }
}
