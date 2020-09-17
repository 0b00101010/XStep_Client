using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Convert this class to Singleton format.
/// And keep the object from being destroyed.
/// </summary>
public class DontDestroySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T instance {
        get {
            if (_instance == null) {
                var obj = GameObject.FindObjectOfType<T>();

                if (obj == null) {
                    var newGameObject = new GameObject(typeof(T).ToString());
                    obj = newGameObject.AddComponent<T>();
                }
                
                _instance = obj;
            
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

}