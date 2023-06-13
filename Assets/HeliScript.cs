using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxPower = 100;
    public int currentHealth;
    public int currentPower;

    public HealthController healthController;
    private void Start()
    {
        currentHealth = maxHealth;
        healthController.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthController.SetHealth(currentHealth);
    }
}
