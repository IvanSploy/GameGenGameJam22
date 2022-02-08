using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public PlayerController player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRate= 2f;
    private float nextFire = 0.0f;
    private bool canShoot;
    private GameObject _bullet;

    private int mySlot = -1;

    private void Awake()
    {
        canShoot = true;
    }

    void Start()
    {
        player = GetComponent<PlayerController>();
        player.totalCooldowns[1] = fireRate;
        bool encontrado = false;
        for (int i = 0; i < player.habilities.Length && !encontrado; i++)
        {
            if(player.habilities[i] == 1)
            {
                encontrado = true;
                mySlot = i;
            }
        }
    }

    void Update()
    {
        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
            if (nextFire < 0) nextFire = 0;
            if (nextFire <= 0)
            {
                canShoot = true;
            }
            if (mySlot >= 0) player.cooldowns[mySlot] = nextFire;
        }
    }

    public bool Fire(float velX, float velY)
    {
        if (canShoot)
        {
            _bullet = Instantiate(bullet, transform.position, Quaternion.identity);
            _bullet.GetComponent<Bullet>().SetVel(velX, velY);
            _bullet.GetComponent<SpriteRenderer>().enabled = true;
            canShoot = false;
            nextFire = fireRate;
            return true;
        }

        return false;
    }
}
