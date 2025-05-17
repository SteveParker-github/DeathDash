using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private float spawnRate = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void triggerRespawn(EnemyController enemyController)
    {
        Vector3 pos = enemyController.pos.position;
        Quaternion rot = enemyController.pos.rotation;
        StartCoroutine(RespawnEnemy(pos, rot));
        Destroy(enemyController.gameObject);
    }

    private IEnumerator RespawnEnemy(Vector3 pos, Quaternion rot)
    {
        Vector3 newPos = pos;
        Quaternion newRot = rot;
        yield return new WaitForSecondsRealtime(spawnRate);

        Instantiate(prefab, newPos, newRot, transform);
    }
}
