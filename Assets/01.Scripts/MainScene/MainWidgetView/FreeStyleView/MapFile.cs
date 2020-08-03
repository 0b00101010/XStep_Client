using UnityEngine;

[CreateAssetMenu(fileName = "MapFile", menuName = "Scriptable Object/Map File", order = 0)]
public class MapFile : ScriptableObject {
    [SerializeField]
    private TextAsset _mapFile;
    public TextAsset  mapFile => _mapFile;

    [SerializeField]
    private AudioClip _clip;

    public AudioClip clip => _clip;
}