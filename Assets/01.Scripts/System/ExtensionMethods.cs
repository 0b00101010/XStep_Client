using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethods{
    public static IEnumerator Start(this IEnumerator coroutine, MonoBehaviour behaviour){
        behaviour.StartCoroutine(coroutine);
        return coroutine;
    }

    public static void Stop(this IEnumerator coroutine, MonoBehaviour behaviour){
        behaviour.StopCoroutine(coroutine);
    }

    public static void Log(this object value){
        #if UNITY_EDITOR
        Debug.Log(value.ToString());
        #endif
    }

    public static Vector2 Direction(this Vector2 start, Vector2 end){
        return (end - start).normalized;
    }

    public static int[] IndexOfMany(this string value, char findValue){
        var findPosition = new List<int>();

        for(int i = 0; i < value.Length; i++){
            if(value[i].Equals(findValue)){
                findPosition.Add(i);
            }
        }

        return findPosition.ToArray(); 
    }
}