using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour 
{
	bool facingRight = true;							// For determining which way the player is currently facing.

	[SerializeField] float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
	[SerializeField] float minJumpForce= 800f;
	[SerializeField] float jumpForce;			// Amount of force added when the player jumps.	
	[SerializeField] float maxJumpForce = 2000f;

	[Range(0, 1)]
	[SerializeField] float crouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	
	[SerializeField] bool airControl = false;			// Whether or not a player can steer while jumping;
	[SerializeField] LayerMask whatIsGround;			// A mask determining what is ground to the character
	
	Transform groundCheck;								// A position marking where to check if the player is grounded.
	float groundedRadius = .2f;							// Radius of the overlap circle to determine if grounded
	public bool grounded = false;								// Whether or not the player is grounded.
	Transform ceilingCheck;								// A position marking where to check for ceilings
	float ceilingRadius = .01f;							// Radius of the overlap circle to determine if the player can stand up

	Transform wallCheck;
	float wallRadius = .5f;
	bool walled = false;
	[SerializeField] float wallJumpForce = 1000f;
	Animator anim;										// Reference to the player's animator component.
	[SerializeField] int maxjump = 2; 					//Reference to the player's max number of air jumps
	int njump;											//number of jumps left
    void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		wallCheck = transform.Find("WallCheck");
		anim = GetComponent<Animator>();
	}


	void FixedUpdate()
	{
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		anim.SetBool("Ground", grounded);

		// Set the vertical animation
		anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
	}


	public void Move(float move, bool crouch, bool jump)
	{


		// If crouching, check to see if the character can stand up
		if(!crouch && anim.GetBool("Crouch"))
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if( Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
				crouch = true;
		}

		// Set whether or not the character is crouching in the animator
		anim.SetBool("Crouch", crouch);

		//only control the player if grounded or airControl is turned on
		if(grounded || airControl)
		{
			// Reduce the speed if crouching by the crouchSpeed multiplier
			move = (crouch ? move * crouchSpeed : move);

			// The Speed animator parameter is set to the absolute value of the horizontal input.
			anim.SetFloat("Speed", Mathf.Abs(move));

			// Move the character
			GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
			
			// If the input is moving the player right and the player is facing left...
			if(move > 0 && !facingRight)
				// ... flip the player.
				Flip();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if(move < 0 && facingRight)
				// ... flip the player.
				Flip();
		}

        // If the player should jump...

		//If player is against a wall in the air, perform a walljump.
		if(jump && Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIsGround) && !grounded)
		{
			int sign = 1;
			if (facingRight) sign = -1;
			GetComponent<Rigidbody2D>().velocity= new Vector2(0,0);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(wallJumpForce, jumpForce));
		}

		//else, try to perform a regular jump.
		else if (njump>1 && jump && !crouch) {
            
            anim.SetBool("Ground", false);

			// If we're in the air, we first reset the vertical velocity before adding a new force, so that each jump feels consistent
			if(!grounded){GetComponent<Rigidbody2D>().velocity= new Vector2(GetComponent<Rigidbody2D>().velocity.x,0);};
			// Add a vertical force to the player.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
			njump=njump-1;
			jumpForce = minJumpForce;
        }

		//If the player touches the ground, reset the number of available jumps
		if(grounded && !crouch)
		{
			njump=maxjump;
			jumpForce = minJumpForce;
		}

		//If the player crouches and press jump, then accumulate energy
		if(crouch && Input.GetButton("Jump") && jumpForce<maxJumpForce){
			jumpForce += 10.0f;
		}

		//If the player has accumulated energy and release jump or crouch button, then jump
		if(jumpForce>minJumpForce && (!Input.GetKey(KeyCode.LeftControl) || !Input.GetButton("Jump"))){
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
			njump=njump-1;
			jumpForce = minJumpForce;
		}

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

	public bool getGrounded(){
		return grounded;
	}
}
