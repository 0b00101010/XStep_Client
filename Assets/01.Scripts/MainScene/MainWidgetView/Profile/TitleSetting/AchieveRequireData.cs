using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchieveRequire", menuName = "Scriptable Object/Achieve Require", order = 0)]
public class AchieveRequireData : ScriptableObject {
    [SerializeField]
    private AchieveRequire[] achieveRequires;
    private Dictionary<string, AchieveRequire> achieveDictionary = new Dictionary<string, AchieveRequire>();

    public void Initialize() {
        for (int i = 0; i < achieveRequires.Length; i++) {
            if (achieveDictionary.ContainsKey(achieveRequires[i].RequireName) == false) {
                achieveDictionary.Add(achieveRequires[i].RequireName, achieveRequires[i]);
            }            
        }
    }

    public void AddValueToRequire(string require, int value) {
        if (achieveDictionary.ContainsKey(require) == false) {
            throw new KeyNotFoundException();
        }

        achieveDictionary[require].Amount += value;
    }
}
