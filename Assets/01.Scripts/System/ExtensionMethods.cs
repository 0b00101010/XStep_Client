using UnityEngine;
using System.Collections;
public static class ExtensionMethods{
    public static IEnumerator Start(this IEnumerator coroutine, MonoBehaviour behaviour){
        behaviour.StartCoroutine(coroutine);
        return coroutine;
    }

    public static void Stop(this IEnumerator coroutine, MonoBehaviour behaviour){
        behaviour.StopCoroutine(coroutine);
    }

    public static void Log(this string value){
        Debug.Log(value);
    }

    public static Vector2 Direction(this Vector2 start, Vector2 end){
        return (end - start).normalized;
    }
}