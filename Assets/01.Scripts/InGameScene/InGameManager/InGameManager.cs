using System.Collections;
using System.Collections.Generic;
using EventTools.Event;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    
    public static InGameManager instance;
    
    [HideInInspector]
    public ScoreManager scoreManager;

    [HideInInspector]
    public NodeCreator nodeCreator;

    [HideInInspector]
    public CenterEffectorController centerEffectorController;
    
    [Header("Events")]
    [SerializeField]
    private UniEvent<Color,Color,float> backgroundColorChangeEvent;

    
    private void Awake(){
        if(instance is null){
            instance = this;
        }

        scoreManager = gameObject.GetComponent<ScoreManager>();
        nodeCreator = gameObject.GetComponent<NodeCreator>();
        centerEffectorController = gameObject.GetComponent<CenterEffectorController>();
    }

    private void Start(){
        GameManager.instance.touchManager.touchType = TouchType.InGame;
    }

    public void ChangeBackgroundColor(Color topColor, Color bottomColor, float duration) {
        backgroundColorChangeEvent.Invoke(topColor,bottomColor,duration);
    }    


}
