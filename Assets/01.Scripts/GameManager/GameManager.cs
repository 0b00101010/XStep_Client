using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public TouchManager touchManager;

    private void Awake(){ 
        if(instance is null){
            instance = this;
        }
        
        touchManager = gameObject.GetComponent<TouchManager>();
        DontDestroyOnLoad(this);
    }
}
