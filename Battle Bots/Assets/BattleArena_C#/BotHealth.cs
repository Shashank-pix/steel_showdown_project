using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;



public class BotHealth : MonoBehaviour
{
    
  [SerializeField] private float maxHealth = 20f;
    public Slider healthBar;
    public string WinnerScene = "WinnerMenu";

    private float currentHealth;
    public float lerpSpeed = 5f;
    private Rigidbody _rigidbody;

    public ParticleSystem Smoke;
    public ParticleSystem Explosion;

    private PlayerInput playerInput; // Reference to PlayerInput component

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        _rigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>(); // Get PlayerInput component
    }

    public void TakeDamage(float damageAmount)
    {
        float newHealth = Mathf.Clamp(currentHealth - damageAmount, 0, maxHealth);

        // Smoothly update health bar using Lerp
        StartCoroutine(UpdateHealthBar(currentHealth, newHealth));

        // Trigger rumble when health decreases
        if (newHealth < currentHealth)
        {
            StartCoroutine(RumbleEffect());
        }

        currentHealth = newHealth;

        // Play smoke effect if health is low
        if (currentHealth <= 20 && Smoke != null && !Smoke.isPlaying)
        {
            Smoke.Play();
        }

        Debug.Log($"{gameObject.name} Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            if (Explosion != null)
            {
                Explosion.Play();
            }

            StartCoroutine(HandleDeath());
        }
    }

    private IEnumerator UpdateHealthBar(float startHealth, float endHealth)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f / lerpSpeed)
        {
            if (healthBar != null)
            {
                healthBar.value = Mathf.Lerp(startHealth, endHealth, elapsedTime * lerpSpeed);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (healthBar != null)
        {
            healthBar.value = endHealth;
        }
    }

    private IEnumerator HandleDeath()
    {
        Debug.Log($"{gameObject.name} has been destroyed.");

        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = true;
        }

        yield return new WaitForSeconds(1f);

        if (!string.IsNullOrEmpty(WinnerScene))
        {
            SceneManager.LoadScene(WinnerScene);
        }
        else
        {
            Debug.LogError("WinnerScene not assigned!");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log($"{gameObject.name} collided with an obstacle");
            TakeDamage(0.8f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log($"{gameObject.name} collided with fire");
            TakeDamage(1f);
        }
    }

    private IEnumerator RumbleEffect()
    {
        Gamepad gamepad = GetPlayerGamepad();
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(0.7f, 0.7f); // Light rumble
            yield return new WaitForSeconds(0.2f); // Short duration
            gamepad.SetMotorSpeeds(0, 0); // Stop rumble
        }
    }

    private Gamepad GetPlayerGamepad()
    {
        if (playerInput != null && playerInput.devices.Count > 0)
        {
            foreach (var device in playerInput.devices)
            {
                if (device is Gamepad gamepad)
                {
                    return gamepad; // Return the assigned gamepad
                }
            }
        }
        return null;
    }
   
}

 


