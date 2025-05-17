using UnityEngine;

public class PlayerCombatAttackState : PlayerCombatBaseState
{
    private bool finishedAttack;
    
    public PlayerCombatAttackState(PlayerCombatMachineState currentContext, PlayerCombatStateFactory playerCombatStateFactory)
    : base(currentContext, playerCombatStateFactory)
    {

    }
    public override void EnterState()
    {
        int attackCounter = Ctx.Anim.GetInteger("Counter");
        string attackName = "Attack" + (attackCounter + 1);
        Ctx.Anim.Play(attackName);

        attackCounter++;
        attackCounter = attackCounter % 3;
        Ctx.Anim.SetInteger("Counter", attackCounter);

        Ctx.CurrentSpear.isMelee = true;
    }

    public override void UpdateState()
    {
        CheckFinishAttack();
        CheckSwitchState();
    }

    public override void ExitState()
    {
        Ctx.Anim.Play("idle");
        Ctx.CurrentSpear.isMelee = false;
        Ctx.StartComboCountdown();
    }

    public override void CheckSwitchState()
    {
        if (finishedAttack)
        {
            SwitchState(Factory.Idle());
        }
    }

    private void CheckFinishAttack()
    {
        //check if the animator has finished its attack animation.
        finishedAttack = Ctx.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }
}
