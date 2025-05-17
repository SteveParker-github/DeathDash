using UnityEngine;

public class PlayerLocomotiveDodgeState : PlayerLocomotiveBaseState, IRootState
{
    private float dodgeTimer = 0.0f;
    private float MAXDODGETIME = .25f;

    public PlayerLocomotiveDodgeState(PlayerLocomotiveStateMachine playerController, PlayerLocomotiveStateFactory playerStateFactory)
        : base(playerController, playerStateFactory)
    {
        IsRootState = true;
    }

    public void HandleGravity()
    {
        Ctx.CurrentMovementY = 0;
        Ctx.AppliedMovementY = 0;
    }

    public override void EnterState()
    {
        Ctx.PlayerControls.IsDodge = false;
        Ctx.IsDodging = true;
        dodgeTimer = MAXDODGETIME;
        Vector2 dir = Ctx.PlayerControls.MoveInput;
        Ctx.DodgeDir = float.IsNegative(dir.x) ? -1 : 1;
    }

    public override void UpdateState()
    {
        Dodge();

        dodgeTimer -= Time.deltaTime;

        CheckSwitchState();
    }

    public override void ExitState()
    {
        Ctx.SetDodgeCooldown();
        Ctx.AppliedMovementZ = 0;
    }

    public override void CheckSwitchState()
    {
        if (dodgeTimer > 0.0f) return;

        if (Ctx.IsGrounded())
        {
            SwitchState(Factory.Grounded());
        }
        else
        {
            SwitchState(Factory.Fall());
        }

    }

    public override void InitializeChildState()
    {
    }

    private void Dodge()
    {
        float moveSpeed = Ctx.WALKSPEED * 4 * Ctx.DodgeDir;
        Ctx.AppliedMovementZ = moveSpeed;
    }
}
