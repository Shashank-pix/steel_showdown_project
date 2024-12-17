using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePad_Checker : MonoBehaviour
{
     void Start()
    {
        foreach (var gamepad in Gamepad.all)
        {
            Debug.Log("Detected gamepad: " + gamepad.name);
        }

        if (Gamepad.all.Count == 0)
        {
            Debug.LogError("No gamepads detected! Ensure they are connected.");
        }
    }
}

