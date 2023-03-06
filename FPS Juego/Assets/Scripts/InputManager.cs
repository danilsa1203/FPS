using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerinput;
    private PlayerInput.OnFootActions onfoot;

    private PlayerMotor motor;

    private PlayerLook look;
  
    private void Awake()
    {
        playerinput = new PlayerInput();
        onfoot = playerinput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onfoot.Jump.performed += ctx => motor.Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onfoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        look.ProcessLook(onfoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onfoot.Enable();
    }

    private void OnDisable()
    {
        onfoot.Disable();
    }

}
