using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public bool haveShield = false;
    public void OnEnemy()
    {
        //Destroy(gameObject);
        if (haveShield == false)
            Debug.Log("Morí");
        else
        {
            Debug.Log("Rompe escudo");
            haveShield = false;
        }
    }
}
