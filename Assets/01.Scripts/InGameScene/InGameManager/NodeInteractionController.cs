using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInteractionController : MonoBehaviour, ITouchObserver
{
    private List<List<Node>> activeNode = new List<List<Node>>();
    private List<List<SlideNode>> activeSlideNode = new List<List<SlideNode>>();
    private List<List<LongNode>> activeLongNode = new List<List<LongNode>>();

    private Vector2 slideStartPosition;
    private Vector2 slideEndPosition;

    private Dictionary<string, HitBox> hitBoxes = new Dictionary<string, HitBox>();
    
    private Ray ray;

    private void Awake(){
        GameManager.instance.touchManager.AddTouchObserver(this);
        
        for(int i = 0; i < 4; i++){
            activeNode.Add(new List<Node>());
            activeSlideNode.Add(new List<SlideNode>());
            activeLongNode.Add(new List<LongNode>());
        }
    }

    #if UNITY_EDITOR
    private void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            NormalNodeInteraction(0);
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            NormalNodeInteraction(1);
        }
        else if(Input.GetKeyDown(KeyCode.Z)){
            NormalNodeInteraction(2);
        }
        else if(Input.GetKeyDown(KeyCode.X)){
            NormalNodeInteraction(3);
        }
        
    }
    #endif


    public void TouchDownNotify(){
        NormalNodeInteraction(GetHitBoxIndex());
        SlideNodeInteractionStart(GetHitBoxPosition(GameManager.instance.touchManager.TouchDownPosition));
    }

    public void TouchUpNotify(){
        SlideNodeInteractionEnd(GetHitBoxPosition(GameManager.instance.touchManager.TouchUpPosition));
    }

    public void AddActiveNode(Node node, int position){
        activeNode[position].Add(node);
    }

    public void RemoveActiveNormalNode(Node node, int position){
        activeNode[position].Remove(node);
    }   

    public void AddActiveSlideNode(Node node, int index){
        var newNode = node as SlideNode;
        activeSlideNode[index].Add(newNode);
    }

    public void RemoveActiveSlideNode(Node node, int index){
        var newNode = node as SlideNode;
        activeSlideNode[index].Remove(newNode);
    }

    public void NormalNodeInteraction(int position){
        try{
            activeNode[position][0]?.Interaction();
        }catch{
            return;
        }
    }

    public void SlideNodeInteractionStart(Vector2 position){
        slideStartPosition = position;
    }

    public void SlideNodeInteractionEnd(Vector2 position){
        slideEndPosition = position;
        SlideNodeInteraction();
    }

    private void SlideNodeInteraction(){
        Vector2 direction = slideStartPosition.Direction(slideEndPosition);
        int index = -1;
        
        if(slideStartPosition.y > 0 && slideEndPosition.y > 0){
            index = 0;
        } else if(slideStartPosition.y < 0 && slideEndPosition.y < 0){
            index = 1;
        } else if(slideStartPosition.x < 0 && slideEndPosition.x < 0){
            index = 2;    
        } else if(slideStartPosition.x > 0 && slideEndPosition.x > 0){
            index = 3;
        } 

        if(index == -1){
            return;
        } else if(activeSlideNode[index].Count == 0){
            return;
        }

        if(activeSlideNode[index][0].SlideDirection.Equals(direction)){
            activeSlideNode[index][0]?.Interaction();
        }
    }

    public void AddActiveLongNode(Node node, int index){
        var newNode = node as LongNode;
        activeLongNode[index].Add(newNode);
    }

    public void RemoveActioveLongNode(Node node, int index){
        var removeNode = node as LongNode;
        activeLongNode[index].Remove(removeNode);
    }

    public void LongNodeInteractionStart(Vector2 position){

    }

    public void LongNodeInteractionEnd(Vector2 poisiton){
        
    }

    public void LongNodeStop(int position){
        int index = -1;

        do{
            index++;
        }while(activeLongNode[position][index].TailStart());
    }

    private int GetHitBoxIndex(){
        ray.origin = GameManager.instance.touchManager.TouchDownPosition;

        RaycastHit2D hit;
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("HitBox"));

        if(!hit.collider.Equals(null)) {
            var objectName = hit.collider.gameObject.name;
            
            if (!hitBoxes.ContainsKey(objectName)){
                hitBoxes.Add(objectName, hit.collider.gameObject.GetComponent<HitBox>());
            }

            return hitBoxes[objectName].Index;
        }
        
        return -1;
    }

    private Vector2 GetHitBoxPosition(Vector2 position){
        ray.origin = position;
        ray.direction = Vector2.zero;
        
        RaycastHit2D hit;
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("HitBox"));

        if(!hit.collider.Equals(null)){
            return hit.collider.gameObject.transform.position;
        }
        
        return Vector2.zero;
    }
}
