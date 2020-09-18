using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StaticUIObject : MonoBehaviour
{
    [SerializeField]
    private GameObject targetPosition;

    private Vector3 defaultPosition;

    private void Awake() {
        defaultPosition = gameObject.transform.position;
    }

    private void OnEnable() {
        gameObject.transform.position = defaultPosition;
        gameObject.transform.DOMoveY(targetPosition.transform.position.y, 0.5f);
    }
}
