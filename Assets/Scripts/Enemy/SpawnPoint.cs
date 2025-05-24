using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //max amount of enemies to spawn
    public int maxLimit = 1;
    //how many enemies to spawn at the start
    public int startLimit = 1;
    //which enemies to spawn
    public GameObject[] spawnableEnemies;
    //how long before able to spawn another enemy
    public float cooldown = 5;
    //counts how many enemies have spawn from this spawn point.
    private int enemyCounter = 0;
    private bool currentSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startLimit; i++)
        {
            StartCoroutine(StartRespawn());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpawning) return;

        if (enemyCounter < maxLimit)
        {
            StartCoroutine(StartRespawn());
        }
    }

    private IEnumerator StartRespawn()
    {
        enemyCounter++;
        currentSpawning = true;
        yield return new WaitForSecondsRealtime(cooldown);
        GameObject enemyPrefab = spawnableEnemies[Random.Range(0, spawnableEnemies.Length - 1)];
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        EnemyController ec = enemy.GetComponent<EnemyController>();
        ec.NewSpawnPoint(this);
        currentSpawning = false;
    }

    public void EnemyDied()
    {
        enemyCounter--;
    }
}
