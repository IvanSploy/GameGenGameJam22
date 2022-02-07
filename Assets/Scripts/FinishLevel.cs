using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && FindObjectOfType<LevelManager>().getKeys() == 0)
        {
            FindObjectOfType<GameManager>().FinishLevel();
        }
    }
}
