using System.Collections.Generic;

public class PlayerCombatStateFactory
{
    private PlayerCombatMachineState context;
    private Dictionary<EPlayerCombatStates, PlayerCombatBaseState> states = new Dictionary<EPlayerCombatStates, PlayerCombatBaseState>();

    public PlayerCombatStateFactory(PlayerCombatMachineState currentContext)
    {
        context = currentContext;
        states[EPlayerCombatStates.idle] = new PlayerCombatIdleState(context, this);
        states[EPlayerCombatStates.attack] = new PlayerCombatAttackState(context, this);
        states[EPlayerCombatStates.block] = new PlayerCombatBlockState(context, this);
        states[EPlayerCombatStates.aim] = new PlayerCombatAimState(context, this);
        states[EPlayerCombatStates.throwSpear] = new PlayerCombatThrowSpearState(context, this);
    }

    public PlayerCombatBaseState Idle()
    {
        return states[EPlayerCombatStates.idle];
    }

    public PlayerCombatBaseState Attack()
    {
        return states[EPlayerCombatStates.attack];
    }

    public PlayerCombatBaseState Block()
    {
        return states[EPlayerCombatStates.block];
    }

    public PlayerCombatBaseState Aim()
    {
        return states[EPlayerCombatStates.aim];
    }

    public PlayerCombatBaseState ThrowSpear()
    {
        return states[EPlayerCombatStates.throwSpear];
    }
}
