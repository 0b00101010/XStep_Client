using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public static class JsonConverter
{
    private static string jsonString;
    private static JsonData jsonData;

    public static void JsonGet(){
        // TODO : Load json data file from server.
        jsonData = JsonMapper.ToObject(jsonString);
    }

    public static string GetData(string keyValue){
        return jsonData[keyValue].ToString();
    }
}
