using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    //Enums
    public enum Direction { up, down, left, right }

    //Referencias
    private Animator anim;
    private InputController input;
    private Rigidbody2D _rigidbody2D;
    private Light _lantern;
    private Light _miniLight;

    //Propiedades
    public int speed = 5;
    public Direction direction;
    [HideInInspector]
    public Vector2 targetPosition;
    public LayerMask obstacles;

    //Habilites
    public int[] habilities = {0, 1, 2};
    public float[] totalCooldowns = new float[9];
    private float[] cooldowns = new float[3];
    private int m_indexHabilities;

    //Estado
    public bool playerCanControl = true;
    public bool isDashing = false;

    public int IndexHabilities
    {
        get { return m_indexHabilities; }

        set
        {
            m_indexHabilities = value;
            if (m_indexHabilities < -habilities.Length) m_indexHabilities = 0;
            m_indexHabilities = m_indexHabilities >= habilities.Length
                ? m_indexHabilities %= habilities.Length
                : m_indexHabilities;
            m_indexHabilities = m_indexHabilities < 0
                ? m_indexHabilities = habilities.Length + m_indexHabilities
                : m_indexHabilities;
        }
    }

    private int performedHabilityIndex = 0;

    private Weapon _weapon;

    private void Awake()
    {
        IndexHabilities = 0;
        anim = GetComponentInChildren<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (!_lantern || !_miniLight)
        {
            Light[] lights = GetComponentsInChildren<Light>();
            _lantern = lights[0];
            _miniLight = lights[1];
        }
        direction = Direction.down;
        input = new InputController();
        transform.position = GameManager.CenterVector(transform.position);
        targetPosition = transform.position;
    }

    private void Start()
    {
        input.Player.Habilities.started += (ctx) => ManageHabilities();
        input.Player.ChangeHability.started += (ctx) => OnNextHability();
        DOTween.Init();
        if(habilities[0] == 4)
            FindObjectOfType<PlayerEvents>().haveShield = true;

        //Activar pasivas.
        CheckPasives();
        //Activar habilidad automatica.
        StartHability();
    }

    private void FixedUpdate()
    {
        anim.SetFloat("speed", speed);
    }

    private void Update()
    {
        //Se reduce el tiempo de los cooldowns.
        UpdateCooldowns();

        if (!playerCanControl) return;
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
        if(habilities[IndexHabilities] == 4)
            FindObjectOfType<PlayerEvents>().haveShield = true;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void UpdateCooldowns()
    {
        for (int i = 0; i < cooldowns.Length; i++)
        {
            if(cooldowns[i] > 0) cooldowns[i] -= Time.deltaTime;
        }
    }

    #region Habilities

    private void Teleport()
    {
        if ((Vector2) transform.position != targetPosition || !CheckCooldown()) return;
        StartCoroutine(DoTeleport());
    }

    IEnumerator DoTeleport()
    {
        playerCanControl = false;
        anim.SetTrigger("teleport");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
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
        anim.SetTrigger("teleport");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        SetCooldown();
        playerCanControl = true;
    }

    private void Dash()
    {
        playerCanControl = false;
        isDashing = true;
        anim.SetTrigger("dash");

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
        while (!result.collider && i <= 4)
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
        aux = GameManager.CenterVector(aux);
        targetPosition = aux;
        transform.DOMove(targetPosition, 0.5f).OnComplete(DashFinish);
    }

    private void DashFinish()
    {
        SetCooldown();
        playerCanControl = true;
        isDashing = false;
    }

    private void Shoot()
    {
        switch (direction)
        {
            case Direction.right:
                if (_weapon.Fire(1, 0)) anim.SetTrigger("shoot");
                break;
            case Direction.left:
                if (_weapon.Fire(-1, 0)) anim.SetTrigger("shoot");
                break;
            case Direction.up:
                if (_weapon.Fire(0, 1)) anim.SetTrigger("shoot");
                break;
            case Direction.down:
                if (_weapon.Fire(0, -1)) anim.SetTrigger("shoot");
                break;
        }
    }

    private void PEM(float time)
    {
        if (!CheckCooldown()) return;
        SetCooldown();
        foreach(Enemy e in FindObjectsOfType<Enemy>())
        {
            e.timeParalized += time;
        }
    }

    private void SetLight(bool active)
    {
        _lantern.enabled = active;
        _miniLight.enabled = !active;
    }

    //Call this methods when User choose this Hability
    private void MoreSpeed(int inc)
    {
        speed += inc;
    }
    
    private void LessCountdown(float div)
    {
        for (int i = 0; i < totalCooldowns.Length; i++)
        {
            totalCooldowns[i] /= div;
        }
    }

    //5s mientras este seleccionado
    private void Shield()
    {
        if (!CheckCooldown()) return;
            SetCooldown();
        FindObjectOfType<PlayerEvents>().haveShield = true;
    }

    //Pasivas
    private void CheckPasives()
    {
        for (int i = 0; i < habilities.Length; i++)
        {
            switch (habilities[i])
            {
                case 7:
                    MoreSpeed(2);
                    break;
                case 8:
                    LessCountdown(2);
                    break;
                default:
                    break;
            }
        }
    }

    private void StartHability()
    {
        switch (habilities[0])
        {
            case 4:

                break;
            case 5:
                SetLight(true);
                break;
            default:
                break;
        }
    }

    private void ManageHabilities()
    {
        if (!playerCanControl) return;
        Debug.Log("Habilidad: " + IndexHabilities);
        performedHabilityIndex = IndexHabilities;
        switch (habilities[IndexHabilities])
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
            case 3:
                PEM(3);
                break;
            default:
                break;
        }
    }

    private void OnNextHability()
    {
        OnNextHability(IndexHabilities);
    }

    private void OnNextHability(int initialHability)
    {
        //Desactivar activas automáticas.
        switch (habilities[IndexHabilities])
        {
            case 4:

                break;
            case 5:
                SetLight(false);
                break;
        }
        IndexHabilities++;
        //Para evitar recursión infinita. Si todas son pasivas.
        if (IndexHabilities == initialHability) return;

        //Aquí se indican las habilidades pasivas.
        //También se activan las activas automáticas.
        switch (habilities[IndexHabilities])
        {
            case 4:
                Shield();
                break;
            case 5:
                SetLight(true);
                break;
            case 6:
            case 7:
            case 8:
                OnNextHability(initialHability);
                break;
            default:
                break;
        }
    }

    #endregion

    #region Cooldowns

    public bool CheckCooldown()
    {
        return cooldowns[performedHabilityIndex] <= 0;
    }

    public void SetCooldown()
    {
        cooldowns[performedHabilityIndex] = totalCooldowns[habilities[performedHabilityIndex]];
    }

    #endregion

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

    //Metodos utiles.
    public Vector3 VectorFromDirection()
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
        return dir;
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