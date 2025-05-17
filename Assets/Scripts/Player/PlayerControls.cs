using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private Control control;

    private Vector2 moveInput;
    private bool isDodge;
    private bool isMovementPressed;
    private bool isJumpPressed = false;
    private bool isWeaponSwing;
    private bool isTimeToggled;
    private bool isSpearThrow;
    private Vector2 aimSpearInput;

    private bool isRotating;

    public Vector2 MoveInput { get => moveInput; }
    public bool IsJumpPressed { get => isJumpPressed; }
    public bool IsDodge { get => isDodge; set => isDodge = value; }
    public bool IsMovementPressed { get => isMovementPressed; }
    public bool IsWeaponSwing { get => isWeaponSwing; set => isWeaponSwing = value; }
    public bool IsTimeToggled { get => isTimeToggled; set => isTimeToggled = value; }
    public Vector2 AimSpearInput { get => aimSpearInput; }
    public bool IsSpearThrow { get => isSpearThrow; }
    public bool IsRotating { get => isRotating; set => isRotating = value; }

    private void Awake()
    {
        control = new Control();

        control.PlayerControls.Move.started += OnMoveInput;
        control.PlayerControls.Move.performed += OnMoveInput;
        control.PlayerControls.Move.canceled += OnMoveInput;

        control.PlayerControls.Dodge.started += OnDodge;
        control.PlayerControls.Dodge.canceled += OnDodge;

        control.PlayerControls.Jump.started += OnJump;
        control.PlayerControls.Jump.canceled += OnJump;

        control.PlayerControls.Attack.started += OnAttack;
        control.PlayerControls.Attack.canceled += OnAttack;

        control.PlayerControls.TimeToggle.started += OnTimeToggle;
        //control.PlayerControls.TimeToggle.performed += OnTimeToggle;
        control.PlayerControls.TimeToggle.canceled += OnTimeToggle;

        control.PlayerControls.AimInput.started += OnAimInput;
        control.PlayerControls.AimInput.performed += OnAimInput;
        control.PlayerControls.AimInput.canceled += OnAimInput;

        control.PlayerControls.SpearThrow.started += OnSpearThrow;
        control.PlayerControls.SpearThrow.performed += OnSpearThrow;
        control.PlayerControls.SpearThrow.canceled += OnSpearThrow;

        control.Enable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        isMovementPressed = moveInput.x != 0 || moveInput.y != 0;
    }

    private void OnDodge(InputAction.CallbackContext context)
    {
        isDodge = context.ReadValueAsButton();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        isWeaponSwing = context.ReadValueAsButton();
    }

    private void OnTimeToggle(InputAction.CallbackContext context)
    {
        isTimeToggled = context.ReadValueAsButton();
    }

    private void OnAimInput(InputAction.CallbackContext context)
    {
        aimSpearInput = context.ReadValue<Vector2>();
    }

    private void OnSpearThrow(InputAction.CallbackContext context)
    {
        isSpearThrow = context.ReadValueAsButton();
    }
}
