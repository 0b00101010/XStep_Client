using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ani : MonoBehaviour
{
     SpriteRenderer sr;
     Image ig;
    private void Awake()
    {
        var a = GetComponent<SpriteRenderer>();
        var b= GetComponent<Image>();
        if (a != null) sr = a;
        if (b != null) ig = b;
    }

    public enum Name
    {
        QuaterTouch,TouchNode
    }

    public void play(Name name,int n)
    {
        
        StartCoroutine(AniPlay(name, n));
        
    }
     IEnumerator AniPlay(Name name,int n)
    {
        Debug.Log("onPlay");
        if (n == 0)
        {
            while (true)
            {
                yield return StartCoroutine(name.ToString());
            }
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                yield return StartCoroutine(name.ToString());
            }
        }
        
       
    }
     IEnumerator QuaterTouch()
    {
        yield return YieldInstructionCache.WaitingSeconds(0.5f);
        gameObject.SetActive(false);
    }
    
    



}
