using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    [SerializeField]
    private Image backgroundImage;

    private void Awake(){
        StartCoroutine(BackgroundRotate());
    }
    
    private IEnumerator BackgroundRotate(){
        while(true){
            backgroundImage.gameObject.transform.Rotate(Vector3.forward);
            yield return YieldInstructionCache.WaitFrame;
        }
    }
}
