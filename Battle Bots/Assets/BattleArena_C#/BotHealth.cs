using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class BotHealth : MonoBehaviour
{





   [SerializeField] private float maxHealth = 20f; // Maximum health
    public Slider healthBar; // Reference to the health bar UI

    public string WinnerScene = "WinnerMenu";//Scene Index

    private float currentHealth;

    
    public float lerpSpeed = 5f; // Speed of the Lerp animation for the health bar

    private Rigidbody _rigidbody;


    private void Start()
    {
        // Initialize health and health bar
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth; // Match slider max value with maxHealth
            healthBar.value = currentHealth; // Initialize slider value
        }

        _rigidbody = GetComponent<Rigidbody>();
    }

    public void TakeDamage(float damageAmount)
    {
        // Reduce health
       // currentHealth -= damageAmount; 
        // Ensure health stays in bounds
       // currentHealth = Mathf.Clamp(currentHealth,0, maxHealth); 

       // Reduce health
        float newHealth = Mathf.Clamp(currentHealth - damageAmount, 0, maxHealth);

        // Smoothly update health bar using Lerp
        StartCoroutine(UpdateHealthBar(currentHealth, newHealth));

        currentHealth = newHealth;
        

        // // Update the health bar UI
        // if (healthBar != null)
        // {
        //   //  healthBar.value = currentHealth; // Directly update slider value
           
        // }

        Debug.Log($"{gameObject.name} Health: {currentHealth}/{maxHealth}");

        // Check if the bot's health is depleted
        if (currentHealth <= 0)
        {
            //Die();
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
                // Smoothly transition between current and new health values
                healthBar.value = Mathf.Lerp(startHealth, endHealth, elapsedTime * lerpSpeed);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the health bar value is set to the final health
        if (healthBar != null)
        {
            healthBar.value = endHealth;
        }
    }

     private IEnumerator HandleDeath()
    {

    //     Debug.Log($"{gameObject.name} has been destroyed.");

    //     // Delay for a brief moment before loading the winner scene
    //     yield return new WaitForSeconds(1f); // 1-second delay

    //     // Check which player won and disable the appropriate bot
    //     GameObject player1Bot = GameObject.FindGameObjectWithTag("Player1Bot");
    //     GameObject player2Bot = GameObject.FindGameObjectWithTag("Player2Bot");

    //     if (gameObject.CompareTag("Player1Bot"))
    //     {
    //         // Player 1 wins
    //         if (player1Bot != null)
    //         {
    //             player1Bot.SetActive(true); // Enable Player 1's bot
    //         }

    //         if (player2Bot != null)
    //         {
    //             player2Bot.SetActive(false); // Disable Player 2's bot
    //         }

    //         Debug.Log("Player 1 Wins!");
    //     }
    //     else if (gameObject.CompareTag("Player2Bot"))
    //     {
    //         // Player 2 wins
    //         if (player2Bot != null)
    //         {
    //             player2Bot.SetActive(true); // Enable Player 2's bot
    //         }

    //         if (player1Bot != null)
    //         {
    //             player1Bot.SetActive(false); // Disable Player 1's bot
    //         }

    //         Debug.Log("Player 2 Wins!");
    //     }

    //     // Load the winner scene
    //     if (!string.IsNullOrEmpty(WinnerScene))
    //     {
    //         SceneManager.LoadScene(WinnerScene); // Load the winner scene
    //     }
    //     else
    //     {
    //         Debug.LogError("WinnerScene not assigned!");
    //     }
    // }
        
        Debug.Log($"{gameObject.name} has been destroyed.");
       // gameObject.SetActive(false);

       if(_rigidbody != null)
       {
        _rigidbody.isKinematic = true;
       }
      
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
    

    private void  OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log($"{gameObject.name} collided with a Roller");
            TakeDamage(0.8f);//Reduces the health by 1hp
        }
    }

    private void  OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("Obstacles")) 
       {
        Debug.Log($"{gameObject.name} collided with fire");
        TakeDamage(1f);
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
//---------------------------------------------------------------------
 


