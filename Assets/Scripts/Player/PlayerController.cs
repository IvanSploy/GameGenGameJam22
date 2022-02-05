using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    //Referencias
    private InputController input;

    //Propiedades
    public int speed = 5;

    private void Awake()
    {
        input = new InputController();
        input.Player.Movement.performed += (ctx) => OnMove(ctx);
        input.Player.Movement.canceled += (ctx) => OnStop();
    }

    public void OnMove(CallbackContext ctx)
    {
        GetComponent<Rigidbody2D>().velocity = ctx.ReadValue<Vector2>() * speed;
    }

    public void OnStop()
    {

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
