using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            OnEnemy();
        }
    }

    public void OnEnemy()
    {
        //Destroy(gameObject);
        Debug.Log("Morí");
    }
}
