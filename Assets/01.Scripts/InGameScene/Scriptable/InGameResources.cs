using UnityEngine;

[CreateAssetMenu(fileName = "InGameResources", menuName = "Scriptable Object/InGameResources", order = 0)]
public class InGameResources : ScriptableObject {
    [SerializeField]
    private Sprite[] numSprites;

    public void Deconstruct(out Sprite[] numSprites){
        numSprites = this.numSprites;
    }   
}