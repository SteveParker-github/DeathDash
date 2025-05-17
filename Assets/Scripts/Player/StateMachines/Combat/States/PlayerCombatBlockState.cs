using UnityEngine;

public class PlayerCombatBlockState : PlayerCombatBaseState
{
    public PlayerCombatBlockState(PlayerCombatMachineState currentContext, PlayerCombatStateFactory playerCombatStateFactory)
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

    }
}
