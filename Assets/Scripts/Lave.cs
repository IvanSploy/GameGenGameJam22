using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lave : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<PlayerController>().isDashing)
            {
                FindObjectOfType<PlayerEvents>().OnDie(false);
            }
        }
    }
}
