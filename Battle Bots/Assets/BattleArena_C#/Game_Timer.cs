using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Game_Timer : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI timerText; // Timer UI Text
    [SerializeField] private TextMeshProUGUI timeOverText; // "Time Over" UI Text
    [SerializeField] private float gameTime = 120f; // Game duration in seconds (1 minute)
    [SerializeField] private float timeOverDisplayDuration = 2f; // Duration for "Time Over" text

    private bool isTimerRunning = false;
    private float remainingTime;

    private BattleArenaManager battleArenaManager; // Reference to the BattleArenaManager

    void Start()
    {
        remainingTime = gameTime;
        UpdateTimerText(); // Initialize the timer text with starting time
        timeOverText.gameObject.SetActive(false); // Hide "Time Over" text initially

        // Find BattleArenaManager if not already assigned
        battleArenaManager = FindObjectOfType<BattleArenaManager>();
    }

    // Function to start the game timer
    public void StartGameTimer()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            StartCoroutine(GameTimerRoutine());
        }
    }

    private IEnumerator GameTimerRoutine()
    {
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime; // Decrease remaining time
            UpdateTimerText(); // Update UI with minutes and seconds
            yield return null;
        }

        // Time is up
        isTimerRunning = false;
        timerText.text = "00:00"; // Set to 00:00 when time runs out
        StartCoroutine(HandleTimeOver());
    }

    // Update the timer text in MM:SS format
    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60); // Get the number of minutes
        int seconds = Mathf.FloorToInt(remainingTime % 60); // Get the number of seconds
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Format as MM:SS
    }

    private IEnumerator HandleTimeOver()
    {
        // Display "Time Over" text
        timeOverText.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeOverDisplayDuration); // Wait for the display duration

        // Hide "Time Over" text after the duration
        timeOverText.gameObject.SetActive(false);

        // Notify BattleArenaManager or proceed to the next scene
        if (battleArenaManager != null)
        {
            battleArenaManager.OnGameTimerEnd(); // Call method in BattleArenaManager
        }

    }




}
//     [SerializeField] private TextMeshProUGUI gameTimerText; // UI text for the game timer
//     [SerializeField] private float gameDuration = 60f; // Total time in seconds
    
//     private float currentGameTime;

//     void Start()
//     {
//         // Initialize game timer
//         gameTimerText.text = FormatTime(gameDuration);
//     }

//     public void StartGameTimer()
//     {
//         currentGameTime = gameDuration;
//         StartCoroutine(GameTimerCountdown());
//     }

//     IEnumerator GameTimerCountdown()
//     {
//     //     float timer = gameDuration;

//     //   while (timer > 0)
//     //  {
//     //     gameTimerText.text = Mathf.Floor(timer).ToString("F0"); // Update timer display
//     //     yield return new WaitForSeconds(1f);
//     //     timer -= 1f;
//     //  }
//     //  EndGame();

//         while (currentGameTime > 0)
//         {
//             currentGameTime -= Time.deltaTime;
//             gameTimerText.text = FormatTime(currentGameTime);
//             yield return null; // Wait for the next frame
//         }

//         EndGame();
//     }

//     void EndGame()
//     {
//         gameTimerText.text = "00:00";
//         SceneManager.LoadScene(2);
//     }

//     string FormatTime(float time)
//     {
//         int minutes = Mathf.FloorToInt(time / 60);
//         int seconds = Mathf.FloorToInt(time % 60);
//         return string.Format("{0:00}:{1:00}", minutes, seconds);
//     }
