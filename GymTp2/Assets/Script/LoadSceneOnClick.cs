using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {
    public void LoadSceneByIndex(int sceneIndex)
    {
        //Load the scene given in parameters.
        //IMPORTANT! Scenes must be added in the build settings first
        SceneManager.LoadScene(sceneIndex);
    }
}
