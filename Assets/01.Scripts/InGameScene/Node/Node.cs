using UnityEngine;

public class Node : MonoBehaviour { 

    [Header("Value")]
    [SerializeField]
    private int redutionHp;

    private Color defaultColor;

    private float _judgePerfect;
    private float _judgeGreat;
    private float _judgeGood;

    protected float judgePerfect => _judgePerfect;
    protected float judgeGreat => _judgeGreat;
    protected float judgeGood => _judgeGood;

    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer spriteRenderer => _spriteRenderer;

    public virtual void Execute(Vector2 targetPosition) {}
    public virtual void Execute(Vector2 startPosition, Vector2 targetPosition) {}
    
    protected void Awake(){
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        defaultColor = _spriteRenderer.color;
        defaultColor.a = 0;
    }

    private void Start(){
        _judgePerfect = InGameManager.instance.judgePerfect;
        _judgeGreat = InGameManager.instance.judgeGreat;
        _judgeGood = InGameManager.instance.judgeGood;
    }

    public virtual void ObjectReset() {
        spriteRenderer.color = defaultColor;
        gameObject.SetActive(false);
    }

    public virtual void Interaction() {}

    public virtual void FailedInteraction() {
        InGameManager.instance.scoreManager.RedutionHP(redutionHp);
    }
}