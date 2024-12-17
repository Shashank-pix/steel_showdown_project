using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_BotMnt : MonoBehaviour
{
    
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Automatic forward movement
        Vector3 forwardMovement = transform.forward * moveSpeed;

        // Add directional control from player input
        Vector3 directionControl = new Vector3(moveInput.x, 0, moveInput.y);

        // Combine automatic and directional movement
        rb.velocity = forwardMovement + directionControl * moveSpeed;
    }

    // Input callback for movement
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Input callback for attack
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log($"{gameObject.name} attacked!");
            // Add attack logic here (e.g., play an animation or reduce enemy health)
        }
    }

   //-----------------Tested Movement(Works)-------------------------
    // [Header("Movement Settings")]
    // [SerializeField] private float moveSpeed = 5f; // Forward speed
    // [SerializeField] private float turnSpeed = 90f; // Rotation speed (degrees per second)

    // [Header("Attack Settings")]
    // [SerializeField] private float attackCooldown = 1f; // Cooldown between attacks
    // private bool canAttack = true;

    // private Vector2 moveInput; // Stores input from the left stick
    // private BotInputActions controls; // Reference to the Input Actions
    // private Rigidbody rb; // Rigidbody for physics-based movement

    // private void Awake()
    // {
    //     // Initialize controls
    //     controls = new BotInputActions();
    // }

    // private void OnEnable()
    // {
    //     // Enable input actions and subscribe to events
    //     controls.Enable();
    //     controls.GamePad_Controls.Move.performed += OnMoveInput;
    //     controls.GamePad_Controls.Move.canceled += OnMoveInput;
    //     controls.GamePad_Controls.Attack.performed += OnAttackInput;
    // }

    // private void OnDisable()
    // {
    //     // Unsubscribe from events and disable controls
    //     controls.GamePad_Controls.Move.performed -= OnMoveInput;
    //     controls.GamePad_Controls.Move.canceled -= OnMoveInput;
    //     controls.GamePad_Controls.Attack.performed -= OnAttackInput;
    //     controls.Disable();
    // }

    // private void Start()
    // {
    //     rb = GetComponent<Rigidbody>();
    // }

    // private void FixedUpdate()
    // {
    //     // Automatic forward movement
    //     Vector3 forwardMovement = transform.forward * moveSpeed * Time.fixedDeltaTime;
    //     rb.MovePosition(rb.position + forwardMovement);

    //     // Apply redirection based on left stick input
    //     if (moveInput != Vector2.zero)
    //     {
    //         float targetRotation = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg;
    //         Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);
    //         rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.fixedDeltaTime));
    //     }
    // }

    // private void OnMoveInput(InputAction.CallbackContext context)
    // {
    //     // Update move input when the left stick is moved
    //     moveInput = context.ReadValue<Vector2>();
    // }

    // private void OnAttackInput(InputAction.CallbackContext context)
    // {
    //     // Handle attack input
    //     if (canAttack)
    //     {
    //         Debug.Log($"{gameObject.name} performed an attack!");
    //         StartCoroutine(AttackCooldown());
    //     }
    // }

    // private IEnumerator AttackCooldown()
    // {
    //     // Cooldown logic for the attack
    //     canAttack = false;
    //     yield return new WaitForSeconds(attackCooldown);
    //     canAttack = true;
    // }
}


