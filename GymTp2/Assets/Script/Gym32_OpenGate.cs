using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym32_OpenGate : MonoBehaviour {

    //The Gate
    private Transform GateTransform;

    //The buttons which have the tag called in the ButtonTag component
    private GameObject[] Buttons;

    //The buttons where we have collected the script
    private Gym32_ButtonAction[] GateButtons;

    //Tag Name of the butttons which are associated to this gate
    [SerializeField] private string ButtonTag = "Button";

    //Number Of buttons which are associated to this gate
    [SerializeField] private int NumberGateButtons = 2;

    //Y translation Gate when the gate is opened
    [SerializeField] private float GateTranslationY = 0.5f;

    //X translation Gate when the gate is opened
    [SerializeField] private float GateTranslationX = 2.0f;

    //Number of pushed button
    private int NumberactivatedButton = 0;

    //This component is false when the door is closed
    private bool OpenedDoor = false;

	// Use this for initialization
	void Start () {

        GateTransform = GetComponent<Transform>();

        Buttons = GameObject.FindGameObjectsWithTag(ButtonTag);

        GateButtons = new Gym32_ButtonAction[Buttons.Length];

        //We collect the script of buttons
        if (GateButtons.Length == NumberGateButtons)
        {
            for (int i = 0; i < NumberGateButtons; ++i)
            {
                GateButtons[i] = Buttons[i].GetComponent<Gym32_ButtonAction>();
            }
            
        }

        else
        {
            Debug.Log("Error with lenght because of the name of button tags or because there are not tags for the buttons");
        }

	}
	
	// Update is called once per frame
	void Update () {

        OpenDoor();

    }

    //Detect if the gate must be opened or not
    private void OpenDoor()
    {
        if (GateButtons.Length == NumberGateButtons)
        {
            NumberactivatedButton = 0;

            //If we have a button pushed, then we increment the value of the number of pushed buttons
            for (int i = 0; i < NumberGateButtons; ++i)
            {
                if (GateButtons[i].getActiveState() == true)
                {
                    NumberactivatedButton += 1;
                }
                
            }
        }

        // If we have the same number of pushed button and detected buttons, then we open the gate 
        if (NumberactivatedButton == NumberGateButtons && !OpenedDoor)
        {
            //We translate the gate
            GateTransform.Translate(0.0f, GateTranslationY, 0.0f);
            GateTransform.Translate(GateTranslationX, 0.0f, 0.0f);
            OpenedDoor = true;
        }


    }





}
