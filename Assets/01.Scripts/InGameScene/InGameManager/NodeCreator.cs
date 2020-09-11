using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class NodeCreator : MonoBehaviour
{

    [Header("Objects")]
    [SerializeField]
    private GameObject normalNodeParentObject;

    [SerializeField]
    private GameObject slideNodeParentObject;

    [SerializeField]
    private GameObject longNodeParentObject;

    [SerializeField]
    private Transform nodeGeneratePosition;

    [Header("Values")]
    [SerializeField]
    private Transform[] normalNodeTransforms;

    [SerializeField]
    private Transform[] slideNodeTransforms;

    private List<Node> normalNodes = new List<Node>();
    private List<Node> longNodes = new List<Node>();
    private List<Node> slideNodes = new List<Node>();

    private Vector2[] normalNodeTargetPositions;
    private Vector2[] slideNodeTargetPositions;

    [Header("Events")]
    [SerializeField]
    private Event<int> longNodeStopEvent;

    private void Awake(){
        Node[] tempNodes;
        
        tempNodes = normalNodeParentObject.GetComponentsInChildren<NormalNode>(true); 
        normalNodes = tempNodes.ToList();

        tempNodes = slideNodeParentObject.GetComponentsInChildren<SlideNode>(true); 
        slideNodes = tempNodes.ToList();

        normalNodeTargetPositions = new Vector2[normalNodeTransforms.Length];
        slideNodeTargetPositions = new Vector2[slideNodeTransforms.Length];

        for(int i = 0; i < normalNodeTransforms.Length; i++){
            normalNodeTargetPositions[i] = normalNodeTransforms[i].position; 
        }

        for(int i = 0; i < slideNodeTransforms.Length; i++){
            slideNodeTargetPositions[i] = slideNodeTransforms[i].position;
        }

        tempNodes = longNodeParentObject.GetComponentsInChildren<LongNode>(true);
        longNodes = tempNodes.ToList();
    }
    
    public void NormalNodeGenerate(int index = 0){
        Node node = GetAvaliableNode(normalNodes);
        node.Execute(nodeGeneratePosition.position, normalNodeTargetPositions[index]);
    }

    // FIXME : 솔직히 조금 그렇지 않나
    public void SlideNodeGenerate(int index = 0){
        Node node = GetAvaliableNode(slideNodes);

        Vector2 startPosition = Vector2.zero;
        Vector2 targetPosition = Vector2.zero;
        
        SetSlideNodeVector(ref startPosition, ref targetPosition, index);
        node.Execute(startPosition, targetPosition, index);
    }

    private void SetSlideNodeVector(ref Vector2 startPosition, ref Vector2 targetPosition, int index){  
        switch(index){
            case 0:
            case 1:
            startPosition = slideNodeTargetPositions[1];
            targetPosition = slideNodeTargetPositions[0];
            break;

            case 2:
            case 3:
            startPosition = slideNodeTargetPositions[0];
            targetPosition = slideNodeTargetPositions[1];
            break;

            case 4:
            case 5:
            startPosition = slideNodeTargetPositions[3];
            targetPosition = slideNodeTargetPositions[2];
            break;

            case 6:
            case 7:
            startPosition = slideNodeTargetPositions[2];
            targetPosition = slideNodeTargetPositions[3];
            break;            
        }

    }
    
    public void LongNodeGenerate(int index = 0){
        Node node = GetAvaliableNode(longNodes);
        node.Execute(nodeGeneratePosition.position, normalNodeTargetPositions[index], index);
    }

    public void LongNodeStop(int index){
        longNodeStopEvent.Invoke(index);
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
