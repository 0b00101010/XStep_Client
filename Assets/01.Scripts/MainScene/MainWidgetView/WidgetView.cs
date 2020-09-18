using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WidgetView : MonoBehaviour {
    private CanvasGroup canvasGroup;

    private void Awake() {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    private void OnEnable() {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1.0f, 0.5f);
    }
}
