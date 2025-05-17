using UnityEngine;
public class PlayerLocomotiveGroundedState : PlayerLocomotiveBaseState, IRootState
{
    public PlayerLocomotiveGroundedState(PlayerLocomotiveStateMachine playerController, PlayerLocomotiveStateFactory playerStateFactory)
        : base(playerController, playerStateFactory)
    {
        IsRootState = true;
    }
    public void HandleGravity()
    {
        Ctx.CurrentMovementY = Ctx.Gravity;
        Ctx.AppliedMovementY = Ctx.Gravity;
    }
    
    public override void EnterState()
    {
        InitializeChildState();
        HandleGravity();
    }

    public override void UpdateState()
    {
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

        if (Ctx.PlayerControls.IsJumpPressed)
        {
            SwitchState(Factory.Jump());
        }

        if (!Ctx.IsGrounded())
        {
            SwitchState(Factory.Fall());
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

}
