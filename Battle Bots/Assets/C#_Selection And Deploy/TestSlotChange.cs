using UnityEngine;

public class TestSlotChange : MonoBehaviour
{
    // Reference to the slot game objects
    public GameObject Panel1_slot1;  // Spin Bot
    public GameObject Panel1_slot2;  // Hammer Bot
    
    private int selectedBotIndex = 0; // 0 for slot 1, 1 for slot 2

    void Start()
    {
        // Initially set slot1 visible and slot2 invisible
        Panel1_slot1.SetActive(true);
        Panel1_slot2.SetActive(false);
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Get the horizontal axis input for left and right movement
        float horizontal = Input.GetAxisRaw("Horizontal1");

        // If the right button (gamepad) is pressed, switch to slot 2
        if (horizontal > 0.1f && selectedBotIndex == 0)
        {
            selectedBotIndex = 1;
            UpdateSlotVisibility();
        }
        // If the left button (gamepad) is pressed, switch to slot 1
        else if (horizontal < -0.1f && selectedBotIndex == 1)
        {
            selectedBotIndex = 0;
            UpdateSlotVisibility();
        }
    }

    void UpdateSlotVisibility()
    {
        // Set slot visibility based on selected index
        if (selectedBotIndex == 0)
        {
            Panel1_slot1.SetActive(true);  // Show Slot 1
            Panel1_slot2.SetActive(false); // Hide Slot 2
        }
        else if (selectedBotIndex == 1)
        {
            Panel1_slot1.SetActive(false); // Hide Slot 1
            Panel1_slot2.SetActive(true);  // Show Slot 2
        }
    }
}
