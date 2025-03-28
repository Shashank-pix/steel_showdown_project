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

    private float navigationThreshold = 0.5f; 
    private bool canNavigate = true; 

    [Header("Gate Animators")]
    [SerializeField] private Animator redFWAnimator;  
    [SerializeField] private Animator blueFWAnimator; 

    [Header("Animation Settings")]
    [SerializeField] private float gateAnimationDuration = 2.5f; 

    [Header("Audio Settings")]
    [SerializeField] private AudioSource hoverSFX; 
    [SerializeField] private AudioSource confirmSFX; 
    [SerializeField] private AudioSource quitSFX; // Quit sound effect
    [SerializeField] private AudioSource gateCloseSFX; // Gate closing sound effect

    private void Awake()
    {
        navigateAction = new InputAction("Navigate", binding: "<Gamepad>/leftStick");
        submitAction = new InputAction("Submit", binding: "<Gamepad>/buttonSouth");

        navigateAction.performed += OnNavigate;
        submitAction.performed += OnSubmit;

        navigateAction.Enable();
        submitAction.Enable();
    }

    private void OnDestroy()
    {
        navigateAction.Disable();
        submitAction.Disable();
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (!canNavigate) return; 

        Vector2 navigation = context.ReadValue<Vector2>();

        if (navigation.x > navigationThreshold) 
        {
            currentIndex = (currentIndex + 1) % images.Length;
            canNavigate = false;
        }
        else if (navigation.x < -navigationThreshold) 
        {
            currentIndex = (currentIndex - 1 + images.Length) % images.Length;
            canNavigate = false;
        }

        UpdateSelection();
        Invoke(nameof(ResetNavigation), 0.2f);
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (confirmSFX != null)
            {
                confirmSFX.Play();
            }

            if (currentIndex == 3) 
            {
                StartCoroutine(PlayGateAndQuit());
            }
            else if (currentIndex >= 0 && currentIndex < sceneNames.Length)
            {
                StartCoroutine(PlayGateAnimationsAndSwitchScene(sceneNames[currentIndex]));
            }
        }
    }

    private IEnumerator PlayGateAnimationsAndSwitchScene(string sceneName)
    {
        Debug.Log("Triggering gate close animations...");

        if (gateCloseSFX != null)
        {
            gateCloseSFX.Play(); // Play gate close sound
        }

        if (redFWAnimator != null)
            redFWAnimator.SetTrigger("CloseGate");

        if (blueFWAnimator != null)
            blueFWAnimator.SetTrigger("CloseGate");

        yield return new WaitForSeconds(gateAnimationDuration);

        Debug.Log("Animation finished. Switching scene...");
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator PlayGateAndQuit()
    {
        Debug.Log("Closing gates before quitting...");

        if (gateCloseSFX != null)
        {
            gateCloseSFX.Play();
        }

        if (redFWAnimator != null)
            redFWAnimator.SetTrigger("CloseGate");

        if (blueFWAnimator != null)
            blueFWAnimator.SetTrigger("CloseGate");

        yield return new WaitForSeconds(gateAnimationDuration);

        if (quitSFX != null)
        {
            quitSFX.Play();
            yield return new WaitForSeconds(quitSFX.clip.length);
        }

        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < images.Length; i++)
        {
            bool isSelected = (i == currentIndex);
            texts[i].gameObject.SetActive(isSelected);

            if (isSelected && hoverSFX != null)
            {
                hoverSFX.Play();
            }
        }
    }

    private void ResetNavigation()
    {
        canNavigate = true;
    }

    //-------------------------------------------
    //  [Header("Menu Settings")]
    // [SerializeField] private Image[] images;
    // [SerializeField] private TMP_Text[] texts;
    // [SerializeField] private string[] sceneNames;

    // private int currentIndex = 0;

    // private InputAction navigateAction;
    // private InputAction submitAction;

    // private float navigationThreshold = 0.5f;
    // private bool canNavigate = true;

    // [Header("Gate Animators")]
    // [SerializeField] private Animator redFWAnimator;
    // [SerializeField] private Animator blueFWAnimator;

    // [Header("Animation Settings")]
    // [SerializeField] private float gateAnimationDuration = 2.5f;

    // [Header("Audio Settings")]
    // [SerializeField] private AudioSource hoverSFX; // Sound effect for hovering
    // [SerializeField] private AudioSource confirmSFX; // Sound effect for confirming selection

    // private void Awake()
    // {
    //     navigateAction = new InputAction("Navigate", binding: "<Gamepad>/leftStick");
    //     submitAction = new InputAction("Submit", binding: "<Gamepad>/buttonSouth");

    //     navigateAction.performed += OnNavigate;
    //     submitAction.performed += OnSubmit;

    //     navigateAction.Enable();
    //     submitAction.Enable();
    // }

    // private void OnDestroy()
    // {
    //     navigateAction.Disable();
    //     submitAction.Disable();
    // }

    // public void OnNavigate(InputAction.CallbackContext context)
    // {
    //     if (!canNavigate) return;

    //     Vector2 navigation = context.ReadValue<Vector2>();

    //     if (navigation.x > navigationThreshold)
    //     {
    //         currentIndex = (currentIndex + 1) % images.Length;
    //         canNavigate = false;
    //     }
    //     else if (navigation.x < -navigationThreshold)
    //     {
    //         currentIndex = (currentIndex - 1 + images.Length) % images.Length;
    //         canNavigate = false;
    //     }

    //     UpdateSelection();
    //     Invoke(nameof(ResetNavigation), 0.2f);
    // }

    // public void OnSubmit(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //     {
    //         // Play confirm SFX
    //         if (confirmSFX != null)
    //         {
    //             confirmSFX.Play();
    //         }

    //         if (currentIndex == 3)
    //         {
    //             Application.Quit();
    //         }
    //         else if (currentIndex >= 0 && currentIndex < sceneNames.Length)
    //         {
    //             StartCoroutine(PlayGateAnimationsAndSwitchScene(sceneNames[currentIndex]));
    //         }
    //     }
    // }

    // private IEnumerator PlayGateAnimationsAndSwitchScene(string sceneName)
    // {
    //     Debug.Log("Triggering gate close animations...");

    //     if (redFWAnimator != null)
    //         redFWAnimator.SetTrigger("CloseGate");

    //     if (blueFWAnimator != null)
    //         blueFWAnimator.SetTrigger("CloseGate");

    //     yield return new WaitForSeconds(gateAnimationDuration);

    //     Debug.Log("Animation finished. Switching scene...");
    //     SceneManager.LoadScene(sceneName);
    // }

    // private void UpdateSelection()
    // {
    //     for (int i = 0; i < images.Length; i++)
    //     {
    //         bool isSelected = (i == currentIndex);
    //         texts[i].gameObject.SetActive(isSelected);

    //         if (isSelected && hoverSFX != null)
    //         {
    //             hoverSFX.Play(); // Play hover sound effect when highlighting a new option
    //         }
    //     }
    // }

    // private void ResetNavigation()
    // {
    //     canNavigate = true;
    // }

    

}
