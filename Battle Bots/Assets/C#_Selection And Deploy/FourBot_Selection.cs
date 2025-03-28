using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FourBot_Selection : MonoBehaviour
{
    [Header("Player-1 Selection")]
    [SerializeField] private GameObject slot1Player1;
    [SerializeField] private GameObject slot2Player1;
    [SerializeField] private GameObject slot3Player1;
    [SerializeField] private GameObject slot4Player1;

    [Header("Player-2 Selection")]
    [SerializeField] private GameObject slot1Player2;
    [SerializeField] private GameObject slot2Player2;
    [SerializeField] private GameObject slot3Player2;
    [SerializeField] private GameObject slot4Player2;

    [Header("Bot Prefabs")]
    [SerializeField] private GameObject spinBotPrefab;
    [SerializeField] private GameObject hammerBotPrefab;
    [SerializeField] private GameObject flameBotPrefab;
    [SerializeField] private GameObject shockBotPrefab;

    private int selectedBotIndex_P1 = 0;
    private int selectedBotIndex_P2 = 0;

    private bool isConfirmed_P1 = false;
    private bool isConfirmed_P2 = false;

    [Header("Confirm Button Editor")]
    [SerializeField] private Button Confirm_P1;
    [SerializeField] private Button Confirm_P2;

    [SerializeField] private Color Confirmed_Color;
    [SerializeField] private string Confirmed_Text;

    [Header("Confirm Animations")]
    [SerializeField] private Animator Player1Anim;
    [SerializeField] private Animator Player2Anim;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource botSwitchSFX_P1;
    [SerializeField] private AudioSource botSwitchSFX_P2;
    [SerializeField] private AudioSource confirmSFX_P1;
    [SerializeField] private AudioSource confirmSFX_P2;
    [SerializeField] private AudioSource finalConfirmSFX;

    private readonly Dictionary<int, GameObject> player1Slots = new Dictionary<int, GameObject>();
    private readonly Dictionary<int, GameObject> player2Slots = new Dictionary<int, GameObject>();

    private float inputCoolDown_P1 = 0.3f;
    private float nextInputTime_P1 = 0f;
    private float inputCoolDown_P2 = 0.3f;
    private float nextInputTime_P2 = 0f;

    void Start()
    {
        player1Slots[0] = slot1Player1;
        player1Slots[1] = slot2Player1;
        player1Slots[2] = slot3Player1;
        player1Slots[3] = slot4Player1;

        player2Slots[0] = slot1Player2;
        player2Slots[1] = slot2Player2;
        player2Slots[2] = slot3Player2;
        player2Slots[3] = slot4Player2;

        UpdateSlotVisibility(player1Slots, selectedBotIndex_P1);
        UpdateSlotVisibility(player2Slots, selectedBotIndex_P2);
    }

    void Update()
    {
        HandleInputPlayer1();
        HandleInputPlayer2();

        if (isConfirmed_P1 && isConfirmed_P2)
        {
            // Play final confirmation sound immediately
            if (!finalConfirmSFX.isPlaying)
            {
                finalConfirmSFX.Play();
            }

            // Load the next scene after a delay
            StartCoroutine(LoadNextSceneWithDelay(1.2f));
        }
    }

    void HandleInputPlayer1()
    {
        if (isConfirmed_P1 || Time.time < nextInputTime_P1) return;

        float horizontal = Input.GetAxisRaw("Horizontal1");
        if (horizontal > 0.5f)
        {
            selectedBotIndex_P1 = (selectedBotIndex_P1 + 1) % 4;
            UpdateSlotVisibility(player1Slots, selectedBotIndex_P1);
            nextInputTime_P1 = Time.time + inputCoolDown_P1;
            botSwitchSFX_P1.Play();
        }
        else if (horizontal < -0.5f)
        {
            selectedBotIndex_P1 = (selectedBotIndex_P1 - 1 + 4) % 4;
            UpdateSlotVisibility(player1Slots, selectedBotIndex_P1);
            nextInputTime_P1 = Time.time + inputCoolDown_P1;
            botSwitchSFX_P1.Play();
        }

        if (Input.GetButtonDown("Submit1"))
        {
            isConfirmed_P1 = true;
            PlayerPrefs.SetInt("Player1Bot", selectedBotIndex_P1);
            ChangeButtonColor(Confirm_P1, Confirmed_Color);
            ChangeButtonText(Confirm_P1, Confirmed_Text);

            // Play Player 1 confirmation sound
            confirmSFX_P1.Play();

            // Play Player 1 confirmation animation
            if (Player1Anim != null)
            {
                Player1Anim.SetTrigger("Confirm");
            }
        }
    }

    void HandleInputPlayer2()
    {
        if (isConfirmed_P2 || Time.time < nextInputTime_P2) return;

        float horizontal = Input.GetAxisRaw("Horizontal2");
        if (horizontal > 0.5f)
        {
            selectedBotIndex_P2 = (selectedBotIndex_P2 + 1) % 4;
            UpdateSlotVisibility(player2Slots, selectedBotIndex_P2);
            nextInputTime_P2 = Time.time + inputCoolDown_P2;
            botSwitchSFX_P2.Play();
        }
        else if (horizontal < -0.5f)
        {
            selectedBotIndex_P2 = (selectedBotIndex_P2 - 1 + 4) % 4;
            UpdateSlotVisibility(player2Slots, selectedBotIndex_P2);
            nextInputTime_P2 = Time.time + inputCoolDown_P2;
            botSwitchSFX_P2.Play();
        }

        if (Input.GetButtonDown("Submit2"))
        {
            isConfirmed_P2 = true;
            PlayerPrefs.SetInt("Player2Bot", selectedBotIndex_P2);
            ChangeButtonColor(Confirm_P2, Confirmed_Color);
            ChangeButtonText(Confirm_P2, Confirmed_Text);

            // Play Player 2 confirmation sound
            confirmSFX_P2.Play();

            // Play Player 2 confirmation animation
            if (Player2Anim != null)
            {
                Player2Anim.SetTrigger("Confirm");
            }
        }
    }

    void UpdateSlotVisibility(Dictionary<int, GameObject> slots, int selectedIndex)
    {
        foreach (var slot in slots)
        {
            slot.Value.SetActive(slot.Key == selectedIndex);
        }
    }

    IEnumerator LoadNextSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(2);
    }

    void ChangeButtonColor(Button button, Color color)
    {
        Image ButtonImage = button.GetComponent<Image>();
        if (ButtonImage != null)
        {
            ButtonImage.color = color;
        }
    }

    void ChangeButtonText(Button button, string newText)
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            buttonText.text = newText;
        }
    }

    //------------------------------------
    // [Header("Player-1 Selection")]
    // [SerializeField] private GameObject slot1Player1; // Spin Bot
    // [SerializeField] private GameObject slot2Player1; // Hammer Bot
    // [SerializeField] private GameObject slot3Player1; // Flame Bot
    // [SerializeField] private GameObject slot4Player1; // Shock Bot

    // [Header("Player-2 Selection")]
    // [SerializeField] private GameObject slot1Player2; // Spin Bot
    // [SerializeField] private GameObject slot2Player2; // Hammer Bot
    // [SerializeField] private GameObject slot3Player2; // Flame Bot
    // [SerializeField] private GameObject slot4Player2; // Shock Bot

    // [Header("Bot Prefabs")]
    // [SerializeField] private GameObject spinBotPrefab;
    // [SerializeField] private GameObject hammerBotPrefab;
    // [SerializeField] private GameObject flameBotPrefab;
    // [SerializeField] private GameObject shockBotPrefab;

    // private int selectedBotIndex_P1 = 0;
    // private int selectedBotIndex_P2 = 0;

    // private bool isConfirmed_P1 = false;
    // private bool isConfirmed_P2 = false;

    // [Header("Confirm Button Editor")]
    // [SerializeField] private Button Confirm_P1;
    // [SerializeField] private Button Confirm_P2;

    // [SerializeField] private Color Confirmed_Color;
    // [SerializeField] private string Confirmed_Text;

    // [Header("Confirm Animations")]
    // [SerializeField] private Animator Player1Anim;
    // [SerializeField] private Animator Player2Anim;

    // private readonly Dictionary<int, GameObject> player1Slots = new Dictionary<int, GameObject>();
    // private readonly Dictionary<int, GameObject> player2Slots = new Dictionary<int, GameObject>();

    // //-------(Cool Down Variables)-------------

    // private float inputCoolDown_P1 = 0.3f;
    // private float nextInputTime_P1 = 0f;
    // private float inputCoolDown_P2 = 0.3f;

    // private float nextInputTime_P2 = 0f;



    // void Start()
    // {
    //     // Map slots for Player 1
    //     player1Slots[0] = slot1Player1;
    //     player1Slots[1] = slot2Player1;
    //     player1Slots[2] = slot3Player1;
    //     player1Slots[3] = slot4Player1;

    //     // Map slots for Player 2
    //     player2Slots[0] = slot1Player2;
    //     player2Slots[1] = slot2Player2;
    //     player2Slots[2] = slot3Player2;
    //     player2Slots[3] = slot4Player2;

    //     UpdateSlotVisibility(player1Slots, selectedBotIndex_P1);
    //     UpdateSlotVisibility(player2Slots, selectedBotIndex_P2);
    // }

    // void Update()
    // {
    //     HandleInputPlayer1();
    //     HandleInputPlayer2();

    //     if (isConfirmed_P1 && isConfirmed_P2)
    //     {
    //         Debug.Log("Both players confirmed! Loading the next scene...");
    //         StartCoroutine(LoadNextSceneWithDelay(1.2f));
    //     }
    // }

    // void HandleInputPlayer1()
    // {
    //     if (isConfirmed_P1 || Time.time < nextInputTime_P1) return; // this prevents quick selection

    //     float horizontal = Input.GetAxisRaw("Horizontal1");
    //     if (horizontal > 0.5f)
    //     {
    //         selectedBotIndex_P1 = (selectedBotIndex_P1 + 1) % 4;
    //         UpdateSlotVisibility(player1Slots, selectedBotIndex_P1);
    //         nextInputTime_P1 = Time.time + inputCoolDown_P1; // Apply coolDown
    //     }
    //     else if (horizontal < -0.5f)
    //     {
    //         selectedBotIndex_P1 = (selectedBotIndex_P1 - 1 + 4) % 4;
    //         UpdateSlotVisibility(player1Slots, selectedBotIndex_P1);
    //         nextInputTime_P1 = Time.time + inputCoolDown_P1; // Apply coolDown
    //     }

    //     if (Input.GetButtonDown("Submit1"))
    //     {
    //         isConfirmed_P1 = true;
    //         PlayerPrefs.SetInt("Player1Bot", selectedBotIndex_P1);
    //         ChangeButtonColor(Confirm_P1, Confirmed_Color);
    //         ChangeButtonText(Confirm_P1, Confirmed_Text);
    //         Debug.Log("Player 1 Bot Confirmed: " + selectedBotIndex_P1);

    //          // Play Player 1 confirmation animation
    //         if (Player1Anim != null)
    //         {
    //           Player1Anim.SetTrigger("Confirm");
    //         }
    //     }
    // }

    // void HandleInputPlayer2()
    // {
    //     if (isConfirmed_P2 || Time.time < nextInputTime_P2) return;

    //     float horizontal = Input.GetAxisRaw("Horizontal2");
    //     if (horizontal > 0.5f)
    //     {
    //         selectedBotIndex_P2 = (selectedBotIndex_P2 + 1) % 4;
    //         UpdateSlotVisibility(player2Slots, selectedBotIndex_P2);
    //         nextInputTime_P2 = Time.time + inputCoolDown_P2;
    //     }
    //     else if (horizontal < -0.5f)
    //     {
    //         selectedBotIndex_P2 = (selectedBotIndex_P2 - 1 + 4) % 4;
    //         UpdateSlotVisibility(player2Slots, selectedBotIndex_P2);
    //         nextInputTime_P2 = Time.time + inputCoolDown_P2;
    //     }

    //     if (Input.GetButtonDown("Submit2"))
    //     {
    //         isConfirmed_P2 = true;
    //         PlayerPrefs.SetInt("Player2Bot", selectedBotIndex_P2);
    //         ChangeButtonColor(Confirm_P2, Confirmed_Color);
    //         ChangeButtonText(Confirm_P2, Confirmed_Text);
    //         Debug.Log("Player 2 Bot Confirmed: " + selectedBotIndex_P2);

    //           // Play Player 2 confirmation animation
    //         if (Player2Anim != null)
    //         {
    //           Player2Anim.SetTrigger("Confirm");
    //         }
    //     }
    // }

    // void UpdateSlotVisibility(Dictionary<int, GameObject> slots, int selectedIndex)
    // {
    //     foreach (var slot in slots)
    //     {
    //         slot.Value.SetActive(slot.Key == selectedIndex);
    //     }
    // }

    // IEnumerator LoadNextSceneWithDelay(float delay)
    // {
    //     Debug.Log("Both players confirmed! Scene will switch in " + delay + " seconds.");
    //     yield return new WaitForSeconds(delay);
    //     SceneManager.LoadScene(2);
    // }

    // void ChangeButtonColor(Button button, Color color)
    // {
    //     Image ButtonImage = button.GetComponent<Image>();
    //     if (ButtonImage != null)
    //     {
    //         ButtonImage.color = color;
    //     }
    // }

    // void ChangeButtonText(Button button, string newText)
    // {
    //     TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
    //     if (buttonText != null)
    //     {
    //         buttonText.text = newText;
    //     }
    // }

}
