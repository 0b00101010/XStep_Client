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

    private Vector2[] slideStartPosition = new Vector2[2];
    private Vector2[] slideEndPosition = new Vector2[2];

    private Dictionary<string, HitBox> hitBoxes = new Dictionary<string, HitBox>();
    
    private Ray ray;

    private Dictionary<LongNode, IEnumerator> touchHoldCoroutines = new Dictionary<LongNode, IEnumerator>();
    private Func<double> getCurrentSample;
    
    private void Awake(){
        for(int i = 0; i < 4; i++){
            activeNode.Add(new List<Node>());
            activeSlideNode.Add(new List<SlideNode>());
            activeLongNode.Add(new List<LongNode>());
        }
    }
    
    private void Start() {
        getCurrentSample = InGameManager.instance.metronome.GetCurrentSample;
        GameManager.instance.touchManager.AddTouchObserver(this);
    }

    #if UNITY_EDITOR
    private void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            NormalNodeInteraction(0, getCurrentSample());
            LongNodeInteractionStart(0, getCurrentSample());
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            NormalNodeInteraction(1, getCurrentSample());
            LongNodeInteractionStart(1, getCurrentSample());
        }
        else if(Input.GetKeyDown(KeyCode.Z)){
            NormalNodeInteraction(2, getCurrentSample());
            LongNodeInteractionStart(2, getCurrentSample());
        }
        else if(Input.GetKeyDown(KeyCode.X)){
            NormalNodeInteraction(3, getCurrentSample());
            LongNodeInteractionStart(3, getCurrentSample());
        }

        if (Input.GetKey(KeyCode.A)) {
            LongNodeInteractionStart(0, getCurrentSample());
        }
        else if (Input.GetKey(KeyCode.S)) {
            LongNodeInteractionStart(1, getCurrentSample());
        }
        else if (Input.GetKey(KeyCode.Z)) {
            LongNodeInteractionStart(2, getCurrentSample());
        }
        else if (Input.GetKey(KeyCode.X)) {
            LongNodeInteractionStart(3, getCurrentSample());
        }
        
    }
    #endif


    public void TouchDownNotify(int touchIndex) {
        var boxIndex = GetHitBoxIndex();

        if (boxIndex == -1) {
            return;
        }
        
        NodeInteraction(boxIndex, getCurrentSample());
        SlideNodeInteractionStart(touchIndex, GetHitBoxPosition(GameManager.instance.touchManager.TouchDownPosition));
    }

    public void TouchUpNotify(int touchIndex) {
        var boxIndex = GetHitBoxIndex();

        if (boxIndex == -1) {
            return;
        }
        
        if (activeNode[boxIndex].Count > 0 && activeNode[boxIndex][0] is LongNode) {
            StopLongCoroutine(activeNode[boxIndex][0] as LongNode);
        }
        
        SlideNodeInteractionEnd(touchIndex, GetHitBoxPosition(GameManager.instance.touchManager.TouchUpPosition[touchIndex]));
    }
    
    private IEnumerator TouchHold(int position) {
        while (true) {
            LongNodeInteractionStart(position, getCurrentSample());
            yield return YieldInstructionCache.WaitFrame;
        }
    }

    private void StopLongCoroutine(LongNode longNode) {
        if (touchHoldCoroutines.ContainsKey(longNode)) {
            touchHoldCoroutines[longNode].Stop(this);
            touchHoldCoroutines.Remove(longNode);
        }
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

    public void NodeInteraction(int position, double interactionTime){
        if (activeNode[position].Count <= 0) {
            return;
        }
        
        if (activeNode[position][0] is NormalNode ) {
            NormalNodeInteraction(position, interactionTime);
        }
        else if (activeNode[position][0] is LongNode) {
            LongNodeInteractionStart(position, interactionTime);

            var longNode = activeNode[position][0] as LongNode;
            if (touchHoldCoroutines.ContainsKey(longNode)) {
                touchHoldCoroutines[longNode]?.Stop(this);
                touchHoldCoroutines.Remove(longNode);
            }
            touchHoldCoroutines.Add(longNode, TouchHold(position).Start(this));
        }
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
    
    public void NormalNodeInteraction(int position, double interactionTime){
        if (activeNode[position].Count > 0) {
            if (activeNode[position][0] is NormalNode) {
                activeNode[position][0].Interaction(interactionTime);
            }
        }
    }

    public void SlideNodeInteractionStart(int index, Vector2 position){
        slideStartPosition[index] = position;
    }

    public void SlideNodeInteractionEnd(int index, Vector2 position){
        slideEndPosition[index] = position;
        SlideNodeInteraction(getCurrentSample(), index);
    }

    private void SlideNodeInteraction(double interactionTime, int slideIndex){
        Vector2 direction = slideStartPosition[slideIndex].Direction(slideEndPosition[slideIndex]);
        int index = -1;
        
        if(slideStartPosition[slideIndex].y > 0 && slideEndPosition[slideIndex].y > 0){
            index = 0;
        } else if(slideStartPosition[slideIndex].y < 0 && slideEndPosition[slideIndex].y < 0){
            index = 1;
        } else if(slideStartPosition[slideIndex].x < 0 && slideEndPosition[slideIndex].x < 0){
            index = 2;    
        } else if(slideStartPosition[slideIndex].x > 0 && slideEndPosition[slideIndex].x > 0){
            index = 3;
        } 

        if(index == -1 || activeSlideNode[index].Count == 0){
            return;
        }

        if(activeSlideNode[index][0].SlideDirection.Equals(direction)){
            activeSlideNode[index][0]?.Interaction(interactionTime);
        }
    }
    
    public void AddActiveLongNode(Node node, int index){
        var newNode = node as LongNode;
        activeNode[index].Add(newNode);
    }

    public void RemoveActionLongNode(Node node, int index){
        var removeNode = node as LongNode;
        StopLongCoroutine(removeNode);
        activeNode[index].Remove(removeNode);
    }

    public void LongNodeInteractionStart(int position, double interactionTime) {
        var longNode = activeNode[position];
        
        if (longNode.Count > 0) {
            if (longNode[0] is LongNode) {
                longNode[0].Interaction(interactionTime);
            }
        }
    }
    
    public void LongNodeStop(int position){
        int index = 0;
        LongNode longNode;
       
        for (int i = 0; i < activeNode[position].Count; i++) {
            if (activeNode[position][i] is LongNode) {
                longNode = activeNode[position][i] as LongNode;
                if (longNode.TailStart() == false) {
                    break;
                }
            }
        }
    }

    private int GetHitBoxIndex(){
        ray.origin = GameManager.instance.touchManager.TouchDownPosition;

        RaycastHit2D hit;
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("HitBox"));

        if(hit.collider != null) {
            var objectName = hit.collider.gameObject.name;
            
            if (!hitBoxes.ContainsKey(objectName)){
                hitBoxes.Add(objectName, hit.collider.gameObject.GetComponent<HitBox>());
            }
            
            hitBoxes[objectName].Execute();
            return hitBoxes[objectName].Index;
        }
        
        return -1;
    }

    private Vector2 GetHitBoxPosition(Vector2 position){
        ray.origin = position;
        ray.direction = Vector2.zero;
        
        RaycastHit2D hit;
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("HitBox"));

        if(hit.collider != null){
            return hit.collider.gameObject.transform.position;
        }
        
        return Vector2.zero;
    }
}
