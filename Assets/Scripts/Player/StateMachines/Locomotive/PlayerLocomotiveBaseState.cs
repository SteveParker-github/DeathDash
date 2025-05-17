public abstract class PlayerLocomotiveBaseState
{
    private bool isRootState = false;
    private PlayerLocomotiveStateMachine ctx;
    private PlayerLocomotiveStateFactory factory;
    private PlayerLocomotiveBaseState currentChildState;
    private PlayerLocomotiveBaseState currentParentState;

    protected bool IsRootState { set => isRootState = value; }
    protected PlayerLocomotiveStateMachine Ctx { get => ctx; }
    protected PlayerLocomotiveStateFactory Factory { get => factory; }

    public PlayerLocomotiveBaseState(PlayerLocomotiveStateMachine currentContext, PlayerLocomotiveStateFactory playerStateFactory)
    {
        ctx = currentContext;
        factory = playerStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
    public abstract void InitializeChildState();

    public void UpdateStates()
    {
        UpdateState();

        if (currentChildState != null)
        {
            currentChildState.UpdateStates();
        }
    }

    protected void SwitchState(PlayerLocomotiveBaseState newState)
    {
        ExitState();

        newState.EnterState();

        if (isRootState)
        {
            ctx.CurrentState = newState;
        }
        else
        {
            currentParentState?.SetChildState(newState);
        }
    }

    protected void SetParentState(PlayerLocomotiveBaseState newParentState)
    {
        currentParentState = newParentState;
    }

    protected void SetChildState(PlayerLocomotiveBaseState newChildState)
    {
        currentChildState = newChildState;
        newChildState.SetParentState(this);
    }
}
