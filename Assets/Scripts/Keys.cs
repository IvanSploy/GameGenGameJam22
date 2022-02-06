using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entro Trigger");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Holiii");
            FindObjectOfType<LevelManager>().decKeys();
            Destroy(this.gameObject);
        }
    }
}
