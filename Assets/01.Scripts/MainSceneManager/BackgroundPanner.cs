using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPanner : MonoBehaviour
{
    [SerializeField]
    private Vector2 direction;
    private Vector2 moveVector;

    private Vector2 limitPosition;
    private Vector2 initialPosition;


    [SerializeField]
    private GameObject[] panner;

    private void Awake(){
        limitPosition = Vector2.zero;
        initialPosition = Vector2.zero;

        limitPosition.x = 9.5f * direction.x;
        initialPosition.x = -9.5f * direction.x;

        initialPosition.y = panner[0].gameObject.transform.position.y;

        moveVector = direction / 20;
        StartCoroutine(MovePanner());
    }

    private IEnumerator MovePanner(){
        while(true){
            for(int i = 0; i < panner.Length; i++){
                panner[i].transform.Translate(moveVector);
                LimitCheck(panner[i]);    
            }
            yield return YieldInstructionCache.WaitFrame;
        }
    }


    // FIXME : 비효율적
    private void LimitCheck(GameObject pannerObject){
        if(direction.x < 0){
            if(pannerObject.transform.position.x < limitPosition.x){
                pannerObject.transform.position = initialPosition;
            }
        }  

        else if (direction.x > 0){
            if(pannerObject.transform.position.x > limitPosition.x){
                pannerObject.transform.position = initialPosition;
            }
        }
        
    }
}
