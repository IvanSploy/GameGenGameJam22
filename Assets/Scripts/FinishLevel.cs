using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{

    private void Start()
    {
        transform.position = GameManager.CenterVector(transform.position);    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && FindObjectOfType<LevelManager>().GetKeys() == 0)
        {
            GameManager.FinishLevel();
        }
    }
}
