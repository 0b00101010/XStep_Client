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
    private Sprite[] judgeSprites;


    [Header("Functions")]
    [SerializeField]
    private INTEvent numberConversionEvent;

    private Vector3 sizeUpVector;
    
    private Coroutine judgeImageSizeUpCoroutine;

    private void Awake(){
        sizeUpVector = Vector3.one * 0.7f;
        sizeUpVector /= 30;
    }

    public void AddScore(int score, int judgeLevel){
        this.score += score;
        numberConversionEvent.Invoke(this.score);
        judgeImage.sprite = judgeSprites[judgeSprites.Length - 1 - judgeLevel];
        
        if(judgeImageSizeUpCoroutine != null){
            StopCoroutine(judgeImageSizeUpCoroutine);
        }

        judgeImageSizeUpCoroutine = StartCoroutine(JudgeImageSizeUpCoroutine());

    }

    public void RedutionHP(float value){
        hp -= value;
        hpImage.fillAmount = hp / defaultHP;
    }

    private IEnumerator JudgeImageSizeUpCoroutine(){
        judgeImage.gameObject.SetActive(true);
        judgeImage.gameObject.transform.localScale = Vector3.one;        
        for(int i = 0; i < 10; i++){
            judgeImage.gameObject.transform.localScale += sizeUpVector;
            yield return YieldInstructionCache.WaitFrame;
        }

        yield return YieldInstructionCache.WaitingSeconds(1.0f);
        judgeImage.gameObject.SetActive(false);
    }
}
