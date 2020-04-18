using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    
    public Dir dir;
    
    public void spawn(Dir dir)
    {
        this.dir = dir;
    }
    private void FixedUpdate()
    {
        transform.position += new Vector3((int)dir, 0);
    }
}
public enum Dir
{
    right, left
}
