using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake(){ 
        if(instance is null){
            instance = this;
        }

        DontDestroyOnLoad(this);
    }
}
