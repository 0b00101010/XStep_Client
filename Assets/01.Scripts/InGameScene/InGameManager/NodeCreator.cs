using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NodeCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject nodeParentObject;

    [SerializeField]
    private Transform[] targetTransforms;

    private List<Node> normalNodes = new List<Node>();
    private Vector2[] targetPositions;

    private void Awake(){
        Node[] tempNodes;
        
        tempNodes = nodeParentObject.GetComponentsInChildren<NormalNode>(true); 
        normalNodes = tempNodes.ToList();

        targetPositions = new Vector2[targetTransforms.Length];

        for(int i = 0; i < targetTransforms.Length; i++){
            targetPositions[i] = targetTransforms[i].position; 
        }
    }

    private void Start(){
        StartCoroutine(UpdateCoroutine());
    }

    private IEnumerator UpdateCoroutine(){
        while(true){
            NodeGenerate();
            yield return YieldInstructionCache.WaitingSeconds(0.5f);
        }
    }

    private void NodeGenerate(){
        Node node;
        
        switch(Random.Range(0,3)){
            case 0:
            case 1:
            case 2:
            node = GetAvaliableNode(normalNodes);
            node.Execute(targetPositions[Random.Range(0,targetPositions.Length)]);
            break;
            default:
            break;
        }
    }

    private Node GetAvaliableNode(List<Node> nodes){
        Node returnNode = null;
        
        nodes.ForEach((node) => {
            if(node.gameObject.activeInHierarchy.Equals(false)){
                returnNode = node;
                return;
            }
        });

        return returnNode;
    }
}
