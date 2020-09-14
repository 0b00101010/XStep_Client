using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "AchieveData", menuName = "Scriptable Object/AchieveData", order = 0)]
public class AchieveData : ScriptableObject {
    [SerializeField]
    private Achieve[] achieves;
    public Achieve[] Achieves => achieves;

    public void Unlock(string achieveName, int amount) {
        Achieve findAchieve = null;
        
        foreach (var achieve in achieves) {
            if (findAchieve != null) {
                break;
            }
            findAchieve = achieve.AchieveName.Equals(achieveName) ? achieve : null;
        }
        
        if (findAchieve.IsUnlock == false) {
            if (amount < findAchieve.Amount) {
                return;
            }
            
            findAchieve.IsUnlock = true;
            GameManager.instance.PlayerSetting.currentExp += findAchieve.RewardXp;
            GameManager.instance.PlayerSetting.TitleData.UnLockTitle(findAchieve.OpenTitleName);
        }
    }

    [Button("Reset")]
    public void Reset() {
        foreach (var achieve in achieves) {
            achieve.IsUnlock = false;
        }
    }
}
