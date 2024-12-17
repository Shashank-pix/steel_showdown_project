using UnityEngine;

public class CharacterSelectionUI : MonoBehaviour
{
    [Header("Player-1 Selection")]
    // Player 1 Slots
    [SerializeField]private GameObject slot1Player1;  // Spin Bot for Player 1
    [SerializeField]private GameObject slot2Player1;  // Hammer Bot for Player 1

    [Header("Player-2 Selection")]
    // Player 2 Slots
    [SerializeField]private GameObject slot1Player2;  // Spin Bot for Player 2
    [SerializeField]private GameObject slot2Player2;  // Hammer Bot for Player 2

    private int selectedBotIndexPlayer1 = 0; // 0 for slot 1, 1 for slot 2 (Player 1)
    private int selectedBotIndexPlayer2 = 0; // 0 for slot 1, 1 for slot 2 (Player 2)

    void Start()
    {
        // Initialize both panels' slot visibility
        UpdateSlotVisibility(slot1Player1, slot2Player1, selectedBotIndexPlayer1);
        UpdateSlotVisibility(slot1Player2, slot2Player2, selectedBotIndexPlayer2);
    }

    void Update()
    {
        // Handle input for both players
        HandleInput("Horizontal1", ref selectedBotIndexPlayer1, slot1Player1, slot2Player1);
        HandleInput("Horizontal2", ref selectedBotIndexPlayer2, slot1Player2, slot2Player2);
    }

    void HandleInput(string axisName, ref int selectedBotIndex, GameObject slot1, GameObject slot2)
    {
        // Get the horizontal axis input for left and right movement
        float horizontal = Input.GetAxisRaw(axisName);

        // If the right button (gamepad) is pressed, switch to slot 2
        if (horizontal > 0.1f && selectedBotIndex == 0)
        {
            selectedBotIndex = 1;
            UpdateSlotVisibility(slot1, slot2, selectedBotIndex);
        }
        // If the left button (gamepad) is pressed, switch to slot 1
        else if (horizontal < -0.1f && selectedBotIndex == 1)
        {
            selectedBotIndex = 0;
            UpdateSlotVisibility(slot1, slot2, selectedBotIndex);
        }
    }

    void UpdateSlotVisibility(GameObject slot1, GameObject slot2, int selectedIndex)
    {
        // Set slot visibility based on the selected index
        if (selectedIndex == 0)
        {
            slot1.SetActive(true);  // Show Slot 1
            slot2.SetActive(false); // Hide Slot 2
        }
        else if (selectedIndex == 1)
        {
            slot1.SetActive(false); // Hide Slot 1
            slot2.SetActive(true);  // Show Slot 2
        }
    }
}
