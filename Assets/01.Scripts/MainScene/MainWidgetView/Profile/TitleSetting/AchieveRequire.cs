using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AchieveRequire {
    [SerializeField]
    private string requireName;
    public string RequireName => requireName;
    
    [SerializeField]
    private int amount;
    public int Amount {
        get => amount;
        set {
            amount = value;
            GameManager.instance.PlayerSetting.AchieveData.Unlock(requireName, amount);
        }
    }
}
