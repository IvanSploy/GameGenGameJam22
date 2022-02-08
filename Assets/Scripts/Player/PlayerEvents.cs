using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerEvents : MonoBehaviour
{
    //Referencia
    private PlayerController _player;
    [SerializeField] private GameObject initialPosition;
    private bool isFstTime = true;
    public int myCheckPoint = 0;
    
    [SerializeField] private GameObject[] respawns;

    public bool haveShield = false;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
        InitialPosition();
    }

    public void OnEnemy()
    {
        //Destroy(gameObject);
        if (haveShield == false)
        {
            MovePlayer();
            Debug.Log("Morí");
        }
        else
        {
            Debug.Log("Rompe escudo");
            haveShield = false;
            _player._shield.enabled = false;
            _player.SetCooldown(_player.HabilityIndex);
        }
    }

    private void InitialPosition()
    {
        if (isFstTime)
        {
            this.gameObject.transform.position = GameManager.CenterVector(initialPosition.transform.position);
            _player.targetPosition = transform.position;
            isFstTime = false;
        }
    }

    public void MovePlayer()
    {
        if (_player.isDashing)
        {
            _player.StopTween();
            _player.DashFinish();
        }
        StartCoroutine(DoMovePlayer());
    }

    IEnumerator DoMovePlayer()
    {
        _player.playerCanControl = false;
        _player.anim.SetTrigger("teleport");
        _player.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(_player.anim.GetCurrentAnimatorStateInfo(0).length);
        _player.GetComponentInChildren<SpriteRenderer>().color = Color.white;

        if (myCheckPoint > 0)
        {
            transform.position = GameManager.CenterVector(respawns[myCheckPoint].GetComponent<Checkpoint>().transform.position);
            _player.targetPosition = transform.position;
        }
        else
        {
            transform.position = GameManager.CenterVector(initialPosition.transform.position);
            _player.targetPosition = transform.position;
        }

        _player.anim.SetTrigger("teleport");
        yield return new WaitForSeconds(_player.anim.GetCurrentAnimatorStateInfo(0).length);
        _player.playerCanControl = true;
    }
}
