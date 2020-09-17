using System;
using UnityEngine;

/// <summary>
/// Convert this class to Singleton format.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T _instance;
    public static T instance {
        get {
            if(_instance == null){
                var obj = GameObject.FindObjectOfType<T>();
                
                if(obj == null){
                    var newObject = new GameObject(typeof(T).ToString());
                    obj = newObject.AddComponent<T>();
                }

                _instance = obj;
            }
            
            return _instance;
        }
    }

    public virtual void OnDestroy(){
        _instance = null;
    }
}