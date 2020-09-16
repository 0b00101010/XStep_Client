using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public static class JsonConverter<T>
{
    public static string ObjectToJson(T objectValue) {
        return JsonMapper.ToJson(objectValue);
    }
    
    public static T JsonToObject(string json){
        var item = JsonMapper.ToObject(json);
        return JsonUtility.FromJson<T>(json);
    }
}
