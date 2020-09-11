using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultSelectButton : MainUIObject {
    [SerializeField]
    private int difficulty;
    
    public override void Execute() {
        GameManager.instance.songData.currentSelectDifficulty = difficulty;
    }
    
}
