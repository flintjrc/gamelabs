using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym21_Player : MonoBehaviour {

	[SerializeField] private int hitpoints = 3;
	[SerializeField] private int maxHealth = 3;
	[SerializeField] private bool immunity = false;
	[SerializeField] private float immuneTime = 2.0f;
	[SerializeField] public UnityEngine.UI.Slider healthbar;
	
	private float timer = 2.0f;
	SpriteRenderer[] lChildRenderers;


	void Start(){
		lChildRenderers=gameObject.GetComponentsInChildren<SpriteRenderer>();
		healthbar.value = ((float)hitpoints/(float)maxHealth) * 100;
	}
	public int getHP(){
		return hitpoints;
	}

	public void takeDamage(int damage){
		
		if(!immunity) {
			hitpoints = Mathf.Max(hitpoints-damage, 0);
			timer = immuneTime;
			immunity = true;
			healthbar.value = ((float)hitpoints/(float)maxHealth) * 100;
		};
		if (hitpoints == 0) UnityEngine.SceneManagement.SceneManager.LoadScene("Gym_21");
	}

	public void heal(int healed){
		hitpoints = Mathf.Min(hitpoints+healed, maxHealth);
		healthbar.value = ((float)hitpoints/(float)maxHealth) * 100;
	}

	void Update(){

		if(immunity){
			timer -= Time.deltaTime;
			bool blink = Time.time % 0.2 <= 0.1f;
         	foreach ( SpriteRenderer lRenderer in lChildRenderers) {lRenderer.enabled=blink;}
			if (timer<0){
				immunity = false;
				foreach ( SpriteRenderer lRenderer in lChildRenderers){lRenderer.enabled=true;}
			}
		}
	}
}
