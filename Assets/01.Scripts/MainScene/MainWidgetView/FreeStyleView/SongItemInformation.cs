using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Song Item Information", menuName = "Scriptable Object/Song Item Information", order = int.MinValue)]
public class SongItemInformation : ScriptableObject
{
    [SerializeField]
    private Sprite eyeCatch;
    
    [SerializeField]
    private string songName;

    [SerializeField]
    private string composerName;

    [SerializeField]
    private int[] stepTags;

    [SerializeField]
    private int[] difficultys = new int[5];

    [SerializeField]
    private MapFile mapFile;
    
    public Sprite EyeCatch => eyeCatch; 
    public string SongName => songName; 
    public string ComposerName => composerName; 
    public int[] StepTags => stepTags; 
    public int[] Difficultys => difficultys;

    public MapFile MapFile => mapFile;
}
