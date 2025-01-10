using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkParticle_Handler : MonoBehaviour
{
   [SerializeField] private GameObject ParticleSystem_Prefab;
   [SerializeField] private Transform CustomSpawnPoint;
  

   //private ParticleSystem particleSystem;

   void OnCollisionEnter(Collision other)
   {
    //to check if the bot collides with wall
       if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacles") || other.gameObject.CompareTag("Bot"))
       {
        Debug.Log($"{gameObject.name} collided with a wall or an Obstacle or a bot");

       //Instantiate the particle prefab on the collision point
        if(ParticleSystem_Prefab != null)
        {
             // Use the custom spawn point for particle system
               Vector3 spawnPosition = CustomSpawnPoint.position;
              
            //gets the collision point
            //Vector3 CollisionPoint = other.contacts[0].point;

            //Instantiate the particle at the collision point without causing any rotation
            GameObject instantiatedParticles = Instantiate(ParticleSystem_Prefab, spawnPosition, Quaternion.identity);

            //playing the particle system
            ParticleSystem particleSystem = instantiatedParticles.GetComponent<ParticleSystem>();
            if(ParticleSystem_Prefab != null)
            {
                particleSystem.Play();
            }

            //Destroy the particle system after its duration to avoid cluttering
            Destroy(instantiatedParticles,2f);
        }
        else
        {
            Debug.LogError(" Particle Prefab is not assigned in the inspector");
        }

       }
   }
}
