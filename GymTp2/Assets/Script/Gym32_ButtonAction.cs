using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym32_ButtonAction : MonoBehaviour {

    // Button Collider
    private BoxCollider ButtonCollider;

    //Button Object
    private Transform Button;

    //Gap in Y axis between the high of the activated button and unactivated button
    [SerializeField] private float PushedButtonGapY = -0.1f;

    //this component is true when the button is activated
    bool isActivated;

	// Use this for initialization
	void Start () {

        Button = GetComponent<Transform>();
        ButtonCollider = GetComponent<BoxCollider>();

        //We activate the button collider
        ButtonCollider.enabled = true;
        isActivated = false;
		
	}

    //This function manages collision between button and player when the player enters in the collision zone of the button
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //We disable the button collider
            ButtonCollider.enabled = false;

            //The button is pushed
            Button.Translate(0.0f, PushedButtonGapY, 0.0f);

            //The button is activated
            isActivated = true;
        }
    }

    public bool getActiveState()
    {
        return isActivated;
    }
}
