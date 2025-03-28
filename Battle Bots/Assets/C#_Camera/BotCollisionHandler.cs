using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCollisionHandler : MonoBehaviour
{
     private CameraZoomHandler cameraZoomHandler;

    void Start()
    {
        cameraZoomHandler = FindObjectOfType<CameraZoomHandler>(); // Find camera script
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bot")) // Detect bot vs bot collision
        {
            if (cameraZoomHandler != null)
            {
                cameraZoomHandler.StartCameraShake(0.2f, 0.1f); // Trigger camera shake
            }
        }
    }
}
