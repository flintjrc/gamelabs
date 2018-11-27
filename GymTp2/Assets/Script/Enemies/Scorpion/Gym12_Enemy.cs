using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym12_Enemy : MonoBehaviour {

    //Ennnemy Health
    [SerializeField] private int EnnemyHealth = 3;

    //Number of damages taken by ennemy
    [SerializeField] private int DamageReceived = 1;

    //Sprite which become red when the ennemy is invincible after an attack
    private SpriteRenderer[] enemySpritesInvincible;

    //Color of the invincible state
    private Color InvincibleColor = Color.red;

    //Default color for a sprite
    private Color NormalColor = Color.white;

    //True if the ennemy can be hurt
    private bool NotInvincible = true;

    //Rigidbody EnemyRigidBody;


    void Start()
    {
        //EnemyRigidBody = GetComponent<Rigidbody>();
        enemySpritesInvincible = gameObject.GetComponentsInChildren<SpriteRenderer>();

        //EnemyRigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
    }


    //Function which is called when the ennemy is attacked
    void DeductPoints(int DamageAmount)
    {
        EnnemyHealth -= DamageAmount;
    }

    //This function is called when an object triggers the box collider of an ennemy. It doesn't work if the object has a collider which is not a trigger.
    //Then, Torch, camera rays and sword must be a trigger as collider.
    //Moreover, the ennemy must have a collider which is not a trigger, otherwise, the hero can just trigger the ennemy to kill him.
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().CompareTag("sword") && NotInvincible)
        {
            DeductPoints(DamageReceived);

            //We need to use startCoroutine to call the function "isInvicible" because we need to measure the time of the invincible state
            StartCoroutine(isInvicible());
        }
        
    }

    void FixedUpdate () {

        //Ennemy is killed when he doesn't have hit points anymore
		if (EnnemyHealth <= 0)
        {
            Destroy(gameObject);
        }

	}

    //Function reflecting the time when the ennemy is invincible (0.75 seconds)
    //This function return an IEnumerator because "yield return new WaitForTime" return an IEnumerator
    IEnumerator isInvicible()
    {
        //Ennemy is invincible, then we put the value at true
        NotInvincible = false;

        foreach (SpriteRenderer SpriteEnemy in enemySpritesInvincible)
        {
            //The sprite of the ennemy become red
            SpriteEnemy.color = InvincibleColor;
        }

        //We wait for 0.75s
        yield return new WaitForSeconds(0.75f);

        //Ennemy is open to attack
        NotInvincible = true;

        //The sprite of ennemy become normal
        foreach (SpriteRenderer SpriteEnemy in enemySpritesInvincible)
        {
            //The sprite of the ennemy become red
            SpriteEnemy.color = NormalColor;
        }
    }

}
