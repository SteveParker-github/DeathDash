using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Transform pos;
    [SerializeField]
    private SpawnPoint spawnPoint;
    public float maxHealth = 50.0f;
    private float currentHealth;
    private EElements weakness;
    private EElements strength;
    [SerializeField]
    private Material[] skinTypes;
    [SerializeField]
    private Renderer rend;

    //ui
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Slider slider;

    private void Awake()
    {
        pos = transform;
        currentHealth = maxHealth;
        canvas.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Hit(float damage, EElements weaponType)
    {
        float totalDamage = damage;

        if (weakness == weaponType)
        {
            totalDamage *= 2;
        }

        if (strength == weaponType)
        {
            totalDamage *= 0.5f;
        }

        currentHealth -= totalDamage;
        currentHealth = Mathf.Max(currentHealth, 0);
        canvas.enabled = true;
        slider.value = currentHealth / maxHealth;

        if (currentHealth == 0)
        {
            Died();
            return;
        }
    }

    private void Died()
    {
        spawnPoint.EnemyDied();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Spawn(SpawnPoint spawnPoint, EElements weakness, EElements strength)
    {
        this.spawnPoint = spawnPoint;
        this.weakness = weakness;
        this.strength = strength;

        rend.material = skinTypes[(int)strength];
    }
}
