using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }
   public void OnPressHome()
   {
    SceneManager.LoadScene(0);
   }
   public void OnPressQuit()
   {
    Application.Quit();
   }
}
