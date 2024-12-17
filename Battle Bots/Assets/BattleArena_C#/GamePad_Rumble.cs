using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePad_Rumble : MonoBehaviour
 {
   

    //---------------------------------------------------------------------------
//      [SerializeField] private Transform spawnPoint1; // Assign Player 1's spawn point
//     [SerializeField] private Transform spawnPoint2; // Assign Player 2's spawn point
//     private GameObject bot1; // Bot controlled by Player 1
//     private GameObject bot2; // Bot controlled by Player 2

//     private const float rumbleDuration = 0.5f;
//     private const float rumbleIntensity = 0.75f;

//     void Start()
//     {
//         StartCoroutine(AssignBots()); // Assign bots after spawn
//     }

//     private IEnumerator AssignBots()
//     {
//         yield return new WaitForSeconds(1f); // Wait for bots to spawn

//         bot1 = FindBotAtSpawnPoint(spawnPoint1);
//         bot2 = FindBotAtSpawnPoint(spawnPoint2);

//         if (bot1 == null || bot2 == null)
//         {
//             Debug.LogError("Bots not found at spawn points. Ensure they are correctly instantiated.");
//         }
//         else
//         {
//             Debug.Log("Bots successfully assigned.");
//         }
//     }

//     private GameObject FindBotAtSpawnPoint(Transform spawnPoint)
//     {
//         Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, 5f); // Adjust radius as needed
//         Debug.Log($"Colliders detected near {spawnPoint.name}: {colliders.Length}");

//         foreach (var collider in colliders)
//         {
//             Debug.Log($"Collider found: {collider.gameObject.name}, Tag: {collider.tag}");
//             if (collider.CompareTag("Bot"))
//             {
//                 return collider.gameObject;
//             }
//         }

//         return null;
//     }

//     private void OnCollisionEnter(Collision collision)
//     {
//         if (collision.collider.CompareTag("Ground")) return; // Ignore ground collisions

//         if (collision.gameObject == bot1)
//         {
//             Debug.Log("Bot1 collided. Triggering rumble for Player 1.");
//             TriggerRumble(Gamepad.all[0]); // Assuming Player 1 is on Gamepad 0
//         }
//         else if (collision.gameObject == bot2)
//         {
//             Debug.Log("Bot2 collided. Triggering rumble for Player 2.");
//             TriggerRumble(Gamepad.all[1]); // Assuming Player 2 is on Gamepad 1
//         }
//     }

//     private void TriggerRumble(Gamepad gamepad)
//     {
//         if (gamepad == null)
//         {
//             Debug.LogWarning("No gamepad found to trigger rumble.");
//             return;
//         }

//         gamepad.SetMotorSpeeds(rumbleIntensity, rumbleIntensity); // Start rumble
//         StartCoroutine(StopRumbleAfterDelay(gamepad));
//     }

//     private IEnumerator StopRumbleAfterDelay(Gamepad gamepad)
//     {
//         yield return new WaitForSeconds(rumbleDuration);
//         gamepad.SetMotorSpeeds(0, 0); // Stop rumble
//     }


   
}
