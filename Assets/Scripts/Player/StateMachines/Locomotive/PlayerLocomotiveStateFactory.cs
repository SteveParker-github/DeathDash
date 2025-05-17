using System.Collections.Generic;

public class PlayerLocomotiveStateFactory
{
    private PlayerLocomotiveStateMachine context;
    private Dictionary<EPlayerLocomotiveStates, PlayerLocomotiveBaseState> states = new Dictionary<EPlayerLocomotiveStates, PlayerLocomotiveBaseState>();

    public PlayerLocomotiveStateFactory(PlayerLocomotiveStateMachine currentContext)
    {
        context = currentContext;
        states[EPlayerLocomotiveStates.idle] = new PlayerLocomotiveIdleState(context, this);
        states[EPlayerLocomotiveStates.walk] = new PlayerLocomotiveWalkState(context, this);
        states[EPlayerLocomotiveStates.dodge] = new PlayerLocomotiveDodgeState(context, this);
        states[EPlayerLocomotiveStates.grounded] = new PlayerLocomotiveGroundedState(context, this);
        states[EPlayerLocomotiveStates.jump] = new PlayerLocomotiveJumpState(context, this);
        states[EPlayerLocomotiveStates.fall] = new PlayerLocomotiveFallState(context, this);
    }

    public PlayerLocomotiveBaseState Idle()
    {
        return states[EPlayerLocomotiveStates.idle];
    }

    public PlayerLocomotiveBaseState Walk()
    {
        return states[EPlayerLocomotiveStates.walk];
    }

    public PlayerLocomotiveBaseState Dodge()
    {
        return states[EPlayerLocomotiveStates.dodge];
    }

    public PlayerLocomotiveBaseState Grounded()
    {
        return states[EPlayerLocomotiveStates.grounded];
    }

    public PlayerLocomotiveBaseState Jump()
    {
        return states[EPlayerLocomotiveStates.jump];
    }

    public PlayerLocomotiveBaseState Fall()
    {
        return states[EPlayerLocomotiveStates.fall];
    }
}
