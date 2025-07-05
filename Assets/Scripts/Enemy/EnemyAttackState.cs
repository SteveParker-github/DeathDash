using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyMachineState currentContext, EnemyStateFactory enemyStateFactory)
    : base(currentContext, enemyStateFactory)
    {
    }
    public override void EnterState()
    {
        Debug.Log("Hit the player!");
        Ctx.Player.Hit(1);
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
        SwitchState(Factory.Idle());
    }
}