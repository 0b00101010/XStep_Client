using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node:MonoBehaviour
{


    public Vector3 dir;
    public GameObject endOb;
    Vector3 startScale;
    Vector3 endScale;
    private void Awake()
    {
        startScale = transform.localScale;
        dir = Vector3.zero;
        endScale = endOb.transform.localScale;
    }

    public void spawn(Vector3 dir)
    {
        transform.localScale = startScale;
        transform.position = Vector3.zero;
        this.dir = dir.normalized*0.1f;
    }

    private void FixedUpdate()
    {
        transform.position += dir;
        transform.localScale = Vector3.Lerp(transform.localScale, endScale,Time.deltaTime*5);
    }

   
}
