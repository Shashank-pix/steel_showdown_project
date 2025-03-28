using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VS : MonoBehaviour
{
    
     [Header("Player-1 & Player-2 Animators")]
    [SerializeField] private Animator player1Animator;
    [SerializeField] private Animator player2Animator;

    private bool isPressed_P1 = false;
    private bool isPressed_P2 = false;

    void Update()
    {
        // Check if Player 1 presses "A" (Submit1)
        if (Input.GetButtonDown("Submit1"))
        {
            isPressed_P1 = true;
            Debug.Log("Player 1 pressed A");
        }

        // Check if Player 2 presses "A" (Submit2)
        if (Input.GetButtonDown("Submit2"))
        {
            isPressed_P2 = true;
            Debug.Log("Player 2 pressed A");
        }

        // If both players pressed "A", start animations
        if (isPressed_P1 && isPressed_P2)
        {
            PlayAnimations();
        }
    }

    void PlayAnimations()
    {
        Debug.Log("Both players pressed A! Playing animations...");
        
        // Play animations (Make sure animation state names match)
        player1Animator.SetTrigger("StartAnimation");
        player2Animator.SetTrigger("StartAnimation");

        // Reset confirmation so it doesn't repeat
        isPressed_P1 = false;
        isPressed_P2 = false;
    }

    //-----------------------------------------------
    //  [Header("Player-1 & Player-2 Animators")]
    // [SerializeField] private Animator player1Animator;
    // [SerializeField] private Animator player2Animator;

    // [Header("Canvas for Camera Shake")]
    // [SerializeField] private RectTransform canvasTransform; // Assign your Canvas RectTransform

    // private bool isPressed_P1 = false;
    // private bool isPressed_P2 = false;

    // void Update()
    // {
    //     // Check if Player 1 presses "A" (Submit1)
    //     if (Input.GetButtonDown("Submit1"))
    //     {
    //         isPressed_P1 = true;
    //         Debug.Log("Player 1 pressed A");
    //     }

    //     // Check if Player 2 presses "A" (Submit2)
    //     if (Input.GetButtonDown("Submit2"))
    //     {
    //         isPressed_P2 = true;
    //         Debug.Log("Player 2 pressed A");
    //     }

    //     // If both players pressed "A", start animations with camera shake
    //     if (isPressed_P1 && isPressed_P2)
    //     {
    //         PlayAnimationsWithCameraShake();
    //     }
    // }

    // void PlayAnimationsWithCameraShake()
    // {
    //     Debug.Log("Both players confirmed! Playing animations with camera shake...");

    //     // Play animations immediately
    //     player1Animator.SetTrigger("StartAnimation");
    //     player2Animator.SetTrigger("StartAnimation");

    //     // Start camera shake effect
    //     StartCoroutine(ShakeCanvas(0.5f, 5f)); // Shake for 0.5 seconds with intensity 5

    //     // Reset confirmation
    //     isPressed_P1 = false;
    //     isPressed_P2 = false;
    // }

    // IEnumerator ShakeCanvas(float duration, float magnitude)
    // {
    //     Vector3 originalPos = canvasTransform.anchoredPosition;
    //     float elapsed = 0f;

    //     while (elapsed < duration)
    //     {
    //         float offsetX = Random.Range(-1f, 1f) * magnitude;
    //         float offsetY = Random.Range(-1f, 1f) * magnitude;
    //         canvasTransform.anchoredPosition = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

    //         elapsed += Time.deltaTime;
    //         yield return null;
    //     }

    //     canvasTransform.anchoredPosition = originalPos; // Reset position
    // }

}
