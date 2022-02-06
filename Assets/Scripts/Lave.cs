using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lave : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Has muerto");
        }
    }
}
