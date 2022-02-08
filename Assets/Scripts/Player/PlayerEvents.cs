using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    //Referencia
    private PlayerController _player;

    public bool haveShield = false;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

    public void OnEnemy()
    {
        //Destroy(gameObject);
        if (haveShield == false)
            Debug.Log("Morí");
        else
        {
            Debug.Log("Rompe escudo");
            haveShield = false;
            _player._shield.enabled = false;
            _player.SetCooldown(_player.HabilityIndex);
        }
    }
}
