using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym22_flask : MonoBehaviour {

[SerializeField] float fuel = 20f;
Gym22_torchLife player;

void Start(){
	player = GameObject.FindGameObjectWithTag("Player").GetComponent<Gym22_torchLife>();

}
void OnTriggerEnter(Collider collider){
	if(collider.CompareTag("Player")){
		player.addFuel(fuel);
		gameObject.SetActive(false);
	}
	
}
}
