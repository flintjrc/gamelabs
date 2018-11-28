using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostBehaviour : MonoBehaviour {

	[SerializeField] Vector3 anchorPoint;
	private enum ZoneType {Square, Circle};
	[SerializeField] ZoneType zoneType;
	[SerializeField] Vector2 squareSize;
	[SerializeField] float circleRadius;
	[SerializeField] Vector3 playerPosition;
	[SerializeField] bool chasing;
	[SerializeField] private float speed = 2.0f;
	private Rigidbody GhostBody;
    public bool isFlashed = false;
    private Color FlashedColor = Color.blue;
    private Color NormalColor = Color.white;
    private SpriteRenderer[] GhostSprite;
    [SerializeField] private float FreezeTime = 3.0f;
	[SerializeField] int damage = 1;

	void Start () {
		anchorPoint = transform.position;
		playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
		GhostBody = GetComponent<Rigidbody>();

        GhostSprite = gameObject.GetComponentsInChildren<SpriteRenderer>();

        //Freeze positions and rotations which are unused
        GhostBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		
	}
	
	// Update is called once per frame
	void Update () {
		playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
		if(zoneType == ZoneType.Square && Mathf.Abs(playerPosition.x-anchorPoint.x)<(squareSize.x/2) && Mathf.Abs(playerPosition.y-anchorPoint.y)<(squareSize.y/2)){
			chasing =true;
		}
		else if(zoneType == ZoneType.Circle && Vector3.Distance(playerPosition, anchorPoint)<circleRadius){
			chasing = true;
		}
		else chasing =false;
		if(!isFlashed) ghostMove();
		else GhostBody.velocity = Vector3.zero;
		
	}


	void ghostMove(){
		if(chasing){
			GhostBody.velocity = speed*Vector3.Normalize(playerPosition - gameObject.transform.position);
			if(transform.localScale.x * (playerPosition.x - transform.position.x) <0){
				Flip();
			}
		}
		else if(Vector3.Distance(anchorPoint,gameObject.transform.position)>0.1){
			GhostBody.velocity = speed*Vector3.Normalize(anchorPoint - gameObject.transform.position);
			if(transform.localScale.x * (anchorPoint.x - transform.position.x) <0){
				Flip();
			}
		}
		else GhostBody.velocity = Vector3.zero;
	}
	void Flip()
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

	private void OnTriggerEnter(Collider collision)
    {
        if (!isFlashed && collision.GetComponent<Collider>().CompareTag("cameraFlash"))
        {

            //We need to use startCoroutine to call the function "FlashGhost" because we need to measure the time of the immobilisation state
            StartCoroutine(FlashGhost());
        }

		else if (!isFlashed && collision.GetComponent<Collider>().CompareTag("Player")){
			collision.GetComponent<Collider>().GetComponent<playerBehaviour>().takeDamage(damage);
		}
    }

	    IEnumerator FlashGhost()
    {
        //Ghost is immobilized, then we put the value at false
        isFlashed = true;

        foreach (SpriteRenderer SpriteParts in GhostSprite)
        {
            //The sprite of the ennemy become red
            SpriteParts.color = FlashedColor;
        }

        //We wait for [T] seconds
        yield return new WaitForSeconds(FreezeTime);

        //Ennemy is open to attack
        isFlashed = false;

        //The sprite of ennemy become normal
        foreach (SpriteRenderer SpriteParts in GhostSprite)
        {
            //The sprite of the ennemy become red
            SpriteParts.color = NormalColor;
        }
    }
}
