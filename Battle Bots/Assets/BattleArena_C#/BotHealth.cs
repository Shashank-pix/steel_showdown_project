using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class BotHealth : MonoBehaviour
{
    public float maxHealth = 20f; // Maximum health
    public Slider healthBar; // Reference to the health bar UI

    public string WinnerScene;//Scene Index

    private float currentHealth;

    private void Start()
    {
        // Initialize health and health bar
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth; // Match slider max value with maxHealth
            healthBar.value = currentHealth; // Initialize slider value
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; // Reduce health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health stays in bounds

        // Update the health bar UI
        if (healthBar != null)
        {
            healthBar.value = currentHealth; // Directly update slider value
        }

        Debug.Log($"{gameObject.name} Health: {currentHealth}/{maxHealth}");

        // Check if the bot's health is depleted
        if (currentHealth <= 0)
        {
            //Die();
            StartCoroutine(HandleDeath());
        }
    }

     private IEnumerator HandleDeath()
    {
        Debug.Log($"{gameObject.name} has been destroyed.");
       // Destroy(gameObject); //Remove Bot from scene
        yield return new WaitForSeconds(1f); // 1-second delay
        if (!string.IsNullOrEmpty(WinnerScene))
        {
            SceneManager.LoadScene(WinnerScene); // Load the winner scene
        }
        else
        {
            Debug.LogError("WinnerScene not assigned!");
        }
    }

    // private void Die()
    // {
    //     Debug.Log($"{gameObject.name} has been destroyed.");
    //     Destroy(gameObject); // Remove the bot from the scene

    //     if(!string.IsNullOrEmpty(WinnerScene))
    //     {
    //         Invoke(nameof(LoadScene),1f);
    //     }
    // }

    // void LoadScene()
    // {
    //     SceneManager.LoadScene(WinnerScene);
    // }
}
