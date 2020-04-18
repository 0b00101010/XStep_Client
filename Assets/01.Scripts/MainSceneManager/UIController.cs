using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text informationBoxText;

    public void SetInformationBoxText(string value){
        informationBoxText.text = value;
    }
}
