using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInteractionController : MonoBehaviour
{
    private List<NormalNode> activeNormalNode = new List<NormalNode>();


    public void AddActiveNormalNode(Node node){
        activeNormalNode.Add(node as NormalNode);
    }

    public void RemoveActiveNormalNode(Node node){
        activeNormalNode.Remove(node as NormalNode);
    }   

    public void NormalNodeInteraction(){
        activeNormalNode[0].Interaction();
    }
}
