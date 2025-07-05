using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMachineState : MonoBehaviour
{
    private EnemyBaseState currentState;
    private EnemyStateFactory factory;
    private float currentDir = 1;
    private PlayerHealth player;
    private Vector3 lastPlayerPos;
    private TimeManager timeManager;
    [SerializeField]
    private Transform checkFloorTran;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private float visionRange = 20.0f;

    public EnemyBaseState CurrentState { get => currentState; set => currentState = value; }
    public float CurrentDir { get => currentDir; set => currentDir = value; }
    public TimeManager TimeManager { get => timeManager; }
    public Transform CheckFloorTran { get => checkFloorTran; }
    public LayerMask GroundMask { get => groundMask; }
    public PlayerHealth Player { get => player; }
    public Vector3 LastPlayerPos { get => lastPlayerPos; set => lastPlayerPos = value; }
    public float VisionRange { get => visionRange; }

    // Start is called before the first frame update
    void Start()
    {
        factory = new EnemyStateFactory(this);
        currentState = factory.Idle();
        currentState.EnterState();

        timeManager = FindAnyObjectByType<TimeManager>();

        if (timeManager == null)
        {
            Debug.Log("TimeManager was not found!");
        }

        player = FindObjectOfType<PlayerHealth>();

        if (player == null)
        {
            Debug.Log("PlayerHealth not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    public bool DetectPlayer()
    {
        //find a way to detect the player.
        //can be multiple ways. i.e hear the player, see the player.

        //see the player:
        Vector3 playerPos = player.transform.position;

        if (Vector3.Distance(transform.position, playerPos) <= visionRange)
        {
            //player is within the range of the enemy's vision.

            //now check if the enemy can see the enemy from their position

            LayerMask playerMask = LayerMask.GetMask("Player");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, visionRange, playerMask))
            {
                lastPlayerPos = player.transform.position;
                return true;
            }

        }

        return false;
    }
}
