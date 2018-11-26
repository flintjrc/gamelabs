using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour {

	//constants for determining which item is active
	const int TORCH =1;
	const int CAMERA =2;
	const int SWORD =3;
	[SerializeField] int currentItem;

	[SerializeField] Inventory inventory;

	SpriteRenderer[] lChildRenderers;
	MeshRenderer swordRenderer;

	//attribs for camera attack
    [SerializeField] private CapsuleCollider FlashCollider;
    [SerializeField] private Light SpotLight;
    [SerializeField] private Light PositionLight;
    [SerializeField] private string NameOfInputAttack = "Fire1";

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
         	foreach ( SpriteRenderer lRenderer in lChildRenderers) {lRenderer.enabled=blink;}
			if (immunityTimer<0){
				immunity = false;
				foreach ( SpriteRenderer lRenderer in lChildRenderers){lRenderer.enabled=true;}
			}
		}

		if(currentItem==TORCH){
			torchFuel = Mathf.Min(torchFuel, torchMaxLifetime);
			torchFuel -= burnSpeed*Time.deltaTime;
			fuelBar.value = ((torchFuel/torchMaxLifetime) * 100);
		}
		if(torchFuel<=0){
			switchItem(1);
		}
		
	}

	public void useItem(){
		switch(currentItem){
			case CAMERA:
				StartCoroutine(cameraAttack());
				break;
			case SWORD:
				StartCoroutine(swordAttack());
				break;
			default:
				break;
		}

	}

	public void switchItem(int sign){
		
		currentItem += sign;
		currentItem %= 4;
		if(currentItem<0) currentItem = 3;
		switch(currentItem){
			case TORCH:
				if(inventory.HasItem("Torch") && torchFuel>0){
				foreach ( SpriteRenderer lRenderer in lChildRenderers) {
					if(lRenderer.gameObject.name =="Picture Camera"){ lRenderer.enabled= false;}
					else if(lRenderer.gameObject.name == "Torch"){lRenderer.enabled = true;}
				}
				swordRenderer.enabled = false;
				}
				else switchItem(sign);
				break;
			case CAMERA:
				if(inventory.HasItem("Camera")){
				foreach ( SpriteRenderer lRenderer in lChildRenderers) {
					if(lRenderer.gameObject.name =="Torch"){ lRenderer.enabled= false;}
					else if(lRenderer.gameObject.name == "Picture Camera"){lRenderer.enabled = true;}
				}
				swordRenderer.enabled = false;
				}
				else switchItem(sign);
				break;
			case SWORD:
				if(inventory.HasItem("Sword")){
				foreach ( SpriteRenderer lRenderer in lChildRenderers) {
					if(lRenderer.gameObject.name =="Torch" || lRenderer.gameObject.name == "Picture Camera"){ lRenderer.enabled= false;}
				}
				swordRenderer.enabled = true;
				}
				else switchItem(sign);
				break;
			default:

				foreach ( SpriteRenderer lRenderer in lChildRenderers) {
					if(lRenderer.gameObject.name =="Torch" || lRenderer.gameObject.name == "Picture Camera"){ lRenderer.enabled= false;}
				}
				swordRenderer.enabled = false;
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
