using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class BattleArenaManager : MonoBehaviour
{
   
    [SerializeField] private string botTag = "Bot"; // Tag for bots
    [SerializeField] private TextMeshProUGUI countdownText; // Countdown UI Text
    [SerializeField] private float countdownTime = 3f; // Time for the countdown

    private List<MonoBehaviour> movementScripts = new List<MonoBehaviour>();
    private List<Rigidbody> botRigidbodies = new List<Rigidbody>();
    private bool botsInitialized = false; // Flag to check if bots are initialized

    private Game_Timer gameTimer; // Reference to GameTimer script

    private GamePause gamePause; // Reference to GamePause script 

    void Start()
    {
        // Find the GameTimer script
        gameTimer = FindObjectOfType<Game_Timer>();

        //Find the GamePause script
        gamePause = FindAnyObjectByType<GamePause>();

        
           // Disable the GamePause script during the countdown
        if (gamePause != null)
        {
            gamePause.enabled = false;
            if (gamePause.pausedText != null)
            {
                gamePause.pausedText.gameObject.SetActive(false);
            }
        }


        // Start the countdown and initialize bots after a short delay
        StartCoroutine(InitializeBattleArena());
    }

    IEnumerator InitializeBattleArena()
    {
        yield return new WaitForSeconds(0.01f); // Wait for bots to spawn

        FindBots();
        DisableBotMovement();

        yield return StartCoroutine(StartCountdown()); // Start countdown

        EnableBotMovement();

         // Re-enable the GamePause script after the countdown
        if (gamePause != null)
        {
            gamePause.enabled =true;
        }

        // Start the game timer after enabling bots
        if (gameTimer != null)
        {
            gameTimer.StartGameTimer();
        }
    }

    void FindBots()
    {
        movementScripts.Clear();
        botRigidbodies.Clear();

        GameObject[] bots = GameObject.FindGameObjectsWithTag(botTag);
        foreach (GameObject bot in bots)
        {
            var script = bot.GetComponent<MonoBehaviour>();
            if (script != null)
            {
                movementScripts.Add(script);
            }

            var rb = bot.GetComponent<Rigidbody>();
            if (rb != null)
            {
                botRigidbodies.Add(rb);
            }
        }

        botsInitialized = true;
    }

    void DisableBotMovement()
    {
          if (!botsInitialized) return;

    foreach (GameObject bot in GameObject.FindGameObjectsWithTag(botTag))
    {
        // Disable movement script
        var movementScript = bot.GetComponent<BotMovement>();
        if (movementScript != null)
            movementScript.enabled = false;

        // Set Rigidbody to kinematic
        var rb = bot.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;
    }
        // if (!botsInitialized) return;

        // foreach (var script in movementScripts)
        // {
        //     script.enabled = false;
        // }

        // foreach (var rb in botRigidbodies)
        // {
        //     rb.isKinematic = true;
        //     //rb.velocity = Vector3.zero; //Stops any linear motion
        //    // rb.angularVelocity = Vector3.zero; //stops any kind of rotation
        // }
    }

    void EnableBotMovement()
    {
        if (!botsInitialized) return;

    foreach (GameObject bot in GameObject.FindGameObjectsWithTag(botTag))
    {
        // Enable movement script
        var movementScript = bot.GetComponent<BotMovement>();
        if (movementScript != null)
            movementScript.enabled = true;

        // Set Rigidbody to non-kinematic
        var rb = bot.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = false;
    }
        // if (!botsInitialized) return;

        // foreach (var script in movementScripts)
        // {
        //     script.enabled = true;
        // }

        // foreach (var rb in botRigidbodies)
        // {
        //     rb.isKinematic = false;
        // }
    }

    IEnumerator StartCountdown()
    {
      // Call DisableBotMovement to completely stop the bots
    DisableBotMovement();

    float timer = countdownTime;

    // Countdown logic
    while (timer > 0)
    {
        countdownText.text = Mathf.Ceil(timer).ToString(); // Display countdown
        yield return new WaitForSeconds(1f);
        timer -= 1f;
    }

    countdownText.text = "Start!";
    yield return new WaitForSeconds(1f); // Pause on "Start!" for a moment
    countdownText.text = ""; // Clear the countdown text

    // Re-enable bot movement after the countdown
    EnableBotMovement();

   
    }

     // This function is called when the game timer ends
    public void OnGameTimerEnd()
    {
        Debug.Log("Game Over: Transitioning to the next scene.");
        
        // Load the next scene (replace with the actual scene index or name)
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }


}
