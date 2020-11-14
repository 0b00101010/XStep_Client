using System;
using UnityEngine;

[Serializable]
public class Achieve {
    [SerializeField]
    private string achieveName;
    public string AchieveName => achieveName;
    
    [SerializeField]
    private string achieveDescription;
    public string AchieveDescription => achieveDescription;
    
    [SerializeField]
    private string require;
    public string Require => require;
    
    [SerializeField]
    private int amount;
    public int Amount => amount;
    
    [SerializeField]
    private int rewardMusicPoint;
    public int RewardMusicPoint => rewardMusicPoint;
    
    [SerializeField]
    private int rewardXp;
    public int RewardXp => rewardXp;

    [SerializeField]
    private string openTitleName;
    public string OpenTitleName => openTitleName;

    [SerializeField]
    private bool isUnlock;
    public bool IsUnlock {
        get => isUnlock;
        set {
            isUnlock = value;
            
            if (isUnlock) {
                GameManager.Instance.UnlockAchieves.Add(this);
                (AchieveName + "Unlock").Log();
            }

        }
    }
}