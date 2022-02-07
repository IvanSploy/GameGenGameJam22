using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //Enums
    public enum Direction { up, down, left, right }

    //Referencias
    private Animator anim;
    private Rigidbody2D _rigidbody2D;

    //Propiedades
    public Direction direction;
    public int speed = 5;
    [HideInInspector]
    public Vector2 targetPosition;
    public LayerMask[] obstacles;
    public bool changeAfterMoves = false;
    public int moves = 3;

    private int currentMoves = 0;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        transform.position = GameManager.CenterVector(transform.position);
        targetPosition = transform.position;
    }

    private void Update()
    {
        if ((Vector2)transform.position == targetPosition)
        {
            if (changeAfterMoves)
            {
                currentMoves++;
                if (currentMoves >= moves)
                {
                    currentMoves = 0;
                    CalculateDirection();
                }
            }
            if (direction == Direction.right)
            {
                if (CanMove)
                {
                    targetPosition += Vector2.right;
                }
            }
            else if (direction == Direction.left)
            {
                if (CanMove)
                {
                    targetPosition += Vector2.left;
                }
            }
            else if (direction == Direction.up)
            {
                if (CanMove)
                {
                    targetPosition += Vector2.up;
                }
            }
            else
            {
                if (CanMove)
                {
                    targetPosition += Vector2.down;
                }
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void ChangeDirection(Direction dir)
    {
        Direction prevDir = direction;
        direction = dir;
        switch (direction)
        {
            case Direction.up:
                break;
            case Direction.down:
                break;
            case Direction.left:
                break;
            case Direction.right:
                break;
        }
    }

    public abstract void CalculateDirection();

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
            int layer = 0;
            for (int i = 0; i < obstacles.Length; i++)
            {
                layer |= obstacles[i].value;
            }
            RaycastHit2D result = Physics2D.Raycast(transform.position, dir, 1, layer);
            if (result.collider)
            {
                CalculateDirection();
            }
            return !result.collider;
        }
    }
}
