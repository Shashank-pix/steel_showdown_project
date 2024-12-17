using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BotMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;  // Forward movement speed
    [SerializeField] private float rotationSpeed = 5f;  // Rotation speed for the bot

    private Vector2 moveInput;  // Input direction for controlling movement and rotation
    private Rigidbody rb;
    private Vector3 currentForwardDirection;  // Tracks the current forward direction of the bot

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentForwardDirection = transform.forward;  // Initially, move in the current facing direction
    }

    private void FixedUpdate()
    {
        // Handle forward movement (automatic)
        MoveForward();

        // Handle rotation (controlled by the player)
        RotateBot();
    }

    // Move the bot forward at a constant speed (automatic movement)
    private void MoveForward()
    {
        Vector3 forwardDirection = currentForwardDirection * moveSpeed * Time.fixedDeltaTime;

        // Apply the movement using Rigidbody for smooth physics-based movement
        rb.MovePosition(rb.position + forwardDirection);
    }

    // Rotate the bot based on player input (using the joystick)
    private void RotateBot()
    {
        if (moveInput != Vector2.zero)
        {
            // Get the direction from the joystick input
            Vector3 targetDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

            // Calculate the rotation angle
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            // Smoothly rotate towards the target direction
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            // Update the forward direction after rotation
            currentForwardDirection = transform.forward;
        }
    }

    // Input actions for controlling movement direction (left stick)
    public void OnMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Neutralize velocity on collision for smooth movement
        rb.velocity = Vector3.zero;

        // Handle collisions with other bots or obstacles
        if (collision.gameObject.CompareTag("Bot"))
        {
            Debug.Log($"Collided with another bot: {collision.gameObject.name}");
        }
    }
    
    //------------------------------------------------------------------
    
//    private Vector2 moveInput;  // Input direction for controlling movement and rotation
//     //private bool isAttacking;   // Whether the bot is attacking or not

//     [Header("Movement Settings")]
//     [SerializeField]private float moveSpeed = 5f;  // Forward movement speed
//     [SerializeField]private float rotationSpeed = 5f;  // Rotation speed for the bot

//     private Rigidbody rb;
//     private Vector3 currentForwardDirection;  // Tracks the current forward direction of the bot

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//         currentForwardDirection = transform.forward;  // Initially, move in the current facing direction
//     }

//     void Update()
//     {
//         // Handle forward movement (automatic)
//         MoveForward();

//         // Handle rotation (controlled by the player)
//         RotateBot();

//         // Handle attacking input
//         //if (isAttacking)
//         //{
//             //HandleAttack();
//         //}
//     }

//     // Move the bot forward at a constant speed (automatic movement)
//     void MoveForward()
//     {
//         // Move the bot forward in the direction it is currently facing
//         Vector3 forwardDirection = currentForwardDirection * moveSpeed * Time.deltaTime;

//         // Apply the movement using Rigidbody for smooth physics-based movement
//         rb.MovePosition(rb.position + forwardDirection);
//     }

//     // Rotate the bot based on player input (using the entire joystick)
//     void RotateBot()
//     {
//         if (moveInput != Vector2.zero)
//         {
//             // Get the direction from the joystick input
//             Vector3 targetDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

//             // Calculate the rotation angle
//             Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

//             // Apply smooth rotation (slerp for smooth interpolation between rotations)
//             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

//             // Update the forward direction after rotation
//             currentForwardDirection = transform.forward;
//         }
//     }

//     // Handle the attack input (button press)
//     //void HandleAttack()
//     //{
//         // You can implement a specific attack animation or logic here
//         //Debug.Log("Bot is attacking!");
//         // Reset attacking status
//         //isAttacking = false;
//    // }

//     // Input actions for controlling movement direction (left stick)
//     public void OnMoveInput(Vector2 input)
//     {
//         moveInput = input;
//     }

//     // Input actions for attacking (South button or a specific button on the gamepad)
//     //public void OnAttackInput()
//     //{
//        // isAttacking = true;
//     //} 

   


}
    
   