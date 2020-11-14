using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using EventTools.Event;
using TMPro;

public class TitleViewController : MainUIObject {
    private Tween logoScaleTween;
    private Tween backgroundScaleTween;
    private Tween textFadeTween;

    private Color textDefaultColor;
    private Color backgroundColor;
    
    [SerializeField]
    private Image logoImage;

    [SerializeField]
    private Image backgroundImage;
    
    [SerializeField]
    private TextMeshProUGUI touchToStartText;
    
    private IEnumerator enableCoroutine;
    private IEnumerator updateCoroutine;

    private void Awake() {
        textDefaultColor = touchToStartText.color;
        backgroundColor = backgroundImage.color;
    }

    private void Start() {
        if (GameManager.Instance.IsSawTitle) {
            gameObject.SetActive(false);
            MainSceneManager.Instance.uiController.TitleViewClose();
        }
    }
    
    private void OnEnable() {
        backgroundImage.color = backgroundColor;
        
        logoImage.gameObject.transform.localScale = Vector3.zero;
        backgroundImage.gameObject.transform.localScale = new Vector3(1,0,1);
        
        var newColor = textDefaultColor;
        newColor.a = 0;
        
        touchToStartText.color = newColor;

        enableCoroutine = EnableCoroutine().Start(this);
    }

    private IEnumerator EnableCoroutine() {
        logoScaleTween = logoImage.gameObject.transform.DOScale(Vector3.one, 0.25f);
        yield return logoScaleTween.WaitForCompletion();

        backgroundScaleTween = backgroundImage.gameObject.transform.DOScale(Vector3.one, 0.25f);
        yield return backgroundScaleTween.WaitForCompletion();

        updateCoroutine = UpdateCoroutine().Start(this);
    }

    public override void Execute() {
        enableCoroutine?.Stop(this);
        updateCoroutine?.Stop(this);

        DisableCoroutine().Start(this);
    }
    
    private IEnumerator UpdateCoroutine() {
        while (true) {
            textFadeTween = touchToStartText.DOFade(1, 0.75f);
            yield return textFadeTween.WaitForCompletion();
             
            textFadeTween = touchToStartText.DOFade(0, 0.75f);
            yield return textFadeTween.WaitForCompletion();
        }
    }

    private IEnumerator DisableCoroutine() {

        backgroundImage.DOColor(Color.white, 0.25f);
        textFadeTween = touchToStartText.DOFade(0, 0.25f);
        yield return textFadeTween.WaitForCompletion();

        logoScaleTween = logoImage.gameObject.transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 0.15f);
        backgroundScaleTween = backgroundImage.gameObject.transform.DOScale(new Vector3(1,0,1), 0.15f);
        yield return backgroundScaleTween.WaitForCompletion();
    
        logoScaleTween = logoImage.gameObject.transform.DOScale(Vector3.zero, 0.25f);
        yield return logoScaleTween.WaitForCompletion();
        
        textFadeTween?.Kill();
        logoScaleTween?.Kill();
        backgroundScaleTween?.Kill();
        
        MainSceneManager.Instance.uiController.TitleViewClose();
        GameManager.Instance.IsSawTitle = true;
        
        gameObject.SetActive(false);
    }
}
