using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRate= 0.5f;
    private float nextFire = 0.0f;
    private bool canShoot;
    private GameObject _bullet;

    private void Awake()
    {
        canShoot = true;
    }

    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            canShoot = true;
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
            return true;
        }

        return false;
    }
}
