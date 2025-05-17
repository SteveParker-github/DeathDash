using UnityEngine;

public class PlayerCombatThrowSpearState : PlayerCombatBaseState
{
    private bool finishedAnimation;
    public PlayerCombatThrowSpearState(PlayerCombatMachineState currentContext, PlayerCombatStateFactory playerCombatStateFactory)
    : base(currentContext, playerCombatStateFactory)
    {
        
    }
    public override void EnterState()
    {
        finishedAnimation = false;
        Ctx.Anim.Play("Throw");
    }

    public override void UpdateState()
    {
        finishedAnimation = Ctx.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;

        CheckSwitchState();
    }

    public override void ExitState()
    {
        Ctx.Anim.Play("idle");
        Debug.Log("Leave Throw State");
    }

    public override void CheckSwitchState()
    {
        if (finishedAnimation)
        {
            SwitchState(Factory.Idle());
        }
    }
}
