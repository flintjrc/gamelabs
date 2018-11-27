using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour {

	//constants for determining which item is active
	[SerializeField] int currentItem;

	[SerializeField] Inventory inventory;

	SpriteRenderer[] lChildRenderers;
	MeshRenderer swordRenderer;

	//attribs for camera attack
    [SerializeField] private CapsuleCollider FlashCollider;
    [SerializeField] private Light SpotLight;
    [SerializeField] private Light PositionLight;

	//attribs for sword attack
	[SerializeField] private BoxCollider BoxObject;

	//attribs for torch
	[SerializeField] float torchMaxLifetime = 30f;
	[SerializeField] float torchFuel;
	[SerializeField] float burnSpeed = 1.0f;
	[SerializeField] public UnityEngine.UI.Slider fuelBar;
	[SerializeField] Light torchLight;

	//attribs for health
	[SerializeField] private int hitpoints = 3;
	[SerializeField] private int maxHealth = 3;
	[SerializeField] private bool immunity = false;
	[SerializeField] private float immuneTime = 2.0f;
	private float immunityTimer = 2.0f;
	[SerializeField] public UnityEngine.UI.Slider healthbar;

	//attribs for cube moving
	public KeyCode grabInput;
    public KeyCode releaseInput;
	bool isAttached = false;
    Collider attachedCollider = null;

	

	// Use this for initialization
	void Start () {
		currentItem = 0;
		lChildRenderers=gameObject.GetComponentsInChildren<SpriteRenderer>();
		inventory = gameObject.GetComponent<Inventory>();
		foreach ( MeshRenderer lRenderer in gameObject.GetComponentsInChildren<MeshRenderer>()){
				if(lRenderer.gameObject.name =="Sword") swordRenderer= lRenderer;
		}
		BoxObject.enabled = false;
		FlashCollider.enabled = false;
        SpotLight.enabled = false;
        PositionLight.enabled = false;
		healthbar.value = ((float)hitpoints/(float)maxHealth) * 100;
		fuelBar.value = ((torchFuel/torchMaxLifetime) * 100);
		foreach ( Light light in gameObject.GetComponentsInChildren<Light>()){
				if(light.gameObject.name =="Torch Light") torchLight= light;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (isAttached && Input.GetKeyDown(releaseInput))
        {            
            attachedCollider.GetComponent<FixedJoint>().connectedBody = null;
            Destroy(attachedCollider.GetComponent<FixedJoint>());
            attachedCollider = null;
            isAttached = false;
        }

		if(immunity){
			immunityTimer -= Time.deltaTime;
			bool blink = Time.time % 0.2 <= 0.1f;
         	foreach ( SpriteRenderer lRenderer in lChildRenderers) {
				 if(lRenderer.gameObject.name !="Torch" && lRenderer.gameObject.name != "Picture Camera"){
					 lRenderer.enabled=blink;
					 }
				 }
			if (immunityTimer<0){
				immunity = false;
				foreach ( SpriteRenderer lRenderer in lChildRenderers) {
				 if(lRenderer.gameObject.name !="Torch" && lRenderer.gameObject.name != "Picture Camera"){
					 lRenderer.enabled=true;
					 }
				 }
			}
		}

		if(inventory.menuItems.Count != 0)
		{	if(inventory.menuItems[currentItem].Name=="Torch"){
				torchFuel = Mathf.Min(torchFuel, torchMaxLifetime);
				torchFuel -= burnSpeed*Time.deltaTime;
				fuelBar.value = ((torchFuel/torchMaxLifetime) * 100);
			}
		}
		if(torchFuel<=0){
			switchItem(1);
		}
		
	}

	public void useItem(){
		switch(inventory.menuItems[currentItem].Name){
			case "Camera":
				StartCoroutine(cameraAttack());
				break;
			case "Sword":
				StartCoroutine(swordAttack());
				break;
			default:
				break;
		}

	}

	public void switchItem(int sign){
		
		currentItem += sign;
		currentItem %= inventory.menuItems.Count;
		if(currentItem<0) currentItem = inventory.menuItems.Count-1;
		foreach ( SpriteRenderer lRenderer in lChildRenderers) {
			if(lRenderer.gameObject.name =="Torch" || lRenderer.gameObject.name == "Picture Camera"){ lRenderer.enabled= false;}
		}
		swordRenderer.enabled = false;
		torchLight.enabled= false;
		switch(inventory.menuItems[currentItem].Name){
			case "Torch":
				foreach ( SpriteRenderer lRenderer in lChildRenderers) {
					if(lRenderer.gameObject.name == "Torch"){lRenderer.enabled = true;}
				}
				torchLight.enabled = true;
				break;
			case "Camera":
				foreach ( SpriteRenderer lRenderer in lChildRenderers) {
					if(lRenderer.gameObject.name == "Picture Camera"){lRenderer.enabled = true;}
				}
				break;
			case "Sword":
				swordRenderer.enabled = true;
				break;
			default:
				break;
		}
	}

	public int getHP(){
		return hitpoints;
	}

//TODO: Changer le chargement de la scène pour la scène actuelle 
	public void takeDamage(int damage){
		
		if(!immunity) {
			hitpoints = Mathf.Max(hitpoints-damage, 0);
			immunityTimer = immuneTime;
			immunity = true;
			healthbar.value = ((float)hitpoints/(float)maxHealth) * 100;
		};
		if (hitpoints == 0) UnityEngine.SceneManagement.SceneManager.LoadScene("MegaGym");
	}

	public void heal(int healed){
		hitpoints = Mathf.Min(hitpoints+healed, maxHealth);
		healthbar.value = ((float)hitpoints/(float)maxHealth) * 100;
	}

	IEnumerator swordAttack()
    {

            BoxObject.enabled = true;

            //We wait 0.40s before disabling the boxCollider of the sword
            yield return new WaitForSeconds(0.4f);

            BoxObject.enabled = false;
    }

	IEnumerator cameraAttack()
    {
            FlashCollider.enabled = true;
            SpotLight.enabled = true;
            PositionLight.enabled = true;

            //We wait 0.40s before disabling the boxCollider of the sword
            yield return new WaitForSeconds(0.5f);

            FlashCollider.enabled = false;
            SpotLight.enabled = false;
            PositionLight.enabled = false;

    }

	//for push and block
	private void OnCollisionStay(Collision collision)
    {
        if (!isAttached && collision.collider.gameObject.tag == "cube" && Input.GetKey(grabInput))
        {
            attachedCollider = collision.collider;
            FixedJoint joint = collision.collider.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = GetComponent<Rigidbody>();
            isAttached = true;
        }
    }

	

	public void addFuel(float fuelAdded){
		torchFuel=Mathf.Min(torchFuel+fuelAdded, torchMaxLifetime);
		fuelBar.value = ((torchFuel/torchMaxLifetime) * 100);
	}


	
}
