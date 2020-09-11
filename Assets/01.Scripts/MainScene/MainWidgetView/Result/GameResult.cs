using System;

[Serializable]
public class GameResult{
    private int[] judgeCounts;
    public int[] JudgeCounts {
        get => judgeCounts;
        set => judgeCounts = value;
    }
    
    private int nodeTotalCount;
    public int NodeTotalCount {
        get => nodeTotalCount;
        set => nodeTotalCount = value;
    }
}
