using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePause : MonoBehaviour
{
    


     private BotInputActions inputActions; 
    private bool isPaused = false;
    private bool isDebouncing = false; // Debounce flag

    [SerializeField] public TextMeshProUGUI pausedText; 

    void Awake()
    {
        inputActions = new BotInputActions();
    }

    void OnEnable()
    {
        inputActions.Player1.Esc.performed += TogglePause; // Bind for Player1
        inputActions.Player2.Esc.performed += TogglePause; // Bind for Player2
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Player1.Esc.performed -= TogglePause;
        inputActions.Player2.Esc.performed -= TogglePause;
        inputActions.Disable();
    }

    private void Start()
    {
        if (pausedText != null)
        {
            pausedText.gameObject.SetActive(false); 
        }
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        if (isDebouncing) return; // Prevent double execution
        isDebouncing = true;

        Debug.Log("Pause button pressed!"); 
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }

        StartCoroutine(ResetDebounce()); // Reset the debounce flag
    }

    private IEnumerator ResetDebounce()
    {
        yield return new WaitForEndOfFrame(); // Wait until the end of the frame
        isDebouncing = false;
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
        Debug.Log("Game Paused. Time.timeScale = " + Time.timeScale); 
        if (pausedText != null)
        {
            pausedText.gameObject.SetActive(true); // Show "Paused" text
        }
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        Debug.Log("Game Resumed. Time.timeScale = " + Time.timeScale); 
        if (pausedText != null)
        {
            pausedText.gameObject.SetActive(false); // Hide "Paused" text
        }
    }

}
