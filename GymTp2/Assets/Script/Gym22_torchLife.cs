using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym22_torchLife : MonoBehaviour {

	[SerializeField] float torchMaxLifetime = 30f;
	[SerializeField] float torchFuel;
	[SerializeField] float burnSpeed = 1.0f;
	[SerializeField] public UnityEngine.UI.Slider fuelBar;
	bool torchIsLit = true;
	SpriteRenderer[] lChildRenderers;

	void Start(){
		lChildRenderers=gameObject.GetComponentsInChildren<SpriteRenderer>();
		fuelBar.value = ((torchFuel/torchMaxLifetime) * 100);
	}

	void lightTorch(bool instruction){
        foreach ( SpriteRenderer lRenderer in lChildRenderers) {
			if(lRenderer.gameObject.name =="Torch"){ lRenderer.enabled= instruction;}
		}
	}

	void Update(){

		if(Input.GetAxis("Mouse ScrollWheel")!=0 && torchFuel>0){
			torchIsLit = !torchIsLit;
			lightTorch(torchIsLit);
		}

		if(torchIsLit){
			torchFuel = Mathf.Min(torchFuel, torchMaxLifetime);
			torchFuel -= burnSpeed*Time.deltaTime;
			fuelBar.value = ((torchFuel/torchMaxLifetime) * 100);
		}
		if(torchFuel<=0){
			lightTorch(false);
			torchIsLit = false;
		}
	}

	public void addFuel(float fuelAdded){
		torchFuel=Mathf.Min(torchFuel+fuelAdded, torchMaxLifetime);
		fuelBar.value = ((torchFuel/torchMaxLifetime) * 100);
	}
}
