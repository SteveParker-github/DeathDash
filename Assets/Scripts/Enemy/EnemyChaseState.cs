using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private bool lostPlayer;
    private bool CantMove;
    public EnemyChaseState(EnemyMachineState currentContext, EnemyStateFactory enemyStateFactory)
    : base(currentContext, enemyStateFactory)
    {
    }
    public override void EnterState()
    {
        //are we facing the player?
        Vector3 heading = Ctx.LastPlayerPos - Ctx.transform.position;
        heading.Normalize();
        float dot = Vector3.Dot(heading, Ctx.transform.TransformDirection(Vector3.forward));
        if (dot == -1)
        {
            Ctx.transform.Rotate(new Vector3(0, 180, 0));
            Ctx.CurrentDir *= -1;
        }

        lostPlayer = false;
        CantMove = false;
    }

    public override void UpdateState()
    {
        Move();
        FindPlayer();
        CheckSwitchState();
    }

    public override void ExitState()
    {
    }

    public override void CheckSwitchState()
    {

        if (Vector3.Distance(Ctx.Player.transform.position, Ctx.transform.position) < 2.0f)
        {
            SwitchState(Factory.Attack());
            return;
        }

        if (lostPlayer && CantMove)
        {
            SwitchState(Factory.Search());
        }
    }

    private void Move()
    {
        if (!CheckFloor())
        {
            CantMove = true;
            Debug.Log("can't walk any further!");
            return;
        }

        Vector3 forwardDir = Ctx.transform.forward * Ctx.CurrentDir * 4 * Time.deltaTime * Ctx.TimeManager.currentTime;

        Ctx.transform.Translate(forwardDir);
    }

    private bool CheckFloor()
    {
        float groundedRadius = 0.2f;
        bool isGrounded = Physics.CheckSphere(Ctx.CheckFloorTran.position, groundedRadius, Ctx.GroundMask, QueryTriggerInteraction.Ignore);
        return isGrounded;
    }

    private void FindPlayer()
    {
        if (Ctx.DetectPlayer())
        {
            Vector3 heading = Ctx.LastPlayerPos - Ctx.transform.position;
            heading.Normalize();
            float dot = Vector3.Dot(heading, Ctx.transform.TransformDirection(Vector3.forward));
            if (dot == -1)
            {
                Ctx.transform.Rotate(new Vector3(0, 180, 0));
                Ctx.CurrentDir *= -1;
            }
        }
        else
        {
            lostPlayer = true;
        }
    }
}