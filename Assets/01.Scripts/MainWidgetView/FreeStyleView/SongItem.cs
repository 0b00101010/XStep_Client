using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

[ExecuteInEditMode]
public class SongItem : MonoBehaviour
{

    [SerializeField]
    private SongItemInformation songItemInformation;

    [SerializeField]
    private Image eyeCatchImage;

    [SerializeField]
    private Text songName;

    [SerializeField]
    private Text composerName;

    [SerializeField]
    private GameObject[] stepTags;

    [SerializeField]
    private Text[] difficultyTexts;
    
    private void Awake(){

        if(songItemInformation is null)
            return;
            
        eyeCatchImage.sprite = songItemInformation.EyeCatch;
        songName.text = songItemInformation.SongName;
        composerName.text = songItemInformation.ComposerName;

        for(int i = 0; i < songItemInformation.StepTags.Length; i++){
            stepTags[i].SetActive(true);
        }

        for(int i = 0; i < songItemInformation.Difficultys.Length; i++){
            difficultyTexts[i].text = songItemInformation.Difficultys[i].ToString();
        }

    }
}
