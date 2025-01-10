using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttack : MonoBehaviour
{
   
    [Header("Attack Settings")]
    [SerializeField] private int attackDamage = 2; // Damage dealt on attack

    private bool isAttacking = false; // Flag to track attack input

    [Header("Animation Setting")]

    [SerializeField] private Animator animator;

    // Called by the Spawner when the Attack action is triggered
    public void OnAttackInput()
    {
        isAttacking = true; // Set attacking state
        TriggerAttackAnimation();
        Debug.Log($"{gameObject.name} is ready to attack.");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bot is currently attacking
        if (isAttacking)
        {
            // Reset the attack flag
            isAttacking = false;

            // Check if the collided object has a BotHealth component
            BotHealth targetHealth = collision.gameObject.GetComponent<BotHealth>();
            if (targetHealth != null)
            {
                // Reduce health of the target bot
                targetHealth.TakeDamage(attackDamage);
                Debug.Log($"{collision.gameObject.name} took {attackDamage} damage!");
            }
        }
    }

    private void TriggerAttackAnimation()
    {
        if(animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }
}
