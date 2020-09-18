using System;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SignUpConnector : ServerConnector, IServerConnector {
    private UserLoginData userData;
    
    public void OnRequest(params object[] args) {
        if (args.Length != 3) {
            throw new ArgumentException();
        }

        SignUp(args[0].ToString(), args[1].ToString(), args[2].ToString()).Start(this);
    }

    public IEnumerator SignUp(string id, string passwd, string name) {
        var parameters = new WWWForm();

        parameters.AddField("id", id);
        parameters.AddField("passwd", passwd);
        parameters.AddField("name", name);

        using (var www = UnityWebRequest.Post($"{GameManager.instance.ServerUrl}/signup", parameters)) {
            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError) {
                www.error.Log();
                throw new NetworkInformationException();
            }

            if (www.responseCode == 409) {
                RequestSuccess = false;
                "중복 되는 유저명 입니다.".Log();
            }

            foreach (var CallBack in Callbacks) {
                CallBack?.Invoke();
            }
        }
        
        PlayerPrefs.SetString("SIGNUP", "true");
    }
}
