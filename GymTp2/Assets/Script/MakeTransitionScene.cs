using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MakeTransitionScene : MonoBehaviour {

    private GameObject Saving;
    [SerializeField] private int NextLevel;
    private BoxCollider Exit;
    private GameObject AudioSource;
	// Use this for initialization
	void Start () {
        Exit = this.GetComponent<BoxCollider>();
        Saving = GameObject.FindGameObjectWithTag("SavingComponents");
        AudioSource = GameObject.FindGameObjectWithTag("Audio Source");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Saving.GetComponent<SaveComponents>().saveState();
            SceneManager.LoadScene(NextLevel);
            DontDestroyOnLoad(Saving);
            DontDestroyOnLoad(AudioSource);


        }
    }
}
