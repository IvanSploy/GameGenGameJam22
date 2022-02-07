using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    //Referencias
    public static int keysTaken = 0;
    private PlayerController player;
    public float speed = 5;

    public bool taken = false;
    public int takenPos = 0;

    private void Start()
    {
        transform.position = GameManager.CenterVector(transform.position);
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (taken)
        {
            Vector3 target = player.transform.position - player.VectorFromDirection() * takenPos;
            float distance = Vector3.Distance(transform.position, target);
            transform.position =  Vector3.MoveTowards(transform.position, target, distance * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entro Trigger");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Holiii");
            FindObjectOfType<LevelManager>().decKeys();
            taken = true;
            keysTaken++;
            takenPos = keysTaken;
            Destroy(GetComponent<Collider2D>());
        }
    }
}
