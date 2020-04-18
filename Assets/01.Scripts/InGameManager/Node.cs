using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node:MonoBehaviour
{


    public Vector3 dir;

    private void Awake()
    {
        dir = Vector3.zero;
    }

    public void spawn(Vector3 dir)
    {
        transform.position = Vector3.zero;
        this.dir = dir.normalized*0.1f;
    }

    private void FixedUpdate()
    {
        transform.position += dir;
    }
    
}
