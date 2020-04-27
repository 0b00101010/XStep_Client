using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeManager : MonoBehaviour
{
    [SerializeField]
    private int repeatFrame = 60;

    public IEnumerator SpriteFadeIn(SpriteRenderer spriteRenderer, float spendTime){
        Color newColor = spriteRenderer.color;
        newColor.a = 0;

        for(int i = 0; i < repeatFrame; i++){
            newColor.a = ((float)i/repeatFrame);
            spriteRenderer.color = newColor;
            yield return YieldInstructionCache.WaitingSeconds(spendTime / repeatFrame);
        }
    }

    public IEnumerator SpriteFadeOut(SpriteRenderer spriteRenderer, float spendTime){
        Color newColor = spriteRenderer.color;
        newColor.a = 1;

        for(int i = 0; i < repeatFrame; i++){
            newColor.a = 1.0f - ((float)i/repeatFrame);
            spriteRenderer.color = newColor;
            yield return YieldInstructionCache.WaitingSeconds(spendTime / repeatFrame);
        }
    }

    public IEnumerator ImageFadeIn(Image image, float spendTime){
        Color newColor = image.color;
        newColor.a = 0;

        for(int i = 0; i < repeatFrame; i++){
            newColor.a = ((float)i/repeatFrame);
            image.color = newColor;
            yield return YieldInstructionCache.WaitingSeconds(spendTime / repeatFrame);
        }
    }

    public IEnumerator ImageFadeOut(Image image, float spendTime){
        Color newColor = image.color;
        newColor.a = 1;

        for(int i = 0; i < repeatFrame; i++){
            newColor.a = 1.0f - ((float)i/repeatFrame);
            image.color = newColor;
            yield return YieldInstructionCache.WaitingSeconds(spendTime / repeatFrame);
        }
    }

    public IEnumerator TextFadeIn(Text text, float spendTime){
        Color newColor = text.color;
        newColor.a = 0;

        for(int i = 0; i < repeatFrame; i++){
            newColor.a = ((float)i/repeatFrame);
            text.color = newColor;
            yield return YieldInstructionCache.WaitingSeconds(spendTime / repeatFrame);
        }
    }

    public IEnumerator TextFadeOut(Text text, float spendTime){
        Color newColor = text.color;
        newColor.a = 1;

        for(int i = 0; i < repeatFrame; i++){
            newColor.a = 1.0f - ((float)i/repeatFrame);
            text.color = newColor;
            yield return YieldInstructionCache.WaitingSeconds(spendTime / repeatFrame);
        }
    }

    
}