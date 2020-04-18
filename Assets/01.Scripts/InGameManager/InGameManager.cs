using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{

    public GameObject bigNode;
    public GameObject bigSafe;
    List<GameObject> nodes;
    List<GameObject> safes;
    List<GameObject> directions;
    public GameObject bigDirection;
    public float time = 0, cool = 2;
    public int safeNum = 0;
    public int dangerNum = 0;
    private void Awake()
    {
        nodes = new List<GameObject>();
        directions= new List<GameObject>();
        safes = new List<GameObject>();
        for (int i = 0; i < bigNode.transform.childCount; i++)
        {
            nodes.Add(bigNode.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < bigDirection.transform.childCount; i++)
        {
            directions.Add(bigDirection.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < bigSafe.transform.childCount; i++)
        {
            safes.Add((bigSafe.transform.GetChild(i).gameObject));
        }

    }
    public void activeSafe(int n)
    {
        
        safes[n].SetActive(true);
        var a = safes[n].GetComponent<Ani>();
        a.play(Ani.Name.QuaterTouch, 1);
    }
 

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > cool)
        {
            spawnNode();
            time = 0;
        }


        
    }
    public void spawnNode()
    {
        GameObject one=null;
        for(int i = 0; i < nodes.Count; i++)
        {
            if (!nodes[i].activeSelf)
            {
                one = nodes[i];
                one.SetActive(true);
                var a = Random.Range(0, directions.Count);
                var v = (directions[a].transform.position - one.transform.position);
                var angle = Mathf.Atan2(v.y, v.x) * Mathf.Deg2Rad;
                var c = 0;
                switch (a)
                {
                    case 0:c = 1;break;
                    case 1:c = -1;break;
                    case 2:c = -1;break;
                    case 3:c = 1;break;
                }
                one.transform.rotation = Quaternion.AngleAxis(angle+45*c, Vector3.forward);
                one.GetComponent<Node>().spawn(directions[a].transform.position);
                break;
            }
        }
        
    }
}
