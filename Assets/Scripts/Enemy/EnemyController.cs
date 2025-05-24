using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform pos;
    private EnemySpawnerController enemySpawnerController;
    private float currentDir = 1;
    [SerializeField]
    private Transform checkFloorTran;
    public LayerMask groundMask;
    private TimeManager timeManager;
    private SpawnPoint spawnPoint;
    public float maxHealth = 50.0f;
    private float currentHealth;
    private bool beenHit = false;

    private void Awake()
    {
        pos = transform;
        currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnerController = FindAnyObjectByType<EnemySpawnerController>();

        timeManager = FindAnyObjectByType<TimeManager>();

        if (timeManager == null)
        {
            Debug.Log("TimeManager was not found!");
        }
    }

    public void Hit(float damage)
    {
        if (beenHit) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth == 0)
        {
            Died();
            return;
        }

        StartCoroutine(GettingHit());
    }

    private IEnumerator GettingHit()
    {
        beenHit = true;
        yield return new WaitForSecondsRealtime(3.0f);
        beenHit = false;
    }

    private void Died()
    {
        //enemySpawnerController.triggerRespawn(this);
        spawnPoint.EnemyDied();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!CheckFloor())
        {
            //turn around and move;
            transform.Rotate(new Vector3(0, 180, 0));
            currentDir *= -1;
            return;
        }

        Vector3 forwardDir = transform.forward * currentDir * 4 * Time.deltaTime * timeManager.currentTime;

        transform.Translate(forwardDir);
    }

    private bool CheckFloor()
    {
        float groundedRadius = 0.2f;
        bool isGrounded = Physics.CheckSphere(checkFloorTran.position, groundedRadius, groundMask, QueryTriggerInteraction.Ignore);
        return isGrounded;
    }

    public void NewSpawnPoint(SpawnPoint spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }
}
