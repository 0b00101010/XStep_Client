using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NodeHitEffect : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private SpriteRenderer[] rectangleImages;

    [SerializeField]
    private SpriteRenderer circleImage;

    private Color defaultColor;

    private Coroutine executeCoroutine;

    private List<Tween> objectTweens = new List<Tween>();

    private Vector3 rightRotateVector;
    private Vector3 leftRotateVector;

    private Vector3 sizeUpScale;

    private bool isTweening;

    private void Awake(){
        isTweening = false;

        leftRotateVector = Vector3.back * 430;
        rightRotateVector = Vector3.forward * 390;

        sizeUpScale = Vector3.one * 1.2f;

        defaultColor = Color.white;
        defaultColor.a = 0;

    }

    public void Execute(){
        gameObject.SetActive(true);

        if(isTweening){
            ObjectReset();
            StopCoroutine(executeCoroutine);
            executeCoroutine = StartCoroutine(ExecuteCoroutine());
            return;
        }

        executeCoroutine = StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        isTweening = true;

        objectTweens.Add(circleImage.DOFade(1.0f, 0.25f));

        for(int i = 0; i < rectangleImages.Length; i++){
            rectangleImages[i].DOFade(1.0f, 0.25f);
            if(i % 2 == 0){
                objectTweens.Add(rectangleImages[i].gameObject.transform.DORotate(leftRotateVector, 0.35f, RotateMode.FastBeyond360));
            } else {
                objectTweens.Add(rectangleImages[i].gameObject.transform.DORotate(rightRotateVector, 0.25f, RotateMode.FastBeyond360));
            }
        }

        objectTweens.Add(circleImage.gameObject.transform.DOScale(sizeUpScale, 0.35f));

        yield return YieldInstructionCache.WaitingSeconds(0.25f);
        
        for(int i = 0; i < rectangleImages.Length; i++){
            objectTweens.Add(rectangleImages[i].DOFade(0, 0.25f));
        }
            
        objectTweens.Add(circleImage.DOFade(0, 0.25f));


        yield return YieldInstructionCache.WaitingSeconds(0.5f);
        ObjectOff();
    }

    private void ObjectReset(){
        objectTweens.ForEach((tween) => {
            tween.Kill();
        });
        
        for(int i = 0; i < rectangleImages.Length; i++){
            rectangleImages[i].gameObject.transform.rotation = Quaternion.Euler(0,0,0);
            rectangleImages[i].color = defaultColor;
        }

        circleImage.gameObject.transform.localScale = Vector3.one;
        circleImage.color = defaultColor;


        // FIXME : 계속 초기화 하는거 별로 안 좋아 보임
        objectTweens.Clear();
    }

    private void ObjectOff(){
        gameObject.SetActive(false);
        ObjectReset();
        isTweening = false;
    }
}
