using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{
    public InGameManager inGameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("node"))
        {
            inGameManager.safeNum++;
            collision.gameObject.SetActive(false);
        };
    }
}
