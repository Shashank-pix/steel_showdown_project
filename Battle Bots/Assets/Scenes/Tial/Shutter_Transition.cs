
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shutter_Transition : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.J))
        {
            SceneManager.LoadScene(1);
        }
    }
}
