using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthController healthController;
    private void Start()
    {
        currentHealth = maxHealth;
        healthController.SetMaxHealth(maxHealth);
    }

    void OnCollisionEnter(Collision collision)
    {
        currentHealth -= 20;
        healthController.SetHealth(currentHealth);
    }
}
