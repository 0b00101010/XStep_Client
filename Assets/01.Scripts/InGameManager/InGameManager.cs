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
    List<GameObject> swipes;
    public GameObject bigDirection;
    public GameObject bigSwipe;
    public float time = 0, cool = 2;
    public float time2 = 0, cool2 = 3;
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
        time2 += Time.deltaTime;
        if (time > cool)
        {
            spawnNode();
            time = 0;
        }
        if (time2 > cool2)
        {
            spawnSwipe();
            time2 = 0;
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
                    case 0:c = 45;break;
                    case 1:c = -45;break;
                    case 2:c = 135;break;
                    case 3:c = 225;break;
                }
                one.transform.rotation = Quaternion.AngleAxis(angle+c, Vector3.forward);
                one.GetComponent<Node>().spawn(directions[a].transform.position);
                break;
            }
        }
        
    }
    public void spawnSwipe()
    {
        GameObject one = null;
        for (int i = 0; i <swipes.Count; i++)
        {
            if (!swipes[i].activeSelf)
            {
                one = swipes[i];
                one.SetActive(true);
                var a = Random.Range(0, 2);

                Dir dir = a == 0 ? Dir.left : Dir.right;
                one.GetComponent<Swipe>().spawn(dir);
                break;
            }
        }

    }
}
