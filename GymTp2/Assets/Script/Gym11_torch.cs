using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym11_torch : MonoBehaviour {
	bool torchIsLit = true;
	SpriteRenderer[] lChildRenderers;

	void Start(){
		lChildRenderers=gameObject.GetComponentsInChildren<SpriteRenderer>();
	}
	void Update () {
		if(Input.GetAxis("Mouse ScrollWheel")!=0){
			torchIsLit = !torchIsLit;
			lightTorch(torchIsLit);
		}
	}

	void lightTorch(bool instruction){
        foreach ( SpriteRenderer lRenderer in lChildRenderers) {
			if(lRenderer.gameObject.name =="cartoon_eyes"){ lRenderer.enabled= !instruction;}
			else{lRenderer.enabled=instruction;}
			gameObject.GetComponentInChildren<Light>().enabled = instruction;
		}
	}
}
