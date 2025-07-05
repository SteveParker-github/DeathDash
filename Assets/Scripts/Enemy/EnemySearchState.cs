using UnityEngine;

public class EnemySearchState : EnemyBaseState
{
    private bool foundPlayer;
    private float timer;
    private float MaxTime = 2f;
    private int turns;
    private int maxTurns = 2;
    public EnemySearchState(EnemyMachineState currentContext, EnemyStateFactory enemyStateFactory)
    : base(currentContext, enemyStateFactory)
    {
    }
    public override void EnterState()
    {
        Debug.Log("Lost the player! search around for them!");
        timer = 0;
    }

    public override void UpdateState()
    {
        CheckDirection();
        SwitchDirection();
        CheckSwitchState();
    }

    public override void ExitState()
    {
    }

    public override void CheckSwitchState()
    {
        if (foundPlayer)
        {
            Debug.Log("Found them, get them!");
            SwitchState(Factory.Chase());
            return;
        }

        if (turns == maxTurns)
        {
            Debug.Log("Give up cant find them!");
            SwitchState(Factory.Idle());
            return;
        }
    }

    private void CheckDirection()
    {
        foundPlayer = Ctx.DetectPlayer();
    }

    private void SwitchDirection()
    {
        timer += Time.deltaTime;

        if (timer < MaxTime)
        {
            return;
        }

        Ctx.transform.Rotate(new Vector3(0, 180, 0));
        Ctx.CurrentDir *= -1;
        timer = 0;
        turns++;
    }
}