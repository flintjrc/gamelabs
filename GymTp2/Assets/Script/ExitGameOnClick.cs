using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameOnClick : MonoBehaviour
{
    public void quitGame()
    {
        //allows to close the game (play mode) in the editor (found in unity tutorials)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //allow to quit the game completely
        Application.Quit();
#endif
    }
}
