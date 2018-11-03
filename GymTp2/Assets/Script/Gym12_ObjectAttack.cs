using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym12_ObjectAttack : MonoBehaviour {

    //It can be the box collider of a torch, a sword or a camera
    private BoxCollider BoxObject;
    [SerializeField] private string NameOfInputAttack = "Fire1";

    // Use this for initialization
    void Start () {
        BoxObject = GetComponent<BoxCollider>();
        BoxObject.enabled = false;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //We need to use startCoroutine because we must wait 0.40s before disabling the boxCollider of the sword
        StartCoroutine(EnableObject());

    }

    //The function return an IEnumerator because "yield return new WaitForTime" return an IEnumerator
    IEnumerator EnableObject()
    {
        //If we click on the left button of the mouse, we activate the boxCollider of the object
        if (Input.GetButtonDown(NameOfInputAttack))
        {
            BoxObject.enabled = true;

            //We wait 0.40s before disabling the boxCollider of the sword
            yield return new WaitForSeconds(0.4f);

            BoxObject.enabled = false;

        }
    }

}
