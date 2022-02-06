using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRate= 0.5f;
    [SerializeField] private float nextFire = 0.0f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            fire();
        }
    }

    private void fire()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
