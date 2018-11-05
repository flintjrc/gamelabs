using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
	bool facingRight = true;
	[SerializeField] float maxSpeed = 10f;	
	[SerializeField] float jumpForce= 300f;
	[SerializeField] float fallForce= 10f;

	[SerializeField] bool grounded= false;

	LayerMask WhatisGround;

	[SerializeField] bool fallForceAdded = false;
	Rigidbody playerRigibody;

	// Use this for initialization
	private void Awake () {
		playerRigibody = GetComponent<Rigidbody>();
		playerRigibody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	#if CROSS_PLATFORM_INPUT
        if (CrossPlatformInput.GetButtonDown("Jump")) playerJump();
		float h = CrossPlatformInput.GetAxis("Horizontal");
	#else
		if (Input.GetButtonDown("Jump")) playerJump();
		float h = Input.GetAxis("Horizontal");
	#endif
	playerMove(h);
	}

	void playerJump(){
		playerRigibody.AddForce(new Vector2(0f, jumpForce));
		fallForceAdded = false;
	}


	void playerMove( float move){
			// Move the character
			playerRigibody.velocity = new Vector2(move * maxSpeed, playerRigibody.velocity.y);
			
			// If the input is moving the player right and the player is facing left...
			if(move > 0 && !facingRight)
				// ... flip the player.
				Flip();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if(move < 0 && facingRight)
				// ... flip the player.
				Flip();
			// Make the character falls a bit faster to prevent "floaty" jump
			 if(playerRigibody.velocity.y <0 && !fallForceAdded) playerRigibody.AddForce(new Vector2(0f, -fallForce));
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
