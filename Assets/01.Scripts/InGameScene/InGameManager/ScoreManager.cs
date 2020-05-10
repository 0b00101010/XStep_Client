using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private float defaultHP;

    private float hp;
    private int score;

    [Header("Objects")]
    [SerializeField]
    private Image hpImage;
    
    [SerializeField]
    private Image judgeImage;

    [Header("Resources")]
    [SerializeField]
    private Sprite[] judgeImages;


    [Header("Functions")]
    [SerializeField]
    private INTEvent numberConversionEvent;

    public void AddScore(int score){
        this.score += score;
        numberConversionEvent.Invoke(this.score);
    }

    public void RedutionHP(float value){
        hp -= value;
        hpImage.fillAmount = hp / defaultHP;
    }
}
