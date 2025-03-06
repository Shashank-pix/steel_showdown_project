using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class MainMenuNav : MonoBehaviour
{
     [Header("Menu Settings")]
    [SerializeField] private Image[] images;
    [SerializeField] private TMP_Text[] texts;
    [SerializeField] private string[] sceneNames;

    private int currentIndex = 0;

    private InputAction navigateAction;
    private InputAction submitAction;

    private float navigationThreshold = 0.5f; // Threshold for joystick movement
    private bool canNavigate = true; // Prevents multiple rapid inputs

    [Header("Gate Animators")]
    [SerializeField] private Animator redFWAnimator;  // Red gate Animator
    [SerializeField] private Animator blueFWAnimator; // Blue gate Animator

    [Header("Animation Settings")]
    [SerializeField] private float gateAnimationDuration = 2.5f; 

    private void Awake()
    {
        // Initialize input actions
        navigateAction = new InputAction("Navigate", binding: "<Gamepad>/leftStick");
        submitAction = new InputAction("Submit", binding: "<Gamepad>/buttonSouth");

        // Add callbacks
        navigateAction.performed += OnNavigate;
        submitAction.performed += OnSubmit;

        // Enable actions
        navigateAction.Enable();
        submitAction.Enable();
    }

    private void OnDestroy()
    {
        // Disable actions when the object is destroyed
        navigateAction.Disable();
        submitAction.Disable();
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (!canNavigate) return; // Prevents rapid navigation

        Vector2 navigation = context.ReadValue<Vector2>();

        if (navigation.y > navigationThreshold) // Up
        {
            currentIndex = (currentIndex - 1 + images.Length) % images.Length;
            canNavigate = false;
        }
        else if (navigation.y < -navigationThreshold) // Down
        {
            currentIndex = (currentIndex + 1) % images.Length;
            canNavigate = false;
        }

        UpdateSelection();

        // Re-enable navigation after a short delay
        Invoke(nameof(ResetNavigation), 0.2f); // Adjust the delay as needed
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (currentIndex == 3) // Assuming 4th image is the quit option
            {
                Application.Quit();
            }
            else if (currentIndex >= 0 && currentIndex < sceneNames.Length)
            {
                StartCoroutine(PlayGateAnimationsAndSwitchScene(sceneNames[currentIndex]));
            }
        }
    }

    private IEnumerator PlayGateAnimationsAndSwitchScene(string sceneNames)
    {

          Debug.Log("Triggering gate close animations...");

    // Trigger the closing animation from Any State
    if (redFWAnimator != null)
        redFWAnimator.SetTrigger("CloseGate");

    if (blueFWAnimator != null)
        blueFWAnimator.SetTrigger("CloseGate");

    // Wait for animation to complete (set the correct duration)
    yield return new WaitForSeconds(gateAnimationDuration);

    Debug.Log("Animation finished. Switching scene...");

    SceneManager.LoadScene(sceneNames); // Load the next scene after animations finish
        // if (redFWAnimator != null)
        //     redFWAnimator.SetTrigger("CloseGate"); // Trigger Red gate animation

        // if (blueFWAnimator != null)
        //     blueFWAnimator.SetTrigger("CloseGate"); // Trigger Blue gate animation

        // // Wait for the longest animation to finish before switching scenes
        // float redAnimationTime = redFWAnimator != null ? redFWAnimator.GetCurrentAnimatorStateInfo(0).length : 0f;
        // float blueAnimationTime = blueFWAnimator != null ? blueFWAnimator.GetCurrentAnimatorStateInfo(0).length : 0f;

        // float maxAnimationTime = Mathf.Max(redAnimationTime, blueAnimationTime);
        // yield return new WaitForSeconds(maxAnimationTime);

        // SceneManager.LoadScene(sceneName); // Load the next scene after animations finish
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < images.Length; i++)
        {
            bool isSelected = (i == currentIndex);
            texts[i].gameObject.SetActive(isSelected); // Enable/disable text based on selection
        }
    }

    private void ResetNavigation()
    {
        canNavigate = true;
    }




    //================================================================
    // [Header("Menu Settings")]
    // [SerializeField] private Image[] images;
    // [SerializeField] private TMP_Text[] texts;
    // [SerializeField] private string[] sceneNames;

    // private int currentIndex = 0;

    // private InputAction navigateAction;
    // private InputAction submitAction;

    // private float navigationThreshold = 0.5f; // Threshold for joystick movement
    // private bool canNavigate = true; // Prevents multiple rapid inputs

    // private void Awake()
    // {
    //     // Initialize input actions
    //     navigateAction = new InputAction("Navigate", binding: "<Gamepad>/leftStick");
    //     submitAction = new InputAction("Submit", binding: "<Gamepad>/buttonSouth");

    //     // Add callbacks
    //     navigateAction.performed += OnNavigate;
    //     submitAction.performed += OnSubmit;

    //     // Enable actions
    //     navigateAction.Enable();
    //     submitAction.Enable();
    // }

    // private void OnDestroy()
    // {
    //     // Disable actions when the object is destroyed
    //     navigateAction.Disable();
    //     submitAction.Disable();
    // }

    // public void OnNavigate(InputAction.CallbackContext context)
    // {
    //     if (!canNavigate) return; // Prevents rapid navigation

    //     Vector2 navigation = context.ReadValue<Vector2>();

    //     if (navigation.y > navigationThreshold) // Up
    //     {
    //         currentIndex = (currentIndex - 1 + images.Length) % images.Length;
    //         canNavigate = false;
    //     }
    //     else if (navigation.y < -navigationThreshold) // Down
    //     {
    //         currentIndex = (currentIndex + 1) % images.Length;
    //         canNavigate = false;
    //     }

    //     UpdateSelection();

    //     // Re-enable navigation after a short delay
    //     Invoke(nameof(ResetNavigation), 0.2f); // Adjust the delay as needed
    // }

    // public void OnSubmit(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //     {
    //         if (currentIndex == 3) // Assuming 4th image is the quit option
    //         {
    //             Application.Quit();
    //         }
    //         else if (currentIndex >= 0 && currentIndex < sceneNames.Length)
    //         {
    //             SceneManager.LoadScene(sceneNames[currentIndex]);
    //         }
    //     }
    // }

    // private void UpdateSelection()
    // {
    //     for (int i = 0; i < images.Length; i++)
    //     {
    //         bool isSelected = (i == currentIndex);
    //         texts[i].gameObject.SetActive(isSelected); // Enable/disable text based on selection
    //         //images[i].color = isSelected ? Color.green : Color.white; // Optional: change color to indicate selection
    //     }
    // }

    // private void ResetNavigation()
    // {
    //     canNavigate = true;
    // }
}
