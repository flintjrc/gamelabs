using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHold : MonoBehaviour {

    //It can be a torch, a sword or a camera
    [SerializeField] private GameObject Object;
    [SerializeField] private string NameOfInputAttack = "Fire1";

    // Use this for initialization
    void Start () {

        Object.SetActive(false);

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //We need to use startCoroutine because we must wait 0.40s before disabling the sword
        StartCoroutine(EnableObject());

    }

    //The function return an IEnumerator because "yield return new WaitForTime" return an IEnumerator
    IEnumerator EnableObject()
    {
        //If we click on the left button of the mouse, we activate the object
        if (Input.GetButtonDown(NameOfInputAttack))
        {
            Object.SetActive(true);

            //We wait 0.40s before disabling the sword
            yield return new WaitForSeconds(0.4f);

            Object.SetActive(false);
        }
    }

}
