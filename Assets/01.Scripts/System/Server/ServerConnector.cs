using System;
using UnityEngine;
using System.Collections.Generic;

public class ServerConnector : MonoBehaviour {
    private List<Action> callbacks = new List<Action>();
    public List<Action> Callbacks => callbacks;

    private bool requestSuccess;
    public bool RequestSuccess {
        get => requestSuccess;
        set => requestSuccess = value;
    }
}