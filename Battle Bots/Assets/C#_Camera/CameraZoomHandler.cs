using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomHandler : MonoBehaviour
{
     public Camera mainCamera;
    public float minFOV = 40f;
    public float maxFOV = 60f;
    public float zoomSpeed = 5f;
    public float distanceThreshold = 10f;
    public float followSpeed = 2f; // Speed at which the camera follows the bots
    public Vector3 offset = new Vector3(0, 10, -10); // Offset to keep the camera at a desirable position

    private List<GameObject> bots = new List<GameObject>();

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        bots.AddRange(GameObject.FindGameObjectsWithTag("Bot"));
    }

    void Update()
    {
        // Update the list of bots dynamically
        bots = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bot"));

        if (bots.Count < 2) return;

        // Calculate the midpoint between the two bots
        Vector3 midpoint = (bots[0].transform.position + bots[1].transform.position) / 2;

        // Smoothly move the camera towards the midpoint with an offset
        Vector3 targetPosition = midpoint + offset;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * followSpeed);

        // Make the camera look directly at the midpoint
        mainCamera.transform.LookAt(midpoint);

        // Calculate the distance between the two bots
        float distance = Vector3.Distance(bots[0].transform.position, bots[1].transform.position);

        // Adjust the camera's field of view based on the distance between the bots
        float targetFOV = Mathf.Lerp(minFOV, maxFOV, distance / distanceThreshold);
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }




    //----------------------------------------------------
    // public Camera mainCamera;
    // public float minFOV = 40f;
    // public float maxFOV = 60f;
    // public float zoomSpeed = 5f;
    // public float distanceThreshold = 10f;
    // public float followSpeed = 2f; // Speed at which the camera follows the bots
    // public Vector3 offset = new Vector3(0, 10, -10); // Offset to keep the camera at a desirable position

    // private List<GameObject> bots = new List<GameObject>();

    // void Start()
    // {
    //     if (mainCamera == null)
    //     {
    //         mainCamera = Camera.main;
    //     }

    //     bots.AddRange(GameObject.FindGameObjectsWithTag("Bot"));
    // }

    // void Update()
    // {
    //     // Update the list of bots dynamically
    //     bots = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bot"));

    //     if (bots.Count < 2) return;

    //     // Calculate the midpoint between the two bots
    //     Vector3 midpoint = (bots[0].transform.position + bots[1].transform.position) / 2;

    //     // Smoothly move the camera towards the midpoint with an offset
    //     Vector3 targetPosition = midpoint + offset;
    //     mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * followSpeed);

    //     // Calculate the distance between the two bots
    //     float distance = Vector3.Distance(bots[0].transform.position, bots[1].transform.position);

    //     // Adjust the camera's field of view based on the distance between the bots
    //     float targetFOV = Mathf.Lerp(minFOV, maxFOV, distance / distanceThreshold);
    //     mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    // }


    //-------------------------------------------------------------
    //  public Camera mainCamera;
    // public float minFOV = 40f;
    // public float maxFOV = 60f;
    // public float zoomSpeed = 5f;
    // public float distanceThreshold = 10f;

    // private List<GameObject> bots = new List<GameObject>();

    // void Start()
    // {
    //     if (mainCamera == null)
    //     {
    //         mainCamera = Camera.main;
    //     }

    //     // Optionally, find any bots that might have been already in the scene (if necessary)
    //     bots.AddRange(GameObject.FindGameObjectsWithTag("Bot"));
    // }

    // void Update()
    // {
    //     // Update the list of bots in case new ones have been spawned
    //     bots = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bot"));

    //     if (bots.Count < 2) return;

    //     // Calculate the distance between the two bots
    //     float distance = Vector3.Distance(bots[0].transform.position, bots[1].transform.position);
    //     Debug.Log("Distance between bots: " + distance);

    //     // Adjust the camera's field of view based on the distance between the bots
    //     float targetFOV = Mathf.Lerp(minFOV, maxFOV, distance / distanceThreshold);
    //     mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    //     Debug.Log("Target FOV: " + targetFOV + ", Current FOV: " + mainCamera.fieldOfView);
    // }

   
}
