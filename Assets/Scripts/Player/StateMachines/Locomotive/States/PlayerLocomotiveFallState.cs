using UnityEngine;

public class PlayerLocomotiveFallState : PlayerLocomotiveBaseState, IRootState
{
    public PlayerLocomotiveFallState(PlayerLocomotiveStateMachine playerController, PlayerLocomotiveStateFactory playerStateFactory)
        : base(playerController, playerStateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        Debug.Log("Falling State");
        InitializeChildState();
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
        if (Ctx.IsGrounded())
        {
            SwitchState(Factory.Grounded());
        }

        if (Ctx.PlayerControls.IsDodge && Ctx.PlayerControls.IsMovementPressed)
        {
            SwitchState(Factory.Dodge());
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

    public void HandleGravity()
    {
        float previousYVelocity = Ctx.CurrentMovementY;

        Ctx.CurrentMovementY = Ctx.CurrentMovementY + Ctx.Gravity * Time.deltaTime;
        Ctx.AppliedMovementY = Mathf.Max((previousYVelocity + Ctx.CurrentMovementY) * 0.5f, Ctx.fallSpeed);
    }

}
