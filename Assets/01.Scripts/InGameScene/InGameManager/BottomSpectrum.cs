using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BottomSpectrum : MonoBehaviour
{
    [SerializeField]
    private Image[] stickImages;

    private void Update(){
        float[] spectrum = AudioListener.GetSpectrumData(2048, 0, FFTWindow.Rectangular);

        for(int i = 0; i < stickImages.Length; i++){
            Vector2 firstSclae = stickImages[i].gameObject.transform.localScale;
            firstSclae.y = spectrum[i] * 30 + 1;
            stickImages[i].gameObject.transform.localScale = Vector2.MoveTowards(stickImages[i].gameObject.transform.localScale, firstSclae, 0.1f);
        }
    }
}
