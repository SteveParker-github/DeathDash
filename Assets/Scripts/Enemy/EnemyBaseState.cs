public abstract class EnemyBaseState
{
    private EnemyMachineState ctx;
    private EnemyStateFactory factory;

    protected EnemyMachineState Ctx { get => ctx; }
    protected EnemyStateFactory Factory { get => factory; }

    public EnemyBaseState(EnemyMachineState currentContext, EnemyStateFactory enemyStateFactory)
    {
        ctx = currentContext;
        factory = enemyStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();

    protected void SwitchState(EnemyBaseState newState)
    {
        ExitState();
        newState.EnterState();
        ctx.CurrentState = newState;
    }
}
