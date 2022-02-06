using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    //Enums
    public enum Direction
    {
        up,
        down,
        left,
        right
    }

    //Referencias
    private Animator anim;
    private InputController input;
    private Rigidbody2D _rigidbody2D;
    private Keyboard space;

    //Propiedades
    public int speed = 5;
    public Direction direction;
    public Vector2 targetPosition;
    public LayerMask obstacles;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        direction = Direction.down;
        input = new InputController();
        float x = (int) (transform.position.x) + Mathf.Sign(transform.position.x) * 0.5f;
        float y = (int) (transform.position.y) + Mathf.Sign(transform.position.y) * 0.5f;
        float z = transform.position.z;
        transform.position = new Vector3(x, y, z);
        targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        anim.SetFloat("speed", speed);
    }

    private void Update()
    {
        Vector2 dir = input.Player.Movement.ReadValue<Vector2>();
        if ((Vector2) transform.position == targetPosition)
        {
            anim.SetFloat("speed", 0);
        }
        else
        {
            anim.SetFloat("speed", speed + 1);
        }
        
        if (input.Player.Habilities.ReadValue<float>() > 0.1f && (Vector2) transform.position == targetPosition)
        {
            Teleport();
            return;
        }
        
        if (dir != Vector2.zero && (Vector2) transform.position == targetPosition)
        {
            anim.SetFloat("speed", 0);
            if (dir.x > 0)
            {
                direction = Direction.right;
                if (CanMove)
                {
                    anim.SetTrigger("right");
                    targetPosition += Vector2.right;
                }
            }
            else if (dir.x < 0)
            {
                direction = Direction.left;
                if (CanMove)
                {
                    anim.SetTrigger("left");
                    targetPosition += Vector2.left;
                }
            }
            else if (dir.y > 0)
            {
                direction = Direction.up;
                if (CanMove)
                {
                    anim.SetTrigger("up");
                    targetPosition += Vector2.up;
                }
            }
            else
            {
                direction = Direction.down;
                if (CanMove)
                {
                    anim.SetTrigger("down");
                    targetPosition += Vector2.down;
                }
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void Teleport()
    {
        Vector2 dir = Vector2.zero;
        switch (direction)
        {
            case Direction.right:
                dir = Vector2.right;
                break;
            case Direction.left:
                dir = Vector2.left;
                break;
            case Direction.up:
                dir = Vector2.up;
                break;
            case Direction.down:
                dir = Vector2.down;
                break;
        }
        int i = 1;
        RaycastHit2D result = Physics2D.Raycast(transform.position, dir, i, obstacles);
        while (!result.collider)
        {
            i++;
            result = Physics2D.Raycast(transform.position, dir, i, obstacles);
        }
        Debug.Log(result.collider);
        Vector2 aux = transform.position;
        switch (direction)
        {
            case Direction.right:
                aux += new Vector2(i - 1, 0);
                break;
            case Direction.left:
                aux += new Vector2(-i + 1, 0);
                break;
            case Direction.up:
                aux += new Vector2(0, i - 1);
                break;
            case Direction.down:
                aux += new Vector2(0, -i + 1);
                break;
        }
        targetPosition = aux;
        transform.position = targetPosition;
    }
    private bool CanMove
    {
        get
        {
            Vector2 dir = Vector2.zero;
            switch (direction)
            {
                case Direction.right:
                    dir = Vector2.right;
                    break;
                case Direction.left:
                    dir = Vector2.left;
                    break;
                case Direction.up:
                    dir = Vector2.up;
                    break;
                case Direction.down:
                    dir = Vector2.down;
                    break;
            }

            RaycastHit2D result = Physics2D.Raycast(transform.position, dir, 1, obstacles);
            return !result.collider;
        }
    }

    #region Input

    public void OnEnable()
    {
        input.Enable();
    }

    public void OnDisable()
    {
        input.Disable();
    }

    #endregion
}