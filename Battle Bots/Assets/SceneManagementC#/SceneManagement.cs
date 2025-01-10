using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
   public InputActionAsset inputActions; // Reference to the InputActionAsset with player1 and player2 action maps
   

    private InputAction player1BackAction; // Player 1's Back action reference
    private InputAction player2BackAction; // Player 2's Back action reference

    private void OnEnable()
    {
        // Get the action maps for Player 1 and Player 2 from the InputActionAsset
        InputActionMap player1ActionMap = inputActions.FindActionMap("Player1");
        InputActionMap player2ActionMap = inputActions.FindActionMap("Player2");

        // Ensure both action maps are found
        if (player1ActionMap != null && player2ActionMap != null)
        {
            // Get the Back action from both player action maps
            player1BackAction = player1ActionMap.FindAction("Back");
            player2BackAction = player2ActionMap.FindAction("Back");

            if (player1BackAction != null && player2BackAction != null)
            {
                // Enable the actions
                player1BackAction.Enable();
                player2BackAction.Enable();
            }
            else
            {
                Debug.LogError("Back action not found in one of the player action maps.");
            }
        }
        else
        {
            Debug.LogError("One or both player action maps not found.");
        }
    }

    private void OnDisable()
    {
        // Disable the actions when the object is disabled
        player1BackAction?.Disable();
        player2BackAction?.Disable();
    }

    private void Update()
    {
        // Check if the Back action is triggered for Player 1 or Player 2
        if ((player1BackAction != null && player1BackAction.triggered) ||
            (player2BackAction != null && player2BackAction.triggered))
        {
            // Load the main menu scene if either player's Back action is triggered
            SceneManager.LoadScene("MainMenu");
        }
    }

}
