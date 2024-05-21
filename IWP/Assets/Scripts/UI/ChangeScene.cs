//Class that defines change scene behavior.

//Import libraries. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //Loads scene.
    public void ChangeMenuScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        //the Scenes need to be added into the build settings
    }

}
