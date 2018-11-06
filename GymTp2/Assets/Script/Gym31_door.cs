using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym31_door : MonoBehaviour {

	// Use this for initialization
	[SerializeField] bool open = false;
	[SerializeField] Vector3 target = new Vector3(1,1,0);
	[SerializeField] Vector3 origin = new Vector3(1,1,0);
	[SerializeField] Vector3 destination;
	Vector3 direction;
	[SerializeField] float openSpeed = 10f;
	[SerializeField] bool moving = false;

	void Start(){
		origin = transform.position;
		target = new Vector3(transform.position.x+target.x,transform.position.y+target.y,transform.position.z+target.z);
		destination = target;
	}
	void Update(){
		if(moving){
			direction =  new Vector3(Mathf.Sign(destination.x - transform.position.x),Mathf.Sign(destination.y - transform.position.y),Mathf.Sign(destination.z - transform.position.z));
			transform.position += new Vector3(Time.deltaTime*openSpeed*direction.x, Time.deltaTime*openSpeed*direction.y, Time.deltaTime*openSpeed*direction.z);

		}
		if(Vector3.Distance(transform.position, destination) <= 0.1){
			transform.position = destination;
			moving = false;
		}

	}

	public void toggleDoor(bool toggle){
		open = toggle;
		if(open){
			destination = target;
		}
		else{
			destination = origin;
		}
		direction =  new Vector3(Mathf.Sign(transform.position.x - destination.x),Mathf.Sign(transform.position.y - destination.y),Mathf.Sign(transform.position.y - destination.y));
		moving = true;
		}
}


