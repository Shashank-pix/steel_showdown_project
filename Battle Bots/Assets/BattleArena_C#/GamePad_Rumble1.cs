using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePad_Rumble1 : MonoBehaviour
{
   [Header("Rumble Settings")]
    [SerializeField] private float rumbleDuration = 0.2f; // Duration of the rumble
    [SerializeField] private float rumbleIntensity = 0.3f; // Intensity of the rumble
    [SerializeField] private float rumbleCooldown = 1f; // Cooldown time between rumbles

    private Gamepad gamepad1;
    private Gamepad gamepad2;
    private bool canRumble1 = true;
    private bool canRumble2 = true;

    void Start()
    {
        var gamepadsList = Gamepad.all;
        if (gamepadsList.Count > 0) gamepad1 = gamepadsList[0];
        if (gamepadsList.Count > 1) gamepad2 = gamepadsList[1];
    }

    void Update()
    {
        if (gamepad1 != null && gamepad1.buttonSouth.wasPressedThisFrame && canRumble1)
        {
            StartCoroutine(RumbleWithCooldown(gamepad1, () => canRumble1 = true));
            canRumble1 = false; // Prevent spamming
        }

        if (gamepad2 != null && gamepad2.buttonSouth.wasPressedThisFrame && canRumble2)
        {
            StartCoroutine(RumbleWithCooldown(gamepad2, () => canRumble2 = true));
            canRumble2 = false; // Prevent spamming
        }
    }

    private IEnumerator RumbleWithCooldown(Gamepad gamepad, System.Action onCooldownComplete)
    {
        gamepad.SetMotorSpeeds(rumbleIntensity, rumbleIntensity); // Start rumble
        yield return new WaitForSeconds(rumbleDuration); // Wait for rumble duration
        gamepad.SetMotorSpeeds(0f, 0f); // Stop rumble

        yield return new WaitForSeconds(rumbleCooldown - rumbleDuration); // Wait for cooldown
        onCooldownComplete?.Invoke(); // Allow rumble again
    }

    //--------------------------------------------
    //  [Header("Rumble Settings")]
    // [SerializeField] private float rumbleDuration = 0.2f; // Duration of the rumble in seconds
    // [SerializeField] private float rumbleIntensity = 0.3f; // Intensity of the rumble (0.0 to 1.0)

    // private Gamepad gamepad1;
    // private Gamepad gamepad2;

    // void Start()
    // {
    //     // Get the connected gamepads
    //     var gamepadsList = Gamepad.all;

    //     // If there are at least two gamepads connected, assign them
    //     if (gamepadsList.Count > 0)
    //         gamepad1 = gamepadsList[0]; // Gamepad 1
    //     if (gamepadsList.Count > 1)
    //         gamepad2 = gamepadsList[1]; // Gamepad 2
    // }

    // void Update()
    // {
    //     // Check if gamepad1 is not null and the "A" button (buttonSouth) is pressed
    //     if (gamepad1 != null && gamepad1.buttonSouth.wasPressedThisFrame)
    //     {
    //         StartCoroutine(RumbleGamepad(gamepad1)); // Trigger rumble for gamepad1
    //     }

    //     // Check if gamepad2 is not null and the "A" button (buttonSouth) is pressed
    //     if (gamepad2 != null && gamepad2.buttonSouth.wasPressedThisFrame)
    //     {
    //         StartCoroutine(RumbleGamepad(gamepad2)); // Trigger rumble for gamepad2
    //     }
    // }

    // private IEnumerator RumbleGamepad(Gamepad gamepad)
    // {
    //     if (gamepad != null)
    //     {
    //         // Start the rumble on the specific gamepad
    //         gamepad.SetMotorSpeeds(rumbleIntensity, rumbleIntensity); // Set both low and high frequency
    //         yield return new WaitForSeconds(rumbleDuration); // Wait for the rumble duration
    //         gamepad.SetMotorSpeeds(0f, 0f); // Stop the rumble
    //     }
    // }
}
