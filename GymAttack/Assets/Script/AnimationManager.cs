using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    [SerializeField] private Animator LeftHandAnimator;
    [SerializeField] private Animator RightHandAnimator;
    [SerializeField] private Animator LeftLegAnimator;
    [SerializeField] private Animator RightLegAnimator;

    [SerializeField] private Rigidbody playerRigibody;

    float HorizontalMove = 0f;
	
	// Update is called once per frame
	void FixedUpdate () {

        //We take the speed on horizontal axis
        HorizontalMove = playerRigibody.velocity.x;

        //This function allows to play the next Animation (LeftMove) controlled by the animator and which is blocked while horizontal speed is equal to 0
        LeftHandAnimator.SetFloat("Speed", Mathf.Abs(HorizontalMove));
        RightHandAnimator.SetFloat("Speed", Mathf.Abs(HorizontalMove));
        RightLegAnimator.SetFloat("Speed", Mathf.Abs(HorizontalMove));
        LeftLegAnimator.SetFloat("Speed", Mathf.Abs(HorizontalMove));

        //Attack animation is enabled when we click on the mouse left button
        LeftHandAnimator.SetBool("Attack", Input.GetButtonDown("Fire1"));
        

    }
}
