using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuaterTouch : MonoBehaviour, IPointerClickHandler
{
    public InGameManager inGameManager;
    public int n;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(n);
        
        inGameManager.activeSafe(n);
    }
}
