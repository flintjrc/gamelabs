using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    // Allows to verify if the game is paused
    public static bool IsGamePaused;

    //Represents the menu
    public GameObject PauseMenuUI;

	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.P))
	    {
	        if (IsGamePaused)
	        {
	            ResumeGame();
	        }
	        else
	        {
	            PauseGame();
	        }
	    }
	}

    void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_menu");
    }
}
