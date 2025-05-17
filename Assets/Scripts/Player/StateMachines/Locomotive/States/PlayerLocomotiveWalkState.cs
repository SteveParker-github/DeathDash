using UnityEngine;

public class PlayerLocomotiveWalkState : PlayerLocomotiveBaseState
{
    public PlayerLocomotiveWalkState(PlayerLocomotiveStateMachine playerController, PlayerLocomotiveStateFactory playerStateFactory)
        : base(playerController, playerStateFactory)
    {

    }
    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        MovePlayer();
        CheckSwitchState();
    }

    public override void ExitState()
    {
    }

    public override void CheckSwitchState()
    {
        if (!Ctx.PlayerControls.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void InitializeChildState()
    {

    }

    private void MovePlayer()
    {
        float moveDir = 1;

        if (float.IsNegative(Ctx.PlayerControls.MoveInput.x))
        {
            moveDir *= -1;
        }

        Ctx.AppliedMovementZ = moveDir * Ctx.WALKSPEED;
    }

}
