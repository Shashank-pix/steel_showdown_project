using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition_Gates : MonoBehaviour
{
   public Slider player1HealthBar;
    public Slider player2HealthBar;

    public Animator player1Animator;
    public Animator player2Animator;

   // public string WinnerScene = "WinnerMenu"; // Scene to load after animations

    private bool animationTriggered = false;

    void Update()
    {
        if (!animationTriggered)
        {
            if (player1HealthBar.value <= 0 || player2HealthBar.value <= 0)
            {
                animationTriggered = true;
                StartCoroutine(PlayGameOverAnimations());
            }
        }
    }

    private IEnumerator PlayGameOverAnimations()
    {
        Debug.Log("Playing Game Over Animations...");

        // Check if both animators exist before triggering animations
        if (player1Animator != null)
        {
            player1Animator.SetTrigger("CloseGate");
        }
        if (player2Animator != null)
        {
            player2Animator.SetTrigger("CloseGate");
        }

        // Wait for the longest animation to complete
        float maxAnimTime = 1f; // Default delay if no animator exists
        if (player1Animator != null)
        {
            maxAnimTime = Mathf.Max(maxAnimTime, player1Animator.GetCurrentAnimatorStateInfo(0).length);
        }
        if (player2Animator != null)
        {
            maxAnimTime = Mathf.Max(maxAnimTime, player2Animator.GetCurrentAnimatorStateInfo(0).length);
        }

        yield return new WaitForSeconds(maxAnimTime);

        //Debug.Log("Switching to Winner Scene...");
        //SceneManager.LoadScene(WinnerScene);
    }
}
