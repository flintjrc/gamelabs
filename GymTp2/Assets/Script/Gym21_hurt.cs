using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym21_hurt : MonoBehaviour {

[SerializeField] int damage = 1;
playerBehaviour player;
void Start(){
	player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehaviour>();

}
void OnCollisionStay(Collision col){
	if(col.collider.CompareTag("Player")){
		player.takeDamage(damage);

	}
	
}

}
