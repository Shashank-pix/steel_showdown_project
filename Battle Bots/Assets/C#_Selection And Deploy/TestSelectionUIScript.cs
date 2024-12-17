using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class TestSelectionUIScript : MonoBehaviour
{
    [Header("Player-1 Selection")]
    [SerializeField] private GameObject slot1Player1; // Player 1's Spin Bot
    [SerializeField] private GameObject slot2Player1; // Player 1's Hammer Bot

    [Header("Player-2 Selection")]
    [SerializeField] private GameObject slot1Player2; // Player 2's Spin Bot
    [SerializeField] private GameObject slot2Player2; // Player 2's Hammer Bot

    [Header("Bot Prefabs")]
    [SerializeField]private GameObject spinBotPrefab;
    [SerializeField]private GameObject hammerBotPrefab;

    private int selectedBotIndex_P1 = 0; // 0 for Slot1, 1 for Slot2 for Player 1
    private int selectedBotIndex_P2 = 0; // 0 for Slot1, 1 for Slot2 for Player 2

    private bool isConfirmed_P1 = false; // Player 1 confirmation status
    private bool isConfirmed_P2 = false; // Player 2 confirmation status

    [Header("Confirm Button Editor")]
    [SerializeField] private Button Confirm_P1;
    [SerializeField] private Button Confirm_P2;

    [SerializeField]private Color Confirmed_Color;

    [SerializeField]private string Confirmed_Text;

    //selection mapping for panel 1 and 2 
    // New and interesting topic 
    private readonly Dictionary<int, GameObject> panel1BotMap = new Dictionary<int, GameObject>();
    private readonly Dictionary<int, GameObject> panel2BotMap = new Dictionary<int, GameObject>();

    void Start()
    {
         // Map slots to bots for Panel 1
    panel1BotMap[0] = spinBotPrefab; // Slot 1 -> Spin Bot
    panel1BotMap[1] = hammerBotPrefab; // Slot 2 -> Hammer Bot

    // Map slots to bots for Panel 2
    panel2BotMap[0] = hammerBotPrefab; // Slot 1 -> Hammer Bot
    panel2BotMap[1] = spinBotPrefab; // Slot 2 -> Spin Bot


        // Initialize slots
        UpdateSlotVisibility(slot1Player1, slot2Player1, selectedBotIndex_P1);
        UpdateSlotVisibility(slot1Player2, slot2Player2, selectedBotIndex_P2);
    }

    void Update()
    {
        HandleInputPlayer1();
        HandleInputPlayer2();

        // Change scene if both players confirm
        if (isConfirmed_P1 && isConfirmed_P2)
        {
            Debug.Log("Both players confirmed! Loading the next scene...");
            //SceneManager.LoadScene(1); // Replace "NextScene" with your actual scene name
            StartCoroutine(LoadNextSceneWithDelay(0.8f));

        }
    }
    void HandleInputPlayer1()
{
    if (isConfirmed_P1) return;

    float horizontal = Input.GetAxisRaw("Horizontal1");
    if (horizontal > 0.1f && selectedBotIndex_P1 == 0)
    {
        selectedBotIndex_P1 = 1;
        UpdateSlotVisibility(slot1Player1, slot2Player1, selectedBotIndex_P1);
    }
    else if (horizontal < -0.1f && selectedBotIndex_P1 == 1)
    {
        selectedBotIndex_P1 = 0;
        UpdateSlotVisibility(slot1Player1, slot2Player1, selectedBotIndex_P1);
    }

    if (Input.GetButtonDown("Submit1"))
    {
        isConfirmed_P1 = true;
        PlayerPrefs.SetInt("Player1Bot", selectedBotIndex_P1);
        ChangeButtonColor(Confirm_P1, Confirmed_Color);
        ChangeButtonText(Confirm_P1, Confirmed_Text);
        Debug.Log("Player 1 Bot Confirmed: " + selectedBotIndex_P1);
    }
}

void HandleInputPlayer2()
{
    if (isConfirmed_P2) return;

    float horizontal = Input.GetAxisRaw("Horizontal2");
    if (horizontal > 0.1f && selectedBotIndex_P2 == 0)
    {
        selectedBotIndex_P2 = 1;
        UpdateSlotVisibility(slot1Player2, slot2Player2, selectedBotIndex_P2);
    }
    else if (horizontal < -0.1f && selectedBotIndex_P2 == 1)
    {
        selectedBotIndex_P2 = 0;
        UpdateSlotVisibility(slot1Player2, slot2Player2, selectedBotIndex_P2);
    }

    if (Input.GetButtonDown("Submit2"))
    {
        isConfirmed_P2 = true;
        PlayerPrefs.SetInt("Player2Bot", selectedBotIndex_P2);
        ChangeButtonColor(Confirm_P2, Confirmed_Color);
        ChangeButtonText(Confirm_P2, Confirmed_Text);
        Debug.Log("Player 2 Bot Confirmed: " + selectedBotIndex_P2);
    }
}

    void UpdateSlotVisibility(GameObject slot1, GameObject slot2, int selectedIndex)
    {
        // Update visibility of the slots
        slot1.SetActive(selectedIndex == 0);
        slot2.SetActive(selectedIndex == 1);
    }

    IEnumerator LoadNextSceneWithDelay(float delay)
    {
         Debug.Log("Both players confirmed! Scene will switch in " + delay + " seconds.");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(2); // Replace "NextScene" with your actual scene name
 
    }

    void ChangeButtonColor(Button button, Color color)
    {
        //to change the button color
        Image ButtonImage = button.GetComponent<Image>();
        if(ButtonImage!= null)
        {
            ButtonImage.color = color;
        }
    }

    void ChangeButtonText(Button button, string newText )
    {
       TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
       if(buttonText != null)
       {
           buttonText.text = newText;
       }
    }
}
