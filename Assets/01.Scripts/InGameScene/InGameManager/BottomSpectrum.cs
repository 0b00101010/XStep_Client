using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BottomSpectrum : MonoBehaviour
{
    [SerializeField]
    private Image[] stickImages;

    [SerializeField]
    private VOIDEvent effectEvent;
    
    private float[] spectrum = new float[2048]; 

    private void Update(){
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        if(spectrum.Max() > 0.35f){
            effectEvent.Invoke();
        }
        for(int i = 0; i < stickImages.Length; i++){
            Vector2 firstSclae = stickImages[i].gameObject.transform.localScale;
            firstSclae.y = spectrum[i] * 30 + 1;
            stickImages[i].gameObject.transform.localScale = Vector2.MoveTowards(stickImages[i].gameObject.transform.localScale, firstSclae, 0.1f);
        }
    }
}
