using System;

[Serializable]
public class GameResult{
    private int[] judgeCounts = new int[5];
    public int[] JudgeCounts {
        get => judgeCounts;
        set => judgeCounts = value;
    }
    
    private int nodeTotalCount;
    public int NodeTotalCount {
        get => nodeTotalCount;
        set => nodeTotalCount = value;
    }

    private float accuracy;
    public float Accuracy {
        get => accuracy;
        set => accuracy = value;
    }

    private int score;
    public int Score {
        get => score;
        set => score = value;
    }
}
