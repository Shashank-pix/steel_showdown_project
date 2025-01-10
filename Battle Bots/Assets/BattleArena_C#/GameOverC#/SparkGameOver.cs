using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkGameOver : MonoBehaviour
{
   // delay the play time of the particle

   [Header("Particle Settings")]

   [SerializeField] private ParticleSystem _particleSystem;
   [SerializeField] private float DelayTime = 3f;

   private float Timer;

   void Start()
   {
    Timer = DelayTime;
   }
    
   void Update()
   {
    if (_particleSystem != null && Timer >0)
    {
        Timer -= Time.deltaTime;//countdown
        if(Timer<=0)
        {
            _particleSystem.Play();
        }
    }
   }
}
