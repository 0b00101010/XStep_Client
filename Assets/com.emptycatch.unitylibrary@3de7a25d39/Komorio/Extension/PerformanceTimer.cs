using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Stopwatch = System.Diagnostics.Stopwatch;

public static class PerformanceTimer{
    private static Dictionary<MonoBehaviour, Stopwatch> stopwatchDictionary = new Dictionary<MonoBehaviour, Stopwatch>();
    
    public static void TimerStart(MonoBehaviour behaviour){ 
        if(!stopwatchDictionary.ContainsKey(behaviour)){
            stopwatchDictionary.Add(behaviour, new Stopwatch());
        }

        stopwatchDictionary[behaviour].Start();
    }

    public static void TimerStop(MonoBehaviour behaviour){
        if(!stopwatchDictionary.ContainsKey(behaviour)){
           throw new KeyNotFoundException();
        }

        stopwatchDictionary[behaviour].Stop();
        $"{stopwatchDictionary[behaviour].ElapsedMilliseconds / 1000.0f}ms".Log();
        
        stopwatchDictionary.Remove(behaviour);
    }
}