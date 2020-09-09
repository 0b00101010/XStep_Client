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
    private Queue<Node> longNodes;
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
        longNodes = new Queue<Node>(tempNodes.ToList());
    }
    
    #if UNITY_EDITOR
    private void Update(){
        switch(Input.anyKeyDown){
            case var k when Input.GetKeyDown(KeyCode.I):
            NormalNodeGenerate();
            break;
            
            case var k when Input.GetKeyDown(KeyCode.O):
            SlideNodeGenerate();
            break;
            
            case var k when Input.GetKeyDown(KeyCode.P):
            LongNodeGenerate();
            break;
        
        } 
    }
    #endif

    private IEnumerator NodeGenerateCoroutine(){
        while(true){
            int i = Random.Range(0,100);
            if(i < 80){
                NormalNodeGenerate();
            } else {
                SlideNodeGenerate();
            }
            yield return YieldInstructionCache.WaitingSeconds(1.5f);
        }
    }

    private void NormalNodeGenerate(){
        Node node = GetAvaliableNode(normalNodes);
        node.Execute(nodeGeneratePosition.position, normalNodeTargetPositions[Random.Range(0, normalNodeTargetPositions.Length)]);
    }


    public void NormalNodeGenerate(int index = 0){
        Node node = GetAvaliableNode(normalNodes);
        node.Execute(nodeGeneratePosition.position, normalNodeTargetPositions[index]);
    }

    private void SlideNodeGenerate(){
        int index = Random.Range(0, 8);

        Node node = GetAvaliableNode(slideNodes);

        Vector2 startPosition = Vector2.zero;
        Vector2 targetPosition = Vector2.zero;
        
        SetSlideNodeVector(ref startPosition, ref targetPosition, index);
        
        node.Execute(startPosition, targetPosition, index);
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

    private void LongNodeGenerate(){
        int index = Random.Range(0,4);
        Node node = longNodes.Dequeue();
        node.Execute(nodeGeneratePosition.position, normalNodeTargetPositions[index], index);
    }

    public void LongNodeGenerate(int index = 0){
        Node node = longNodes.Dequeue();
        node.Execute(nodeGeneratePosition.position, normalNodeTargetPositions[index], index);
    }

    public void LongNodeStop(int index){
        longNodeStopEvent.Invoke(index);
    }

    public void EnqueueNode(Node node, int none) {
        if (node is LongNode == false) {
            throw new ArgumentException();
        }

        longNodes.Enqueue(node);
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
