using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndingCollisionController : MonoBehaviour {
    //Represents the ending menu
    public GameObject EndingMenuUI;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EndingMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
