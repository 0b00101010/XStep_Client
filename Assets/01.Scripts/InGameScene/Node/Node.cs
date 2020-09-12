using System;
using UnityEngine;

public class Node : MonoBehaviour { 

    [Header("Value")]
    [SerializeField]
    private int redutionHp;

    [SerializeField]
    private float _arriveTime;
    protected float arriveTime => _arriveTime;

    private double _arriveTimeToSample;
    protected double arriveTimeToSample => _arriveTimeToSample;
    
    private Color defaultColor;

    [Header("Judge zone")]
    [SerializeField]
    private double _judgePerfect;
    
    [SerializeField]
    private double _judgeGreat;
    
    [SerializeField]  
    private double _judgeGood;
    
    protected double judgePerfect;
    protected double judgeGreat;
    protected double judgeGood;

    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer spriteRenderer => _spriteRenderer;

    private double _generateTime;
    public double generateTime {
        get => _generateTime;
        set => _generateTime = value;
    }

    private double _perfectSample;
    protected double perfectSample {
        get => _perfectSample;
        set => _perfectSample = value;
    }

    private Func<double> getCurrentTimeSample;
    public Func<double> GetCurrentTimeSample => getCurrentTimeSample;

    public virtual void Execute(Vector2 targetPosition, double generateTime) {}
    public virtual void Execute(Vector2 startPosition, Vector2 targetPosition, double generateTime) {}
    public virtual void Execute(Vector2 startPosition, Vector2 targetPosition, double generateTime, int index) {}

    protected void Awake(){
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        defaultColor = _spriteRenderer.color;
        defaultColor.a = 0;
    }

    private void Start() {
        getCurrentTimeSample = InGameManager.instance.metronome.GetCurrentSample;
        var frequency = InGameManager.instance.metronome.GetFrequency();

        _arriveTimeToSample = frequency * _arriveTime;

        judgePerfect = (_judgePerfect / 1000) * frequency;
        judgeGreat = (_judgeGreat / 1000) * frequency;
        judgeGood = (_judgeGood / 1000) * frequency;
    }

    public virtual void ObjectReset() {
        spriteRenderer.color = defaultColor;
        gameObject.SetActive(false);
    }

    public virtual void Interaction(double interactionTime) {}

    public virtual void FailedInteraction() { }
}