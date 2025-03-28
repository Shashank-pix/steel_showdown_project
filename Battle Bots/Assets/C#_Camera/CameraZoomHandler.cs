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
    public float followSpeed = 2f; 
    public Vector3 offset = new Vector3(0, 10, -10);
    
    private List<GameObject> bots = new List<GameObject>();
    private bool isShaking = false;
    
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
        bots = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bot"));

        if (bots.Count < 2) return;

        Vector3 midpoint = (bots[0].transform.position + bots[1].transform.position) / 2;
        Vector3 targetPosition = midpoint + offset;
        
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * followSpeed);
        mainCamera.transform.LookAt(midpoint);

        float distance = Vector3.Distance(bots[0].transform.position, bots[1].transform.position);
        float targetFOV = Mathf.Lerp(minFOV, maxFOV, distance / distanceThreshold);
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }

    public void StartCameraShake(float duration, float magnitude)
    {
        if (!isShaking)
        {
            StartCoroutine(CameraShake(duration, magnitude));
        }
    }

    private IEnumerator CameraShake(float duration, float magnitude)
    {
        isShaking = true;
        Vector3 originalPos = mainCamera.transform.position;
        
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            mainCamera.transform.position = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalPos;
        isShaking = false;
    }

    //----------------------------------------------
    //  public Camera mainCamera;
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

    //     // Make the camera look directly at the midpoint
    //     mainCamera.transform.LookAt(midpoint);

    //     // Calculate the distance between the two bots
    //     float distance = Vector3.Distance(bots[0].transform.position, bots[1].transform.position);

    //     // Adjust the camera's field of view based on the distance between the bots
    //     float targetFOV = Mathf.Lerp(minFOV, maxFOV, distance / distanceThreshold);
    //     mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    // }
   
}
