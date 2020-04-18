using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YieldInstructionCache
{
    private static Dictionary<float, WaitForSeconds> waitingSeconds = new Dictionary<float, WaitForSeconds>();
    private static Dictionary<float, WaitForSecondsRealtime> waitingRealTime = new Dictionary<float, WaitForSecondsRealtime>();
    private static object waitFrame = null;

    public static object WaitFrame => waitFrame;

    public static WaitForSeconds WaitingSeconds(float waitingTime){
        if(!waitingSeconds.ContainsKey(waitingTime)){
            waitingSeconds.Add(waitingTime, new WaitForSeconds(waitingTime));
        }

        return waitingSeconds[waitingTime];
    }

    public static WaitForSecondsRealtime WaitingRealTime(float waitingTime){
        if(!waitingRealTime.ContainsKey(waitingTime)){
            waitingRealTime.Add(waitingTime, new WaitForSecondsRealtime(waitingTime));
        }

        return waitingRealTime[waitingTime];
    }
}
