using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extension Method is a method that can be used in a particular data type.
/// </summary>
public static class SystemExtensions{
    public static void Log(this object value){
        #if UNITY_EDITOR
        Debug.Log(value.ToString());
        #endif
    }

    /// <summary>
    /// It automatically stops when you convert the scene.
    /// </summary>
    public static IEnumerator Start(this IEnumerator coroutine, MonoBehaviour behaviour){
        behaviour.StartCoroutine(coroutine);
        return coroutine;
    }

    public static void Stop(this IEnumerator coroutine, MonoBehaviour behaviour){
        behaviour.StopCoroutine(coroutine);
    }
}