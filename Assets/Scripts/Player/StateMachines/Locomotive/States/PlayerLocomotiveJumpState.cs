using System.Collections;
using UnityEngine;

public class PlayerLocomotiveJumpState : PlayerLocomotiveBaseState, IRootState
{
    bool isFalling;
    public PlayerLocomotiveJumpState(PlayerLocomotiveStateMachine playerController, PlayerLocomotiveStateFactory playerStateFactory)
    : base(playerController, playerStateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        Debug.Log("Jump Start");
        InitializeChildState();
        HandleJump();
    }

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchState();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        if (Ctx.PlayerControls.IsDodge && Ctx.PlayerControls.IsMovementPressed)
        {
            SwitchState(Factory.Dodge());
        }

        if (!isFalling) return;
        if (Ctx.IsGrounded())
        {
            SwitchState(Factory.Grounded());
        }
    }

    public override void InitializeChildState()
    {
        if (!Ctx.PlayerControls.IsMovementPressed)
        {
            SetChildState(Factory.Idle());
        }
        else
        {
            SetChildState(Factory.Walk());
        }
    }

    private void HandleJump()
    {
        Ctx.IsJumping = true;
        Ctx.CurrentMovementY = Ctx.InitalJumpVelocity;
        Ctx.AppliedMovementY = Ctx.InitalJumpVelocity;
    }

    public void HandleGravity()
    {
        isFalling = Ctx.CurrentMovementY <= 0.0f || !Ctx.PlayerControls.IsJumpPressed;
        float fallMultiplier = 2.0f;
        float previousYVelocity = Ctx.CurrentMovementY;

        if (isFalling)
        {
            Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.Gravity * fallMultiplier * Time.deltaTime);
            Ctx.AppliedMovementY = Mathf.Max((previousYVelocity + Ctx.CurrentMovementY) * 0.5f, Ctx.fallSpeed);
        }
        else
        {
            Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.Gravity * Time.deltaTime);
            Ctx.AppliedMovementY = (previousYVelocity + Ctx.CurrentMovementY) * 0.5f;
        }
    }

}
