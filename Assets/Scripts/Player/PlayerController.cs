using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
using DG.Tweening;

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

    //Habilites
    public int[] habilities = {0, 1, 2};
    private int m_indexHabilities;
    public int IndexHabilities
    {
        get
        {
            return m_indexHabilities;
        }

        set
        {
            m_indexHabilities = value;
            if (m_indexHabilities < -habilities.Length) m_indexHabilities = 0;
            m_indexHabilities = m_indexHabilities >= habilities.Length ? m_indexHabilities %= habilities.Length : m_indexHabilities;
            m_indexHabilities = m_indexHabilities < 0 ? m_indexHabilities = habilities.Length + m_indexHabilities : m_indexHabilities;
        }
    }
    private Weapon _weapon;
    private void Awake()
    {
        IndexHabilities = 0;
        anim = GetComponentInChildren<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        direction = Direction.down;
        input = new InputController();
        CenterPlayer();
        targetPosition = transform.position;
    }

    private void Start()
    {
        input.Player.Habilities.started += (ctx) => ManageHabilities();
        input.Player.ChangeHability.started += (ctx) => OnNextHability();
        DOTween.Init();
    }

    private void FixedUpdate()
    {
        anim.SetFloat("speed", speed);
    }

    private void Update()
    {
        Vector2 dir = input.Player.Movement.ReadValue<Vector2>();
        if (dir == Vector2.zero && (Vector2) transform.position == targetPosition)
        {
            anim.SetFloat("speed", 0);
        }
        else
        {
            anim.SetFloat("speed", speed + 1);
        }
        
        if (dir != Vector2.zero && (Vector2) transform.position == targetPosition)
        {
            Direction prevDir = direction;
            if (dir.x > 0)
            {
                direction = Direction.right;
                if (CanMove)
                {
                    if (!prevDir.Equals(direction))
                    {
                        anim.SetFloat("speed", 0);
                        anim.SetTrigger("right");
                    }
                    targetPosition += Vector2.right;
                }
            }
            else if (dir.x < 0)
            {
                direction = Direction.left;
                if (CanMove)
                {
                    if (!prevDir.Equals(direction))
                    {
                        anim.SetFloat("speed", 0);
                        anim.SetTrigger("left");
                    }
                    targetPosition += Vector2.left;
                }
            }
            else if (dir.y > 0)
            {
                direction = Direction.up;
                if (CanMove)
                {
                    if (!prevDir.Equals(direction))
                    {
                        anim.SetFloat("speed", 0);
                        anim.SetTrigger("up");
                    }
                    targetPosition += Vector2.up;
                }
            }
            else
            {
                direction = Direction.down;
                if (CanMove)
                {
                    if (!prevDir.Equals(direction))
                    {
                        anim.SetFloat("speed", 0);
                        anim.SetTrigger("down");
                    }
                    targetPosition += Vector2.down;
                }
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void CenterPlayer()
    {
        float x = (int)(transform.position.x) + Mathf.Sign(transform.position.x) * 0.5f;
        float y = (int)(transform.position.y) + Mathf.Sign(transform.position.y) * 0.5f;
        float z = transform.position.z;
        //transform.position = new Vector3(x, y, z);
        targetPosition = new Vector3(x, y, z);
    }

    private void Teleport()
    {
        if ((Vector2)transform.position == targetPosition)
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
            while (!result.collider && i <= 1000)
            {
                i++;
                result = Physics2D.Raycast(transform.position, dir, i, obstacles);
            }
            if (i > 1000)
            {
                Debug.LogWarning("Teleport fallido, demasiadas iteraciones.");
            }
            else
            {
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
        }
    }
    
    private void Dash()
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
        while (!result.collider && i <=4)
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
        this.transform.DOMove(targetPosition, 0.5f).OnComplete(CenterPlayer);
    }

    private void Shoot()
    {
        switch (direction)
        {
            case Direction.right:
                _weapon.fire(1, 0);
                break;
            case Direction.left:
                _weapon.fire(-1, 0);
                break;
            case Direction.up:
                _weapon.fire(0, 1);
                break;
            case Direction.down:
                _weapon.fire(0, -1);
                break;
        }
    }

    private void ManageHabilities()
    {
        Debug.Log("Habilidad: " + IndexHabilities);
        switch (IndexHabilities)
        {
            //Aqu� aparecen las habilidades activas.
            case 0:
                Teleport();
                break;
            case 1:
                Shoot();
                break;
			case 2:
				Dash();
                break;
            default:
                break;
        }
    }

    private void OnNextHability()
    {
        IndexHabilities++;
        switch (IndexHabilities)
        {
            //Aqu� se indican las habilidades pasivas.
            case 3:
                OnNextHability();
                break;
            default:
                break;
        }
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