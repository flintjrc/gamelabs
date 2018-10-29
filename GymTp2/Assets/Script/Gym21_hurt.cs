using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym21_hurt : MonoBehaviour {

[SerializeField] int damage = 1;
Gym21_Player player;
void Start(){
	player = GameObject.FindGameObjectWithTag("Player").GetComponent<Gym21_Player>();

}
void OnCollisionStay(Collision col){
	if(col.collider.CompareTag("Player")){
		player.takeDamage(damage);

	}
	
}

}
