using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWithBlock : MonoBehaviour {

    // Button Collider
    private BoxCollider ButtonCollider;

    //Button Object
    private Transform Button;

    //Gap in Y axis between the high of the activated button and unactivated button
    private float PushedButtonGapY = -0.1f;

    //this component is true when the button is activated
    [SerializeField] bool isActivated;

    //Gate associated with the button
    [SerializeField] private GameObject Door;

    //This parameter represents the number of detected collider which are in the same time on the button (works only for the player and the block)
    [SerializeField] int NbCollision;

    // Use this for initialization
    void Start()
    {

        Button = GetComponent<Transform>();
        ButtonCollider = GetComponent<BoxCollider>();

        //We activate the button collider
        ButtonCollider.enabled = true;
        isActivated = false;
        NbCollision = 0;

    }

    private void Update()
    {
        //If we have one object on the button and if the button is disabled, then we open the door
        if (NbCollision == 1 && !isActivated)
        {
            OpenDoor();
        }

        //If we have two object on the button and if the button is disabled, then we open the door
        if(NbCollision == 2 && !isActivated)
        {
            OpenDoor();
        }

        //If we doesn't have any object on the button and if we have a button enabled, then we close the door
        if(NbCollision == 0 && isActivated)
        {
            CloseDoor();
        }

        //If we exceed the number of possible detected objects on the button

        if(NbCollision < 0)
        {
            NbCollision = 0;
        }

        if(NbCollision > 2)
        {
            NbCollision = 2;
        }
        
    }

    //This function manages collision between button, block and player when the player or the block enters in the collision zone of the button
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cube") || other.CompareTag("Player") && !isActivated)
        {
            NbCollision++;
        }

    }

    //This function manages collision between button, block and player when the player or the block leaves the collision zone of the button
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("cube") || other.CompareTag("Player") && isActivated)
        {
            NbCollision--;

        }
    }

    public void OpenDoor()
    {
        //The button is pushed
        Button.Translate(0.0f, PushedButtonGapY, 0.0f);

        //The gate is going to open
        Door.GetComponent<Animator>().Play("DoorOpening");

        //The button is activated
        isActivated = true;
    }

    public void CloseDoor()
    {
        //The button is pushed
        Button.Translate(0.0f, -PushedButtonGapY, 0.0f);

        //The gate is going to close, we get the time of the current animation to play the animation of closing in the current state of the door.
        float NormTime = Door.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
        float TotalTime = Door.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        float Time = NormTime * TotalTime;

        //If the time exceeds the time of the clip, we put this time at the maximum value of the time for the clip.
        if (Time > TotalTime) Time = TotalTime;

        //We start the animation at the specific time defined above.
        Door.GetComponent<Animator>().Play("DoorClosing", 0, TotalTime - Time);

        //The button is disabled
        isActivated = false;
    }

}
