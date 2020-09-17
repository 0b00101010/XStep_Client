using System;
using UnityEngine;

[Serializable]
public class UserLoginData {
    public string id;
    public string passwd;
}

[Serializable]
public class UserData {
    public string id;
    public string name;
    public string token;
    public string finalNickChanged;
}