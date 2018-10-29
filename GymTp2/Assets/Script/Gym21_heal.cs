using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym21_heal : MonoBehaviour {

[SerializeField] int heal = 1;
Gym21_Player player;
void Start(){
	player = GameObject.FindGameObjectWithTag("Player").GetComponent<Gym21_Player>();

}
void OnCollisionEnter(Collision col){
	if(col.collider.CompareTag("Player")){
		player.heal(heal);

	}
	
}
}
