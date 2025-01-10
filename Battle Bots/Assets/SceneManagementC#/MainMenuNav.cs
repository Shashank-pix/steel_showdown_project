using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

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
                SceneManager.LoadScene(sceneNames[currentIndex]);
            }
        }
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < images.Length; i++)
        {
            bool isSelected = (i == currentIndex);
            texts[i].gameObject.SetActive(isSelected); // Enable/disable text based on selection
            //images[i].color = isSelected ? Color.green : Color.white; // Optional: change color to indicate selection
        }
    }

    private void ResetNavigation()
    {
        canNavigate = true;
    }
}
