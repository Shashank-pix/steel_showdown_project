using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BotSpawner : MonoBehaviour
{
    [Header("Bot Prefabs")]
    [SerializeField] private GameObject spinBotPrefab; // Spin Bot prefab
    [SerializeField] private GameObject hammerBotPrefab; // Hammer Bot prefab

    [Header("Spawn Points")]
    [SerializeField] private Transform spawnPointPlayer1; // Player 1 spawn point
    [SerializeField] private Transform spawnPointPlayer2; // Player 2 spawn point

    [Header("Health Bars")]
    [SerializeField] private Slider player1HealthBar; // Health bar for Player 1
    [SerializeField] private Slider player2HealthBar; // Health bar for Player 2

    private readonly Dictionary<int, GameObject> panel1BotMap = new Dictionary<int, GameObject>();
    private readonly Dictionary<int, GameObject> panel2BotMap = new Dictionary<int, GameObject>();

    private void Start()
    {
        // Map slots to bots for Panel 1 (Player 1)
        panel1BotMap[0] = spinBotPrefab; // Slot 1 -> Spin Bot
        panel1BotMap[1] = hammerBotPrefab; // Slot 2 -> Hammer Bot

        // Map slots to bots for Panel 2 (Player 2)
        panel2BotMap[0] = hammerBotPrefab; // Slot 1 -> Hammer Bot
        panel2BotMap[1] = spinBotPrefab; // Slot 2 -> Spin Bot

        // Retrieve player selections from PlayerPrefs
        int player1SelectedBot = PlayerPrefs.GetInt("Player1Bot", 0); // Default Slot 1
        int player2SelectedBot = PlayerPrefs.GetInt("Player2Bot", 0); // Default Slot 1

        // Debug logs to verify selections
        Debug.Log("Player 1 selected bot (slot): " + player1SelectedBot);
        Debug.Log("Player 2 selected bot (slot): " + player2SelectedBot);

        // Spawn and configure Player 1's bot
        if (panel1BotMap.TryGetValue(player1SelectedBot, out GameObject player1BotPrefab))
        {
            GameObject player1Bot = Instantiate(player1BotPrefab, spawnPointPlayer1.position, spawnPointPlayer1.rotation);
            Debug.Log("Spawned Player 1 bot: " + player1BotPrefab.name);

            // Assign health bar to Player 1's bot
            BotHealth player1Health = player1Bot.GetComponent<BotHealth>();
            if (player1Health != null)
            {
                player1Health.healthBar = player1HealthBar; // Assign Player 1's health bar
                player1Health.WinnerScene = "P2_Winner";
            }

            // Assign movement and attack inputs
            BotMovement player1BotMovement = player1Bot.GetComponent<BotMovement>();
            BotAttack player1BotAttack = player1Bot.GetComponent<BotAttack>();
            if (player1BotMovement != null)
            {
                player1Bot.GetComponent<PlayerInput>().actions["Move"].performed += ctx => player1BotMovement.OnMoveInput(ctx.ReadValue<Vector2>());
            }
            if (player1BotAttack != null)
            {
                player1Bot.GetComponent<PlayerInput>().actions["Attack"].performed += ctx => player1BotAttack.OnAttackInput();
            }
        }

        // Spawn and configure Player 2's bot
        if (panel2BotMap.TryGetValue(player2SelectedBot, out GameObject player2BotPrefab))
        {
            GameObject player2Bot = Instantiate(player2BotPrefab, spawnPointPlayer2.position, spawnPointPlayer2.rotation);
            Debug.Log("Spawned Player 2 bot: " + player2BotPrefab.name);

            // Assign health bar to Player 2's bot
            BotHealth player2Health = player2Bot.GetComponent<BotHealth>();
            if (player2Health != null)
            {
                player2Health.healthBar = player2HealthBar; // Assign Player 2's health bar
                player2Health.WinnerScene = "P1_Winner";
                
            }

            // Assign movement and attack inputs
            BotMovement player2BotMovement = player2Bot.GetComponent<BotMovement>();
            BotAttack player2BotAttack = player2Bot.GetComponent<BotAttack>();
            if (player2BotMovement != null)
            {
                player2Bot.GetComponent<PlayerInput>().actions["Move"].performed += ctx => player2BotMovement.OnMoveInput(ctx.ReadValue<Vector2>());
            }
            if (player2BotAttack != null)
            {
                player2Bot.GetComponent<PlayerInput>().actions["Attack"].performed += ctx => player2BotAttack.OnAttackInput();
            }
        }
    }

 
    //-----------------------------------------------------------------
    //  [Header("Bot Prefabs")]
    // [SerializeField] private GameObject spinBotPrefab; // Spin Bot prefab
   
    // [SerializeField] private GameObject hammerBotPrefab; // Hammer Bot prefab

    // [Header("Spawn Points")]
    // [SerializeField] private Transform spawnPointPlayer1; // Player 1 spawn point
    // [SerializeField] private Transform spawnPointPlayer2; // Player 2 spawn point

    // [Header("Health Bar")]
    // [SerializeField] private Slider Player1Healthbar;
    // [SerializeField] private Slider Player2Healthbar;

    // private readonly Dictionary<int, GameObject> panel1BotMap = new Dictionary<int, GameObject>();
    // private readonly Dictionary<int, GameObject> panel2BotMap = new Dictionary<int, GameObject>();

    // private void Start()
    // {
    //     // Map slots to bots for Panel 1 (Player 1)
    //     panel1BotMap[0] = spinBotPrefab; // Slot 1 -> Spin Bot
    //     panel1BotMap[1] = hammerBotPrefab; // Slot 2 -> Hammer Bot

    //     // Map slots to bots for Panel 2 (Player 2)
    //     panel2BotMap[0] = hammerBotPrefab; // Slot 1 -> Hammer Bot
    //     panel2BotMap[1] = spinBotPrefab; // Slot 2 -> Spin Bot

    //     // Retrieve player selections from PlayerPrefs
    //     int player1SelectedBot = PlayerPrefs.GetInt("Player1Bot", 0); // Default Slot 1
    //     int player2SelectedBot = PlayerPrefs.GetInt("Player2Bot", 0); // Default Slot 1

    //     // Debug logs to verify selections
    //     Debug.Log("Player 1 selected bot (slot): " + player1SelectedBot);
    //     Debug.Log("Player 2 selected bot (slot): " + player2SelectedBot);

    //     // Spawn Player 1's bot
    //     if (panel1BotMap.TryGetValue(player1SelectedBot, out GameObject player1BotPrefab))
    //     {
    //         GameObject player1Bot = Instantiate(player1BotPrefab, spawnPointPlayer1.position, spawnPointPlayer1.rotation);
    //         Debug.Log("Spawned Player 1 bot: " + player1BotPrefab.name);
            
    //         // Get the BotMovement script and assign the controls for Player 1
    //         BotMovement player1BotMovement = player1Bot.GetComponent<BotMovement>();
    //          //Get the BotAttack script and assign the controls for player1
    //         BotAttack player1BotAttack = player1Bot.GetComponent<BotAttack>();
    //         if (player1BotMovement != null)
    //         {
    //             player1BotMovement.enabled = true; // Ensure the movement script is enabled
    //             // Bind movement input for Player 1
    //             player1Bot.GetComponent<PlayerInput>().actions["Move"].performed += ctx => player1BotMovement.OnMoveInput(ctx.ReadValue<Vector2>());
    //             // Bind attack input (South button for Player 1)
    //             player1Bot.GetComponent<PlayerInput>().actions["Attack"].performed += ctx => player1BotAttack.OnAttackInput();
                   
                
               
    //         }
    //     }

    //     // Spawn Player 2's bot
    //     if (panel2BotMap.TryGetValue(player2SelectedBot, out GameObject player2BotPrefab))
    //     {
    //         GameObject player2Bot = Instantiate(player2BotPrefab, spawnPointPlayer2.position, spawnPointPlayer2.rotation);
    //         Debug.Log("Spawned Player 2 bot: " + player2BotPrefab.name);
            
    //         // Get the BotMovement script and assign the controls for Player 2
    //         BotMovement player2BotMovement = player2Bot.GetComponent<BotMovement>();
    //         //Get the BotAttack script and assign the controls for player2
    //          BotAttack player2BotAttack = player2Bot.GetComponent<BotAttack>();
    //         if (player2BotMovement != null)
    //         {
    //             player2BotMovement.enabled = true; // Ensure the movement script is enabled
    //             // Bind movement input for Player 2
    //             player2Bot.GetComponent<PlayerInput>().actions["Move"].performed += ctx => player2BotMovement.OnMoveInput(ctx.ReadValue<Vector2>());
    //             // Bind attack input (South button for Player 2)
    //             player2Bot.GetComponent<PlayerInput>().actions["Attack"].performed += ctx => player2BotAttack.OnAttackInput();
    //         }
    //     }
    // }
    
    
    
}





