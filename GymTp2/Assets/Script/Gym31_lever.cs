using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym31_lever : MonoBehaviour {

[SerializeField] bool pulled = false;
[SerializeField] float timeToReset = 10f;
Gym31_door door;

void Start(){
	door = GameObject.Find("Door").GetComponent<Gym31_door>();

}
void OnTriggerStay(Collider collider){
	if(collider.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !pulled){
		toggleLever();
		StartCoroutine(timedLever());
	}
	
}

void toggleLever(){
	pulled = !pulled;
	door.toggleDoor(pulled);
	Vector3 theScale = transform.localScale;
	theScale.x *= -1;
	transform.localScale = theScale;

}

IEnumerator timedLever(){
	yield return new WaitForSeconds(timeToReset);
	toggleLever();
}
}
