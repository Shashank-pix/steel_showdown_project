using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
      private static BackgroundMusicManager instance;
    
    [SerializeField] private string scene1 = "MainMenu"; // Set your first scene name
    [SerializeField] private string scene2 = "Selectn_Test"; // Set your second scene name

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep music playing across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
            return;
        }
        
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene change event
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != scene1 && scene.name != scene2)
        {
            Destroy(gameObject); // Stop music if entering a different scene
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to prevent memory leaks
    }
}
