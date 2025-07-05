using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private PlayerHealthUI healthUI;
    private int maxHealth = 10;
    private int currentHealth;
    private bool beenhit = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        healthUI.CreateHearts(currentHealth / 2);
    }

    public void Hit(int damage)
    {
        if (beenhit) return;

        int totalDamage = Mathf.Min(damage, currentHealth);
        currentHealth -= totalDamage;

        healthUI.UpdateHealth(currentHealth);

        if (currentHealth == 0)
        {
            Debug.Log("Player is dead!");
            return;
        }

        StartCoroutine(BeenHit());
    }

    private IEnumerator BeenHit()
    {
        beenhit = true;
        yield return new WaitForSeconds(1.0f);
        beenhit = false;
    }
}
