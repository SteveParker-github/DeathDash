using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private bool isThrown = false;
    private bool hitWall = false;
    private float maxTimeLimit = 2.0f;

    //throwing variables
    private int maxSteps = 49;
    private float mass = 0.1f;
    private LineRenderer lineRenderer;
    private Rigidbody rb;
    private BoxCollider spearCollider;

    //melee
    public bool isMelee = false;
    public float damage = 20.0f;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        rb = GetComponent<Rigidbody>();
        mass = rb.mass;
        spearCollider = GetComponent<BoxCollider>();
        spearCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isThrown && !isMelee) return;

        if (other.tag == "Enemy")
        {
            EnemyController enemy = other.GetComponent<EnemyController>();

            if (enemy == null)
            {
                Debug.Log("Cant find enemyController!");
                return;
            }

            enemy.Hit(damage);

            if (isThrown)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isThrown) return;

        Collider other = collision.collider;

        if (isThrown && other.tag == "Wall")
        {
            gameObject.layer = LayerMask.NameToLayer("Ground");
            hitWall = true;
            Collider collider = GetComponent<Collider>();
            collider.excludeLayers = collider.includeLayers;
            collider.isTrigger = false;
            Destroy(GetComponent<Rigidbody>());
            Destroy(gameObject, maxTimeLimit);
        }
    }
    public void ShowAimArc(float force)
    {
        lineRenderer.enabled = true;
        List<Vector3> renderPoints = new List<Vector3>();

        Vector3 direction = transform.forward;
        Vector3 launchPos = transform.position;
        float vel = force / mass * Time.fixedDeltaTime;

        for (int i = 0; i < maxSteps; i++)
        {
            Vector3 calcPos = launchPos + direction * vel * i * 0.1f;
            calcPos.y += Physics2D.gravity.y * 0.5f * Mathf.Pow(i * 0.1f, 2);

            renderPoints.Add(calcPos);
        }

        lineRenderer.positionCount = renderPoints.Count;
        lineRenderer.SetPositions(renderPoints.ToArray());
    }

    public void HideLineRenderer()
    {
        lineRenderer.enabled = false;
    }

    public void Throw(float force)
    {
        isThrown = true;
        transform.parent = null;
        spearCollider.isTrigger = false;
        rb.useGravity = true;
        Vector3 forceDir = transform.forward * force;
        rb.AddForce(forceDir, ForceMode.Force);

        StartCoroutine(SpearRotate());
    }

    public void Throw(float force, quaternion dir)
    {
        isThrown = true;
        transform.rotation = dir;
        Debug.Log(dir + "dir, spear:" + transform.rotation);

        transform.parent = null;
        spearCollider.isTrigger = false;
        Vector3 forceDir = transform.forward * force;
        rb.AddForce(forceDir, ForceMode.Force);
    }

    private IEnumerator SpearRotate()
    {
        while (!hitWall)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            yield return null;
        }
    }
}
