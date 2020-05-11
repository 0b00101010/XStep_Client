using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInteractionController : MonoBehaviour
{
    private List<List<Node>> activeNode = new List<List<Node>>();


    private void Awake(){
        for(int i = 0; i < 4; i++){
            activeNode.Add(new List<Node>());
        }
    }

    public void AddActiveNode(Node node, int position){
        activeNode[position].Add(node);
    }

    public void RemoveActiveNormalNode(Node node, int position){
        activeNode[position].Remove(node);
    }   

    public void NormalNodeInteraction(int position){
        try{
            activeNode[position][0]?.Interaction();
        }catch{
            return;
        }
    }
}
