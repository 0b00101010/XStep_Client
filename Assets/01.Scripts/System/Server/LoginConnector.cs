using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine;
public class LoginConnector : ServerConnector, IServerConnector {
    private UserData userData;
    
    public void OnRequest(params object[] args) {
        if (args.Length != 2) {
            throw new ArgumentException();
        }
        
        Login(args[0].ToString(), args[1].ToString()).Start(this);
    }
    
    private IEnumerator Login(string id, string passwd) {
        var parameters = new WWWForm();
        
        parameters.AddField("id", id);
        parameters.AddField("passwd", passwd);

        using (var www = UnityWebRequest.Post($"{GameManager.instance.ServerUrl}/signin", parameters)) {
            yield return www.SendWebRequest();
            
            if (www.responseCode == 404) {
                RequestSuccess = false;
                "User를 찾을 수 없거나 ID 혹은 Password가 잘못되었습니다.".Log();
                FailedCallback?.Invoke();
            }
            else {
                var request = www.downloadHandler.text;
                userData = JsonUtility.FromJson<UserData>(request);
            }

            foreach (var CallBack in Callbacks) {
                CallBack?.Invoke();
            }
        }
    }

    public UserData GetUserData() {
        return userData;
    }
}
