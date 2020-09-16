using System;
using UnityEngine;

[Serializable]
public class UserLoginData {
    private string _id;
    public string id {
        get => _id;
        set => _id = value;
    }

    private string _passwd;
    public string passwd {
        get => _passwd;
        set => _passwd = value;
    }
}

[Serializable]
public class UserData {
    private string _id;
    public string id {
        get => _id;
        set => _id = value;
    }

    private string _token;
    public string token {
        get => _token;
        set => _token = value;
    }

    private string _nickName;
    public string nickName {
        get => _nickName;
        set => _nickName = value;
    }
}

[Serializable]
public class GameFinishData {
    private string _musicName;
    public string musicName {
        get => _musicName;
        set => _musicName = value;
    }

    
    private string _recordTotal;
    public string recordTotal {
        get => _recordTotal;
        set => _recordTotal = value;
    }

    private string _perPlay;
    public string perPlay {
        get => _perPlay;
        set => _perPlay = value;
    }

    private string _playedUser;
    public string playedUser {
        get => _playedUser;
        set => _playedUser = value;
    }

    private string _musicToken;
    public string musicToken {
        get => _musicToken;
        set => _musicToken = value;
    }

    private string _userToken;
    public string userToken {
        get => _userToken;
        set => _userToken = value;
    }
}