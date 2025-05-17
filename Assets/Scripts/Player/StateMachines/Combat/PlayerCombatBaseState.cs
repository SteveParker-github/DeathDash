public abstract class PlayerCombatBaseState
{
    private PlayerCombatMachineState ctx;
    private PlayerCombatStateFactory factory;

    protected PlayerCombatMachineState Ctx { get => ctx; }
    protected PlayerCombatStateFactory Factory { get => factory; }

    public PlayerCombatBaseState(PlayerCombatMachineState currentContext, PlayerCombatStateFactory playerCombatStateFactory)
    {
        ctx = currentContext;
        factory = playerCombatStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();

    protected void SwitchState(PlayerCombatBaseState newState)
    {
        ExitState();
        newState.EnterState();
        ctx.CurrentState = newState;
    }
}
