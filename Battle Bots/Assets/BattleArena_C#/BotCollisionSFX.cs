using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCollisionSFX : MonoBehaviour
{
    [Header("Collision Sound Effects")]
    [SerializeField] private AudioClip[] collisionSFX; // Array of 4 sound effects

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionSFX.Length > 0)
        {
            int randomIndex = Random.Range(0, collisionSFX.Length); // Pick a random clip
            PlaySound(collisionSFX[randomIndex]);
        }
    }

    void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
