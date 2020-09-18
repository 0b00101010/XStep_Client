using System;
using System.IO;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Networking;

public class ServerConnectorController : MonoBehaviour {
    [SerializeField]
    private string serverUrl;
    public string ServerUrl => serverUrl;

    private LoginConnector loginConnector;
    public LoginConnector LoginConnector => loginConnector;
    
    private SignUpConnector signUpConnector;
    public SignUpConnector SignUpConnector => signUpConnector;
    
    private void Awake() {
        loginConnector = gameObject.GetComponent<LoginConnector>();
        signUpConnector = gameObject.GetComponent<SignUpConnector>();
    }

    public void SignUp(string id, string passwd, string name, Action failedCallback, params Action[] callbacks) {
        for (int i = 0; i < callbacks.Length; i++) {
            signUpConnector.Callbacks.Add(callbacks[i]);
        }
        
        signUpConnector.FailedCallback = failedCallback;
        signUpConnector.OnRequest(id, passwd, name);
    }

    public void Login(string id, string passwd, Action failedCallback, params Action[] callbacks) {
        for (int i = 0; i < callbacks.Length; i++) {
            loginConnector.Callbacks.Add(callbacks[i]);
        }

        loginConnector.FailedCallback = failedCallback;
        loginConnector.OnRequest(id, passwd);
    }
}
