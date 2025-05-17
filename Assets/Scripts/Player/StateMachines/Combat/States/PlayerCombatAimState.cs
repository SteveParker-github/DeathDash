using UnityEngine;

public class PlayerCombatAimState : PlayerCombatBaseState
{
    private float angle;
    private float angleMin = -45.0f;
    private float angleMax = 45.0f;
    private float forceMin = 600.0f;
    private float forceMax = 1200.0f;
    private float forceDefault = 1000.0f;
    private float angleDefault = -10f;
    private bool cancelThrow = true;
    public PlayerCombatAimState(PlayerCombatMachineState currentContext, PlayerCombatStateFactory playerCombatStateFactory)
    : base(currentContext, playerCombatStateFactory)
    {

    }
    
    public override void EnterState()
    {
        angle = angleDefault;
        Ctx.Force = forceDefault;
        Time.timeScale = 0.2f;
    }

    public override void UpdateState()
    {
        if (Ctx.PlayerControls.IsRotating)
        {
            Ctx.CurrentSpear.HideLineRenderer();
        }
        else
        {
            Aim();
        }

        CheckSwitchState();
    }

    public override void ExitState()
    {
        Ctx.CurrentSpear.HideLineRenderer();
        Time.timeScale = 1.0f;

        if (cancelThrow)
        {
            Vector3 spearRot = Ctx.CurrentSpear.transform.localRotation.eulerAngles;
            spearRot.x = angleDefault;
            Ctx.CurrentSpear.transform.localRotation = Quaternion.Euler(spearRot);
            Ctx.Force = forceDefault;
        }
    }

    public override void CheckSwitchState()
    {
        if (!Ctx.PlayerControls.IsTimeToggled)
        {
            SwitchState(Factory.Idle());
        }

        if (Ctx.PlayerControls.IsSpearThrow && !Ctx.PlayerControls.IsRotating)
        {
            cancelThrow = false;
            SwitchState(Factory.ThrowSpear());
        }
    }

    private void Aim()
    {
        Vector2 inputAiming = Ctx.PlayerControls.AimSpearInput.normalized;

        float time = Time.deltaTime / Time.timeScale;

        angle += inputAiming.y * time * 80;
        angle = Mathf.Clamp(angle, angleMin, angleMax);

        Ctx.Force += inputAiming.x * time * 200;
        Ctx.Force = Mathf.Clamp(Ctx.Force, forceMin, forceMax);

        Vector3 spearRot = Ctx.CurrentSpear.transform.localRotation.eulerAngles;
        spearRot.x = angle;
        Ctx.CurrentSpear.transform.localRotation = Quaternion.Euler(spearRot);
        Ctx.CurrentSpear.ShowAimArc(Ctx.Force);
    }
}
