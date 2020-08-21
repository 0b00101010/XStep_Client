using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlayerSetting", menuName = "Scriptable Object/PlayerSetting", order = 0)]
public class PlayerSetting : ScriptableObject {
    [SerializeField]
    private Title _title;
    public Title title {get => _title; set => _title = value;}

    [SerializeField]
    private string _userName;
    public string userName {get => _userName; set => _userName = value;}

    [SerializeField]
    private Sprite _profileSprite;
    public Sprite profileSprite {get => _profileSprite; set => _profileSprite = value;}

    [Space(10)]
    [SerializeField]
    private int _currentLevel;
    public int currentLevel {get => _currentLevel; set => _currentLevel = value;} 

    [SerializeField]
    private int _currentExp;
    public int currentExp {get => _currentExp; set => _currentExp = value;}

    [SerializeField]
    private int _levelUpExp;
    public int levelUpExp {get => _levelUpExp; set => _levelUpExp = value;}

    [Space(10)]
    [SerializeField]
    private ulong _totalScore;
    public ulong totalScore {get => _totalScore; set => _totalScore = value;}
    
    // FIXME : score level로 바꾸기
    [SerializeField]
    private int _highClearScore;
    public int highClearScore {get => _highClearScore; set => _highClearScore = value;}

    [Space(10)]
    [SerializeField]
    private int _perfectPlay;
    public int perfectPlay {get => _perfectPlay; set => _perfectPlay = value;}
    
    [SerializeField]
    private int _challengeClear;
    public int challengeClear {get => _challengeClear; set => _challengeClear = value;}

    [SerializeField]
    private int _freeStyleClear;
    public int freeStyleClera {get => _freeStyleClear; set => _freeStyleClear = value;}
}
