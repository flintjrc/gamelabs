using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tikiBehaviour : MonoBehaviour {


    //Ennnemy Health
    [SerializeField] private int EnnemyHealth = 3;

    //Number of damages taken by ennemy
    [SerializeField] private int DamageReceived = 1;

    //Sprite which become red when the ennemy is invincible after an attack
    private SpriteRenderer[] enemySpritesInvincible;

    //Color of the invincible state
    private Color InvincibleColor = Color.red;

    //Default color for a sprite
    private Color NormalColor = Color.white;

    //True if the ennemy can be hurt
    private bool NotInvincible = true;

    Rigidbody enemyRigidBody;
	[SerializeField] Vector3 forcePush;
	[SerializeField] float invincibleTime = 0.75f;

	[SerializeField] Vector3 anchorPoint;
	private enum ZoneType {Square, Circle};
	[SerializeField] ZoneType zoneType;
	[SerializeField] Vector2 squareSize;
	[SerializeField] float circleRadius;
	[SerializeField] Vector3 playerPosition;
	[SerializeField] bool chasing;
	[SerializeField] private float speed = 2.0f;
	[SerializeField] bool startPositionAsAnchor;
	[SerializeField] Vector3 startPosition;
	[SerializeField] int damage = 1;

	[SerializeField] LayerMask whatIsGround;            // A mask determining what is ground to the character
	[SerializeField] bool grounded;


    void Start()
    {
		startPosition = transform.position;
		if(startPositionAsAnchor) anchorPoint = transform.position;
		playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        enemyRigidBody = GetComponent<Rigidbody>();
        enemySpritesInvincible = gameObject.GetComponentsInChildren<SpriteRenderer>();

        //Freeze positions and rotations which are unused
        enemyRigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }


    //Function which is called when the ennemy is attacked
    void DeductPoints(int DamageAmount)
    {
        EnnemyHealth -= DamageAmount;
    }

    //This function is called when an object triggers the box collider of an ennemy. It doesn't work if the object has a collider which is not a trigger.
    //Then, Torch, camera rays and sword must be a trigger as collider.
    //Moreover, the ennemy must have a collider which is not a trigger, otherwise, the hero can just trigger the ennemy to kill him.
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().CompareTag("sword") && NotInvincible)
        {
            DeductPoints(DamageReceived);
			enemyRigidBody.velocity= Vector3.zero;
			float sign = Mathf.Sign(transform.position.x - collision.GetComponent<Collider>().transform.position.x);
			enemyRigidBody.AddForce(new Vector3(sign*forcePush.x, forcePush.y, 0));
            //We need to use startCoroutine to call the function "isInvicible" because we need to measure the time of the invincible state
            StartCoroutine(isInvicible());
        }
        
    }

	    private void OnCollisionEnter(Collision collision)
    {
        //If we have the same mask between the ground and the collider, then we are grounded.
        if ((whatIsGround.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            grounded = true;
		}

		else if (collision.collider.CompareTag("Player")){
			collision.collider.GetComponent<playerBehaviour>().takeDamage(damage);
		}

    }

		private void OnCollisionExit(Collision collision)
    {
        //If we have the same mask between the ground and the collider, then we are grounded.
        if ((whatIsGround.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            grounded = false;
        }


    }

    void Update () {
			playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
		if(zoneType == ZoneType.Square && Mathf.Abs(playerPosition.x-anchorPoint.x)<(squareSize.x/2) && Mathf.Abs(playerPosition.y-anchorPoint.y)<(squareSize.y/2)){
			chasing =true;
		}
		else if(zoneType == ZoneType.Circle && Vector3.Distance(playerPosition, anchorPoint)<circleRadius){
			chasing = true;
		}
		else chasing =false;
		if (NotInvincible && grounded) tikiMove();
        //Ennemy is killed when he doesn't have hit points anymore
		if (EnnemyHealth <= 0)
        {
            Destroy(gameObject);
        }

	}


	void tikiMove(){
		if(chasing){
			enemyRigidBody.velocity = speed*Vector3.Normalize(new Vector3(playerPosition.x - transform.position.x, enemyRigidBody.velocity.y, 0.0f));
			if(transform.localScale.x * (playerPosition.x - transform.position.x) >0){
				Flip();
			}
		}
		else if(Vector3.Distance(startPosition,gameObject.transform.position)>0.1){
			enemyRigidBody.velocity = speed*Vector3.Normalize(new Vector3(startPosition.x - transform.position.x, enemyRigidBody.velocity.y, 0.0f));
			if(transform.localScale.x * (startPosition.x - transform.position.x) >0){
				Flip();
			}
		}
		else enemyRigidBody.velocity = Vector3.zero;
	}

		void Flip()
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    //Function reflecting the time when the ennemy is invincible (0.75 seconds)
    //This function return an IEnumerator because "yield return new WaitForTime" return an IEnumerator
    IEnumerator isInvicible()
    {
        //Ennemy is invincible, then we put the value at true
        NotInvincible = false;

        foreach (SpriteRenderer SpriteEnemy in enemySpritesInvincible)
        {
            //The sprite of the ennemy become red
            SpriteEnemy.color = InvincibleColor;
        }

        //We wait for 0.75s
        yield return new WaitForSeconds(invincibleTime);

        //Ennemy is open to attack
        NotInvincible = true;

        //The sprite of ennemy become normal
        foreach (SpriteRenderer SpriteEnemy in enemySpritesInvincible)
        {
            //The sprite of the ennemy become red
            SpriteEnemy.color = NormalColor;
        }
    }

}
