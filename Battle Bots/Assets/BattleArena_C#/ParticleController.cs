using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleController : MonoBehaviour
{
      [Header("ParticleSystem Settings")]
    [SerializeField] private List<ParticleSystem> FireParticle; // List of particle systems

    [Header("Timing Settings")]
    [SerializeField] private float ActiveFireDuration = 3f; // The fire particle will play for 3 seconds
    [SerializeField] private float InActiveFireDuration = 3f; // After every 5 seconds, the particle will play

    // Start is called before the first frame update
    void Start()
    {
        if (FireParticle == null || FireParticle.Count == 0)
        {
            Debug.LogError("Particle system not assigned");
            return;
        }

        StartCoroutine(ManageParticleSystem());
    }

    private IEnumerator ManageParticleSystem()
    {
        while (true) // This will keep looping
        {
            foreach (var particleSystem in FireParticle)
            {
                if (particleSystem != null) // Ensure the particle system is assigned
                {
                    particleSystem.gameObject.SetActive(true);
                    yield return new WaitForSeconds(Random.Range(0f, ActiveFireDuration / FireParticle.Count)); // Random delay for each activation
                }
            }

            yield return new WaitForSeconds(ActiveFireDuration); // Keep particles active for the defined duration

            foreach (var particleSystem in FireParticle)
            {
                if (particleSystem != null) // Ensure the particle system is assigned
                {
                    particleSystem.gameObject.SetActive(false);
                    yield return new WaitForSeconds(Random.Range(0f, InActiveFireDuration / FireParticle.Count)); // Random delay for deactivation
                }
            }

            yield return new WaitForSeconds(InActiveFireDuration); // Wait before the next cycle
        }
    }


    //-----------------------------------------------------
//     [Header("ParticleSystem Settings")]
//     [SerializeField] private List<ParticleSystem> FireParticle; // list of particles

//     [Header("Timing Settings")]
//     [SerializeField] private float ActiveFireDuration = 3f; //The fire particle will play for 3 seconds
//     [SerializeField]private float InActiveFireDuration = 3f; //after every 5 seconds the particle will play

//     // Start is called before the first frame update
//     void Start()
//     {
//         if(FireParticle == null || FireParticle.Count ==0)
//         {
//             Debug.LogError("Particle system not assigned");
//             return;
//         }

//         StartCoroutine (ManageParticleSystem());
//     }

//   private IEnumerator ManageParticleSystem()
//    {

//     while(true) // it will be playing in loop
//     {
//         foreach(var ParticleSystem in FireParticle)
//         {
//             if(ParticleSystem != null) // if the particle system assigned
//             {
//                 ParticleSystem.gameObject.SetActive(true);

//             }
//         }
//         yield return new WaitForSeconds(ActiveFireDuration);
        

//          foreach(var ParticleSystem in FireParticle)
//         {
//             if(ParticleSystem != null) // if the particle system assigned
//             {
//                 ParticleSystem.gameObject.SetActive(false);

//             }
//         }
//         yield return new WaitForSeconds(InActiveFireDuration);



//         // Fire.gameObject.SetActive(true);
//         // yield return new WaitForSeconds(ActiveFireDuration);

//         // Fire.gameObject.SetActive(false);
//         // yield return new WaitForSeconds(InActiveFireDuration);

//     }

//    }
}
