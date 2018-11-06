using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym31_lever : MonoBehaviour {

[SerializeField] bool pulled = false;
Gym31_door door;

void Start(){
	door = GameObject.Find("Door").GetComponent<Gym31_door>();

}
void OnTriggerStay(Collider collider){
	if(collider.CompareTag("Player") && Input.GetKeyDown(KeyCode.E)){
		toggleLever();
	}
	
}

void toggleLever(){
	pulled = !pulled;
	door.toggleDoor(pulled);
	Vector3 theScale = transform.localScale;
	theScale.x *= -1;
	transform.localScale = theScale;

}

}
