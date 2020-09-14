using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    private int comboCount;
    private int totalScore;
    public int TotalScore => totalScore;
    
    private int[] judgeCountArray = new int[5]{0,0,0,0,0};

    [Header("Objects")]
    [SerializeField]
    private Image progressImage;
    
    [SerializeField]
    private Image judgeImage;

    [SerializeField]
    private NormalNodeHitEffect[] normalNodeHitEffects;

    [SerializeField]
    private SlideNodeHitEffect[] slideNodeHitEffects;

    [Header("Resources")]
    [SerializeField]
    private Sprite[] judgeSprites;


    [Header("Functions")]
    [SerializeField]
    private Event<int> numberConversionEvent;

    [Header("Judge Effect")]

    [SerializeField]
    private float sizeUpValue;

    [SerializeField]
    private float sizeUpDuration;

    [SerializeField]
    private Ease easeType;

    private Vector3 sizeUpVector;
    
    private Coroutine judgeImageSizeUpCoroutine;

    private Tween sizeUpTween;

    private void Awake(){
        sizeUpVector = Vector3.one * sizeUpValue;
    }

    public void AddScore(int judgeLevel){
        if(judgeLevel.Equals(0) || judgeLevel.Equals(1)){
            comboCount = 0;
        } else {
            comboCount++;
        }
        
        numberConversionEvent.Invoke(this.comboCount);
        judgeImage.sprite = judgeSprites[judgeSprites.Length - 1 - judgeLevel];

        switch (judgeLevel) {
            case 1:
                totalScore += 400 + (comboCount / 100);
                break;
            case 2:
                totalScore += 600 + (comboCount / 100);
                break;
            case 3:
                totalScore += 800 + (comboCount / 100);
                break;
            case 4:
                totalScore += 1000 + (comboCount / 100); 
                break;
        }
        
        if(judgeImageSizeUpCoroutine != null){
            StopCoroutine(judgeImageSizeUpCoroutine);
        }

        GameManager.instance.gameResult.JudgeCounts[judgeLevel]++;
        GameManager.instance.gameResult.NodeTotalCount++;
        
        sizeUpTween?.Kill();
        judgeImageSizeUpCoroutine = StartCoroutine(JudgeImageSizeUpCoroutine());

    }
    
    private IEnumerator JudgeImageSizeUpCoroutine(){
        judgeImage.gameObject.SetActive(true);
        judgeImage.gameObject.transform.localScale = Vector3.one;        
        
        sizeUpTween = judgeImage.gameObject.transform.DOScale(sizeUpVector * sizeUpValue, sizeUpDuration).SetEase(easeType);
        yield return sizeUpTween.WaitForCompletion();

        yield return YieldInstructionCache.WaitingSeconds(1.0f);
        judgeImage.gameObject.SetActive(false);
    }

    public void SongProgressChange(double progress) {
        progressImage.fillAmount = (float)progress;
    }

    public void NormalNodeExecuteEffect(int index){
        normalNodeHitEffects[index].Execute();
    }

    public void SlideNodeExecuteEffect(int index, int reversed){
        slideNodeHitEffects[index].Execute(reversed);
    }

}
