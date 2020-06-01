using UnityEngine;

public class Node : MonoBehaviour { 

    [Header("Value")]
    [SerializeField]
    private int redutionHp;

    public virtual void Execute(Vector2 targetPosition) {}
    public virtual void Execute(Vector2 startPosition, Vector2 targetPosition) {}
    
    public virtual void ObjectReset() {
        gameObject.SetActive(false);
    }

    public virtual void Interaction() {}

    public virtual void FailedInteraction() {
        InGameManager.instance.scoreManager.RedutionHP(redutionHp);
    }
}