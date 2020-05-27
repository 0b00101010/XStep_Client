using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInteractionController : MonoBehaviour, ITouchObserver
{
    private List<List<Node>> activeNode = new List<List<Node>>();
    private Ray ray;

    private void Awake(){
        GameManager.instance.touchManager.AddTouchObserver(this);
        
        for(int i = 0; i < 4; i++){
            activeNode.Add(new List<Node>());
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

    public void TouchDownNotify(){
        NormalNodeInteraction(GetHitBoxIndex());
    }

    public void TouchUpNotify(){

    }

    private int GetHitBoxIndex(){
        ray.origin = GameManager.instance.touchManager.TouchDownPosition;
        ray.direction = Vector2.zero;

        RaycastHit2D hit;
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("HitBox"));

        if(hit.collider != null){
            return hit.collider.gameObject.GetComponent<HitBox>().Index;
        }
        
        return -1;
    }

    private void OnDestroy() {
        GameManager.instance.touchManager.RemoveTouchObserver(this);        
    }
}
