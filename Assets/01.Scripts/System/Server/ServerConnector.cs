using System;
using System.IO;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerConnector : MonoBehaviour {
    [SerializeField]
    private string serverUrl;

    public bool SignIn() {
        return false;
    }

    public UserData Login(string id, string password) {
        try {
            var userData = new UserData();
            
            var loginData = new UserLoginData();
            loginData.id = id;
            loginData.passwd = password;

            var requestJson = JsonConverter<UserLoginData>.ObjectToJson(loginData);
            var bytes = System.Text.Encoding.UTF8.GetBytes(requestJson);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://{serverUrl}/signin");

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = bytes.Length;
            
            using (var stream = request.GetRequestStream()) {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using(StreamReader reader = new StreamReader(response.GetResponseStream())){
                string responseJson = reader.ReadToEnd();
                userData = JsonConverter<UserData>.JsonToObject(responseJson);
            }
            
            return userData;
        }
        catch(Exception e) {
            e.Log();
            return null;
        }
    }


}
