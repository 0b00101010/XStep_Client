using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NumberConverter : MonoBehaviour
{

    [Header("Objects")]
    [SerializeField]
    private Image[] imageObjects;

    [Header("Resources")]
    [SerializeField]
    private Sprite[] spriteImages;

    private void Awake(){
        GameManager.Instance.inGameResources?.Deconstruct(out spriteImages);
    }

    public void Conversion(int value){
        
        bool notZero = false;

        for(int i = 0; i < imageObjects.Length; i++){
            int number = System.Convert.ToInt32(value.ToString("D" + imageObjects.Length)[i].ToString());
            imageObjects[i].sprite = spriteImages[number];

            if(number != 0 || i == imageObjects.Length - 1){
                notZero = true;
            }

            if(notZero){
                imageObjects[i].gameObject.SetActive(true);
            }
            else{
                imageObjects[i].gameObject.SetActive(false);
            }

        }        
    }
}
