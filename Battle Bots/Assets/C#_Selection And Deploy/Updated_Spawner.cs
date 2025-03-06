using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Updated_Spawner : MonoBehaviour
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

        // Spawn and configure Player 1's bot
        if (panel1BotMap.TryGetValue(player1SelectedBot, out GameObject player1BotPrefab))
        {
            GameObject player1Bot = Instantiate(player1BotPrefab, spawnPointPlayer1.position, spawnPointPlayer1.rotation);
            Debug.Log("Spawned Player 1 bot: " + player1BotPrefab.name);

            // Enable red outline shader
            SetOutlineShader(player1Bot, Color.red);

            // Assign health bar to Player 1's bot
            ConfigureBot(player1Bot, player1HealthBar, "P2WinnerFinal", "Move", "Attack");
        }

        // Spawn and configure Player 2's bot
        if (panel2BotMap.TryGetValue(player2SelectedBot, out GameObject player2BotPrefab))
        {
            GameObject player2Bot = Instantiate(player2BotPrefab, spawnPointPlayer2.position, spawnPointPlayer2.rotation);
            Debug.Log("Spawned Player 2 bot: " + player2BotPrefab.name);

            // Enable blue outline shader
            SetOutlineShader(player2Bot, Color.blue);

            // Assign health bar to Player 2's bot
            ConfigureBot(player2Bot, player2HealthBar, "P1WinnerFinal", "Move", "Attack");
        }
    }

    private void SetOutlineShader(GameObject bot, Color outlineColor)
    {
         // Get all MeshRenderer components in the children of the bot
    MeshRenderer[] meshRenderers = bot.GetComponentsInChildren<MeshRenderer>();

    foreach (MeshRenderer renderer in meshRenderers)
    {
        // Loop through each material and create a new instance for unique customization
        for (int i = 0; i < renderer.materials.Length; i++)
        {
            Material originalMaterial = renderer.materials[i];
            Material newMaterial = new Material(originalMaterial); // Create a unique material instance
            newMaterial.SetColor("_OutlineColor", outlineColor);
            newMaterial.SetFloat("_OutlineEnabled", 1); // Enable the outline

            renderer.materials[i] = newMaterial; // Assign the new material back to the renderer
        }
    }
    }

    private void ConfigureBot(GameObject bot, Slider healthBar, string winnerScene, string moveAction, string attackAction)
    {
        BotHealth botHealth = bot.GetComponent<BotHealth>();
        if (botHealth != null)
        {
            botHealth.healthBar = healthBar;
            botHealth.WinnerScene = winnerScene;
        }

        BotMovement botMovement = bot.GetComponent<BotMovement>();
        BotAttack botAttack = bot.GetComponent<BotAttack>();
        PlayerInput playerInput = bot.GetComponent<PlayerInput>();

        if (botMovement != null && playerInput != null)
        {
            playerInput.actions[moveAction].performed += ctx => botMovement.OnMoveInput(ctx.ReadValue<Vector2>());
        }
        if (botAttack != null && playerInput != null)
        {
            playerInput.actions[attackAction].performed += ctx => botAttack.OnAttackInput();
        }
    }
}
