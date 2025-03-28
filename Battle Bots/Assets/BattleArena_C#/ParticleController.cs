using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleController : MonoBehaviour
{
     [Header("ParticleSystem Settings")]
    [SerializeField] private List<ParticleSystem> FireParticles; // List of fire particle systems
    [SerializeField] private List<int> InitialActiveIndices; // Indices of the first 4 particles

    [Header("Timing Settings")]
    [SerializeField] private float InitialDuration = 3f;  // First 3s with 4 chosen particles
    [SerializeField] private float ActiveFireDuration = 3f;  // Fire stays active for this duration
    [SerializeField] private float InActiveFireDuration = 3f; // Wait time before activating another fire

    void Start()
    {
        if (FireParticles == null || FireParticles.Count < 4)
        {
            Debug.LogError("At least 4 fire particle systems are required.");
            return;
        }

        if (InitialActiveIndices.Count != 4)
        {
            Debug.LogError("Please assign exactly 4 indices in the InitialActiveIndices list.");
            return;
        }

        StartCoroutine(InitializeParticles());
    }

    private IEnumerator InitializeParticles()
    {
        // Activate the first 4 specific particles
        foreach (var particle in FireParticles)
        {
            particle.gameObject.SetActive(false);
        }

        foreach (int index in InitialActiveIndices)
        {
            if (index >= 0 && index < FireParticles.Count)
            {
                FireParticles[index].gameObject.SetActive(true);
            }
        }

        yield return new WaitForSeconds(InitialDuration); // Keep them active for 3 seconds

        // Start the normal cycle
        StartCoroutine(ManageParticleSystem());
    }

    private IEnumerator ManageParticleSystem()
    {
        while (true)
        {
            // Deactivate all particles before activating new ones
            foreach (var particle in FireParticles)
            {
                particle.gameObject.SetActive(false);
            }

            // Select two unique random indices
            int index1 = Random.Range(0, FireParticles.Count);
            int index2;
            do
            {
                index2 = Random.Range(0, FireParticles.Count);
            } while (index2 == index1); // Ensure the second index is different

            // Activate the two selected fire particles
            FireParticles[index1].gameObject.SetActive(true);
            FireParticles[index2].gameObject.SetActive(true);

            yield return new WaitForSeconds(ActiveFireDuration); // Keep them active

            yield return new WaitForSeconds(InActiveFireDuration); // Wait before switching
        }
    }


    

   
}
