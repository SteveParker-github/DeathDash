using System.Collections.Generic;

public class EnemyStateFactory
{
    private EnemyMachineState context;
    private Dictionary<EEnemyStates, EnemyBaseState> states = new Dictionary<EEnemyStates, EnemyBaseState>();

    public EnemyStateFactory(EnemyMachineState currentContext)
    {
        context = currentContext;
        states[EEnemyStates.idle] = new EnemyIdleState(context, this);
        states[EEnemyStates.search] = new EnemySearchState(context, this);
        states[EEnemyStates.chase] = new EnemyChaseState(context, this);
        states[EEnemyStates.attack] = new EnemyAttackState(context, this);
    }

    public EnemyBaseState Idle()
    {
        return states[EEnemyStates.idle];
    }

    public EnemyBaseState Search()
    {
        return states[EEnemyStates.search];
    }

    public EnemyBaseState Chase()
    {
        return states[EEnemyStates.chase];
    }

    public EnemyBaseState Attack()
    {
        return states[EEnemyStates.attack];
    }
}
