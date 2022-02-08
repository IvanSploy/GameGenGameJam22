using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool haveCheckPoint = false;
    private GameObject checkPoint;
    [SerializeField] private int numCheck;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && FindObjectOfType<PlayerEvents>().myCheckPoint < numCheck)
        {
            FindObjectOfType<PlayerEvents>().myCheckPoint = numCheck;
            checkPoint = this.gameObject;
            GetComponent<Collider2D>().enabled = false;
            haveCheckPoint = true;
        }
    }

    public bool GetHaveCheckPoint()
    {
        return haveCheckPoint;
    
    }

    public GameObject GetCheckPoint()
    {
        return checkPoint;
    }
}
