using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{

    //Enums
    public enum Direction { up, down, left, right }

    //Referencias
    private Animator anim;
    private InputController input;

    //Propiedades
    public int speed = 5;
    public Direction direction;
    public Vector2 targetPosition;
    public LayerMask obstacles;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        direction = Direction.down;
        input = new InputController();
        float x = (int)(transform.position.x) + Mathf.Sign(transform.position.x) * 0.5f;
        float y = (int)(transform.position.y) + Mathf.Sign(transform.position.y) * 0.5f;
        float z = transform.position.z;
        transform.position = new Vector3(x, y, z);
        targetPosition = transform.position;
    }

    private void Update()
    {
        Vector2 dir = input.Player.Movement.ReadValue<Vector2>();
        Debug.Log(dir);
        if(dir != Vector2.zero && (Vector2) transform.position == targetPosition)
        {
            if(dir.x > 0)
            {
                direction = Direction.right;
                if (CanMove) targetPosition += Vector2.right;
            }
            else if (dir.x < 0)
            {
                direction = Direction.left;
                if (CanMove) targetPosition += Vector2.left;

            }
            else if (dir.y > 0)
            {
                direction = Direction.up;
                if (CanMove) targetPosition += Vector2.up;

            }
            else
            {
                direction = Direction.down;
                if (CanMove) targetPosition += Vector2.down;

            }
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
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
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    #endregion
}
