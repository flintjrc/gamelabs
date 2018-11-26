using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym22_flask : MonoBehaviour {

[SerializeField] float fuel = 20f;
playerBehaviour player;

void Start(){
	player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehaviour>();

}
void OnTriggerEnter(Collider collider){
	if(collider.CompareTag("Player")){
		player.addFuel(fuel);
		gameObject.SetActive(false);
	}
	
}
}
