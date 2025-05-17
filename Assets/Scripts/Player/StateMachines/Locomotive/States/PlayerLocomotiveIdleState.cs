using UnityEngine;

public class PlayerLocomotiveIdleState : PlayerLocomotiveBaseState
{
    public PlayerLocomotiveIdleState(PlayerLocomotiveStateMachine playerController, PlayerLocomotiveStateFactory playerStateFactory)
    : base(playerController, playerStateFactory)
    {

    }
    public override void EnterState()
    {
        Ctx.AppliedMovementZ = 0.0f;
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
        if (Ctx.PlayerControls.IsMovementPressed)
        {
            SwitchState(Factory.Walk());
        }
    }

    public override void InitializeChildState()
    {

    }

}
