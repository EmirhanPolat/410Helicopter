using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (!(collision.gameObject.tag == "pad"))
        {
            currentHealth -= 20;
            healthController.SetHealth(currentHealth);
        }

        if (currentHealth == 0)
        {
            StartCoroutine(LoadGameOverAfterDelay(2));
        }

        if ((collision.gameObject.name == "Plane (1)"))
        {
            StartCoroutine(LoadGameOverAfterDelay(2));
        }
    }

    IEnumerator LoadGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameOver");
    }
}