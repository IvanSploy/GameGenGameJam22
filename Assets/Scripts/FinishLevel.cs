using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entro Trigger");
        if (other.tag == "Player" && FindObjectOfType<LevelManager>().getKeys() == 0)
        {
            Debug.Log("Soy jugador");
            FindObjectOfType<GameManager>().FinishLevel();
        }
    }
}
