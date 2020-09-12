using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlideNodeHitEffect : MonoBehaviour {
    [Header("Values")]
    [SerializeField]
    private float sizeUpDuration;

    [SerializeField]
    private float fadeInDuration;

    [SerializeField]
    private float fadeOutDuration;

    [SerializeField]
    private float fadeAfterWaitingTime;
    
    [Header("Objects")]
    [SerializeField]
    private SpriteRenderer[] rhombusObjects;
    
    private List<SpriteRenderer> rhombusLists = new List<SpriteRenderer>();

    private List<Tween> objectTweens = new List<Tween>();
    private IEnumerator executeCoroutine;

    private Color transparent;
    private Vector2 defaultScale;

    private bool isTweening;

    private void Awake(){
        transparent = Color.white;
        transparent.a = 0;

        defaultScale = Vector2.zero;
    }

    public void Execute(int reversed){
        gameObject.SetActive(true);

        if(reversed != 0){
            rhombusLists = rhombusObjects.Reverse().ToList();
        } else {
            rhombusLists = rhombusObjects.ToList();
        }

        if(isTweening){
            ObjectReset();
            executeCoroutine.Stop(this);
        }
        
        executeCoroutine = ExecuteCoroutine().Start(this);

    }

    private IEnumerator ExecuteCoroutine(){
        isTweening = true;
        for(int i = 0; i < rhombusLists.Count; i++){
            TweenCoroutine(rhombusLists[i]).Start(this);
            objectTweens.Add(rhombusLists[i].gameObject.transform.DOScale(1,sizeUpDuration));

            yield return YieldInstructionCache.WaitingSeconds(0.03f);
        }

        yield return objectTweens[objectTweens.Count-1].WaitForCompletion();
        yield return YieldInstructionCache.WaitingSeconds(fadeAfterWaitingTime);
        
        ObjectReset();
        ObjectOff();
    }

    private IEnumerator TweenCoroutine(SpriteRenderer spriteRenderer){
        objectTweens.Add(spriteRenderer.DOFade(1,fadeInDuration));
        yield return objectTweens[objectTweens.Count-1].WaitForCompletion();
        objectTweens.Add(spriteRenderer.DOFade(0,fadeOutDuration));
    }

    private void ObjectReset(){
        for(int i = 0; i < objectTweens.Count; i++){
            objectTweens[i].Kill();
        }

        for(int i = 0; i < rhombusObjects.Length; i++){
            rhombusObjects[i].color = transparent;
            rhombusObjects[i].gameObject.transform.localScale = defaultScale;
        }   
    }

    private void ObjectOff(){
        isTweening = false;
        gameObject.SetActive(false);
    }
}
