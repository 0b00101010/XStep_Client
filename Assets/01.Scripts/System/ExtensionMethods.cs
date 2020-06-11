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
}