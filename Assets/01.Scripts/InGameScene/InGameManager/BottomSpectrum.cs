using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BottomSpectrum : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Image[] stickImages;
    
    [Header("Values")]
    [SerializeField]
    private float scaleValue;

    private float[] spectrum = new float[2048]; 

    private void Update(){
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        
        for(int i = 0; i < stickImages.Length; i++){
            Vector2 firstSclae = stickImages[i].gameObject.transform.localScale;
            firstSclae.y = (spectrum[i] * scaleValue * (audioSource.volume/1.0f)) + 1;
            stickImages[i].gameObject.transform.localScale = Vector2.MoveTowards(stickImages[i].gameObject.transform.localScale, firstSclae, 0.1f);
        }
    }
}
