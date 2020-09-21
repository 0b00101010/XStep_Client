using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ServerIP", menuName = "Scriptable Object/ServerIP", order = 0)]
public class ServerIP : ScriptableObject {
    [SerializeField]
    private string _serverIP;
    public string serverIP => _serverIP;
}
