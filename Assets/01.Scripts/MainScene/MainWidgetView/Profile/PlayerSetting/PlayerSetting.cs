using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "PlayerSetting", menuName = "Scriptable Object/PlayerSetting", order = 0)]
public class PlayerSetting : ScriptableObject {
    [SerializeField]
    private ProfileTitleData titleData;
    public ProfileTitleData TitleData => titleData;
    
    [SerializeField]
    private AchieveData achieveData;
    public AchieveData AchieveData => achieveData;

    [SerializeField]
    private AchieveRequireData achieveRequireData; // 외부에서는 얘에만 접근해서 값 변경해주면 되는 구조
    public AchieveRequireData AchieveRequireData => achieveRequireData;
    
    [SerializeField]
    [Dropdown("titles")]
    private Title _title;
    public Title title {get => _title; set => _title = value;}

    private int guestNumber = 0;
    public int GuestNumber {
        get => guestNumber;
        set => guestNumber = value;
    }

    private DropdownList<Title> titles(){
        if(titleData is null){
            return null;
        }

        var titleDropdownList = new DropdownList<Title>();
        
        foreach(var item in titleData.TitleResources){
            if (item.isUnLock) {
                titleDropdownList.Add(item.title, item);
            }
        }

        return titleDropdownList;
    }

    [SerializeField]
    private string _userName;
    public string userName {get => _userName; set => _userName = value;}

    [SerializeField]
    private Sprite _profileSprite;
    public Sprite profileSprite {get => _profileSprite; set => _profileSprite = value;}

    [Space(10)]
    [SerializeField]
    private int _currentLevel;

    public int currentLevel {
        get => _currentLevel;
        set {
            if ((value < 50) == false) {
                return;
            }

            _currentLevel = value;
            GameManager.instance.PlayerSetting.AchieveRequireData.SetValueToRequire("LV", value);
        }
    }

    [SerializeField]
    private int _currentExp;
    public int currentExp {
        get => _currentExp;
        set {
            _currentExp = value;
            if (_currentExp > _levelUpExp) {
                currentLevel++;
                _currentExp = 0;
                _levelUpExp = 50 + (currentLevel * currentLevel / 2 * 100);
            }
        }
    }

    [SerializeField]
    private int _levelUpExp;
    public int levelUpExp {get => _levelUpExp; set => _levelUpExp = value;}

    [Space(10)]
    [SerializeField]
    private ulong _totalScore;
    public ulong totalScore {get => _totalScore; set => _totalScore = value;}
    
    [SerializeField]
    private int _highClearLevel;
    public int highClearLevel {get => _highClearLevel; set => _highClearLevel = value;}

    [Space(10)]
    [SerializeField]
    private int _perfectPlay;

    public int perfectPlay {
        get => _perfectPlay;
        set {
            _perfectPlay = value; 
            GameManager.instance.PlayerSetting.AchieveRequireData.AddValueToRequire("PerfectPlay", 1);
        }
    }
    
    [SerializeField]
    private int _challengeClear;
    public int challengeClear {get => _challengeClear; set => _challengeClear = value;}

    [SerializeField]
    private int _freeStyleClear;
    public int freeStyleClear {get => _freeStyleClear; set => _freeStyleClear = value;}
}
