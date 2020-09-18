using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

public class AchieveNotify : MonoBehaviour {
    [SerializeField]
    private GameObject targetPosition;
    private CanvasGroup canvasGroup;
    
    private Vector3 defaultPosition;

    private void Awake() {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        defaultPosition = gameObject.transform.position;
    }

    private void OnEnable() {
        canvasGroup.alpha = 1.0f;
        gameObject.transform.position = defaultPosition;
    }

    private IEnumerator EnableCoroutine() {
        var moveTween = gameObject.transform.DOMoveY(targetPosition.transform.position.y, 0.25f);
        yield return moveTween.WaitForCompletion();

        yield return YieldInstructionCache.WaitingSeconds(1.0f);
        
        var fadeTween = canvasGroup.DOFade(0.0f, 0.25f);
        yield return fadeTween.WaitForCompletion();

        gameObject.SetActive(false);
    }
}

