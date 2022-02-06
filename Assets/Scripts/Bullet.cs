using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float velX;
    [SerializeField] private float velY = 0;
    [SerializeField] private float speed = 1;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(velX, velY);
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if (other.tag == "Muro")
            Destroy(this.gameObject);
    }

    public void SetVel(float x, float y)
    {
        velX = x * speed;
        velY = y * speed;
    }
}
