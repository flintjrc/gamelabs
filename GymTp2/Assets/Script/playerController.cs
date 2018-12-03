using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class playerController : MonoBehaviour {
	bool facingRight = true;
    [SerializeField] float maxSpeed = 10f;              // The fastest the player can travel in the x axis.
    [SerializeField] float minAirJumpForce = 400f;      // The minimum force used for regular jumps
    [SerializeField] float jumpForce = 400f;                   // Current Amount of force added when the player jumps.	
    [SerializeField] float maxJumpForce = 1000f;        // The max amount of force when doing  a charged jump

    [SerializeField] bool grounded= false;

    [SerializeField] bool charged = false;              //Check if we use a charged jump
    [SerializeField] LayerMask whatIsGround;            // A mask determining what is ground to the character

    [SerializeField] int maxJump = 2;                   //Reference to the player's max number of air jumps
    [SerializeField] int nAirJump;						//Number of air jumps left

    [SerializeField] bool fallForceAdded = false;
	Rigidbody playerRigibody;
    [SerializeField] private float fallForce = 10f;    //Force added when Ringo fall

    [SerializeField] playerBehaviour behaviour;
    [SerializeField] Inventory inventory;

    [SerializeField] private bool shoesTaken;

    string currentScene;

	// Use this for initialization
	private void Awake () {

        currentScene = SceneManager.GetActiveScene().name.ToString();

        if (currentScene == "Level_2" || currentScene == "Level_3")
        {
            shoesTaken = true;
        }
        else
        {
            shoesTaken = false;
        }
		playerRigibody = GetComponent<Rigidbody>();
        behaviour = GetComponent<playerBehaviour>();
        inventory = GetComponent<Inventory>();
		playerRigibody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
	}


    // Update is called once per frame
    void Update () {

#if CROSS_PLATFORM_INPUT
        if (CrossPlatformInput.GetButtonDown("Jump")) playerJump();
		float h = CrossPlatformInput.GetAxis("Horizontal");
#else
        if (Input.GetButtonDown("Jump"))
        {
            playerJump();
        }
		float h = Input.GetAxis("Horizontal");

#endif
        charged = Input.GetKey(KeyCode.LeftControl);
        if(Input.GetButtonDown("Fire1") && inventory.menuItems.Count != 0 && behaviour.currentItem != 0) behaviour.useItem();
        if(Input.GetAxis("Mouse ScrollWheel")!=0 && inventory.menuItems.Count != 0) behaviour.switchItem((int) Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));
        playerMove(h);
	}

	void playerJump()
    {

        //Try to perform a regular jump.
		if ((nAirJump > 0 || grounded) && !charged)
        {
            // If we're in the air, we first reset the vertical velocity before adding a new force, so that each jump feels consistent
            if (!grounded) {
                playerRigibody.velocity = new Vector2(playerRigibody.velocity.x, 0);
            }

            // Add a vertical force to the player.
            playerRigibody.AddForce(new Vector2(0f, jumpForce));
            nAirJump = nAirJump - 1;
            jumpForce = minAirJumpForce;
            grounded = false;
        }


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

            //If the player touches the ground for a certain time, reset the number of available jumps
            if (grounded && !(nAirJump == maxJump))
            {
                nAirJump = maxJump;
                jumpForce = minAirJumpForce;
            }

           //If the player press jump, then accumulate energy
            if (Input.GetButton("Jump") && jumpForce < maxJumpForce && charged && grounded && shoesTaken)
            {
                jumpForce += 10.0f;
            }

            //If the player has accumulated energy and release jump or crouch button, then jump
            if (jumpForce > minAirJumpForce && (!Input.GetKey(KeyCode.LeftControl) || !Input.GetButton("Jump")))
            {
                playerRigibody.AddForce(new Vector2(0f, jumpForce));
                nAirJump = nAirJump - 1;
                jumpForce = minAirJumpForce;
                grounded = false;
            }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If we have the same mask between the ground and the collider, then we are grounded.
        if ((whatIsGround.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            grounded = true;
        }


    }

    public bool GetGrounded()
    {
        return grounded;
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

    public void TakeShoes()
    {
        shoesTaken = true;
    }
}
