using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerSceneManager : MonoBehaviour
{
     public GameObject player1WinCube; // Cube displaying Player 1 win message
    public GameObject player2WinCube; // Cube displaying Player 2 win message
    public GameObject player1BotPrefab; // Player 1's bot prefab
    public GameObject player2BotPrefab; // Player 2's bot prefab
    public Transform spawnPoint; // A spawn point for the bot to appear

    private void Start()
    {
        // Get the winner from PlayerPrefs
        string winner = PlayerPrefs.GetString("Winner", "None");

        // Display the winner message and the winning bot
        if (winner == "Player 1")
        {
            // Enable Player 1's winning message and bot
            player1WinCube.SetActive(true);
            Instantiate(player1BotPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else if (winner == "Player 2")
        {
            // Enable Player 2's winning message and bot
            player2WinCube.SetActive(true);
            Instantiate(player2BotPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Winner not set correctly!");
        }
    }
}

   






