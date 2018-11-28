using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoesPickUp : MonoBehaviour {

    private SpriteRenderer shoesSprite;
    [SerializeField]private bool shoesTaken;
    private BoxCollider shoesBox; 
    playerController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        shoesSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        shoesTaken = false;
        shoesBox = GetComponent<BoxCollider>();
        shoesBox.enabled = true;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && !shoesTaken)

        {
            player.TakeShoes();
            shoesTaken = true;
            shoesSprite.enabled = false;
            shoesBox.enabled = false;

        }

    }

    public bool GetShoesTaken()
    {
        return shoesTaken;
    }
}
