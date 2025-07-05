using UnityEditor;
using UnityEngine;
public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyMachineState currentContext, EnemyStateFactory enemyStateFactory)
    : base(currentContext, enemyStateFactory)
    {
    }
    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        Move();
        DetectPlayer();
        CheckSwitchState();
    }

    public override void ExitState()
    {
    }

    public override void CheckSwitchState()
    {
        if (DetectPlayer())
        {
            SwitchState(Factory.Chase());
        }

    }

    private void Move()
    {
        if (!CheckFloor())
        {
            //turn around and move;
            Ctx.transform.Rotate(new Vector3(0, 180, 0));
            Ctx.CurrentDir *= -1;
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

    private bool DetectPlayer()
    {
        //find a way to detect the player.
        //can be multiple ways. i.e hear the player, see the player.

        //see the player:
        Vector3 playerPos = Ctx.Player.transform.position;

        if (Vector3.Distance(Ctx.transform.position, playerPos) <= Ctx.VisionRange)
        {
            //player is within the range of the enemy's vision.

            //now check if the enemy can see the enemy from their position

            LayerMask playerMask = LayerMask.GetMask("Player");
            RaycastHit hit;
            if (Physics.Raycast(Ctx.transform.position, Ctx.transform.TransformDirection(Vector3.forward), out hit, Ctx.VisionRange, playerMask))
            {
                Ctx.LastPlayerPos = Ctx.Player.transform.position;
                return true;
            }

        }

        return false;
    }
}