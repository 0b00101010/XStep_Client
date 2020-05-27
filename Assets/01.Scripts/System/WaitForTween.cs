using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaitForTween : CustomYieldInstruction
{
    private Tween tween;

    public override bool keepWaiting{
        get{
            return tween.IsPlaying();
        }
    }

    public WaitForTween(Tween tween){
        this.tween = tween;
    }
}
