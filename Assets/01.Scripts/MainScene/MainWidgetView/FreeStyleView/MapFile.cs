using UnityEngine;
using System;
using Packages.Rider.Editor.UnitTesting;

[Serializable]
public class Map {
    [SerializeField]
    private TextAsset _map;
    public TextAsset map => _map;

    [SerializeField]
    private int _difficulty;
    public int difficulty => _difficulty;
}

[CreateAssetMenu(fileName = "MapFile", menuName = "Scriptable Object/Map File", order = 0)]
public class MapFile : ScriptableObject {
    [SerializeField]
    private Map[] _maps;
    public Map[] maps => _maps;
    
    [SerializeField]
    private TextAsset _backgroundFile;

    public TextAsset backgroundFile => _backgroundFile;
    
    [SerializeField]
    private AudioClip _clip;

    public AudioClip clip => _clip;

    [SerializeField]
    private int _currentSelectDifficulty;

    public int currentSelectDifficulty {
        get => _currentSelectDifficulty;
        set => _currentSelectDifficulty = value;
    }
}