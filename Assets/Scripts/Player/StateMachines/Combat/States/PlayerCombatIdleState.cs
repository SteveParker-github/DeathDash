using UnityEngine;

public class PlayerCombatIdleState : PlayerCombatBaseState
{
    public PlayerCombatIdleState(PlayerCombatMachineState currentContext, PlayerCombatStateFactory playerCombatStateFactory)
    : base(currentContext, playerCombatStateFactory)
    {

    }
    public override void EnterState()
    {

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
        if (Ctx.PlayerControls.IsSpearThrow && Ctx.HaveSpear)
        {
            SwitchState(Factory.ThrowSpear());
        }

        if (Ctx.PlayerControls.IsWeaponSwing && Ctx.CanAttack)
        {
            SwitchState(Factory.Attack());
        }
    }
}
