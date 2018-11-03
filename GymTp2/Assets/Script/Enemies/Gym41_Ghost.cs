using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym41_Ghost : MonoBehaviour {

    //Spirit Sprite
    private SpriteRenderer[] GhostSprite;

    //SpiritBody
    private Rigidbody GhostBody;

    //True if the ennemy is flashed
    private bool isFlashed = false;

    //Color of the immobile state when ghost is flashed
    private Color FlashedColor = Color.blue;

    //Default color for the ghost sprite
    private Color NormalColor = Color.white;

    //The ghost turns over when he exceeds this time
    [SerializeField] private float MAX_TIME = 100;

    //Ghost's speed
    [SerializeField] private float speed = 2.0f;

    //Timer updated for each frame
    [SerializeField] private float timer = 0;

    // Use this for initialization
    void Start () {

        GhostBody = GetComponent<Rigidbody>();

        GhostSprite = gameObject.GetComponentsInChildren<SpriteRenderer>();

        //Freeze positions and rotations which are unused
        GhostBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }


    // Update is called once per frame
    void Update()
    {
        if (!isFlashed)
        {
            //We update the time passed
            timer = timer + 0.5f;

            //Enemy's moving is updated
            EnemyMovingHorizontal();
        }
        else
        {
            //Ghost doesn't move
            GhostBody.velocity = new Vector2(0.0f, 0.0f);
        }
    }

    void EnemyMovingHorizontal()
    {
        //The Enemy's speed is updated
        GhostBody.velocity = new Vector2(-speed, 0.0f);

        //When timer exceeds Max time, then we change speed direction, we restart the timer and we flip the enemy's frame
        if (timer >= MAX_TIME)
        {
            speed *= -1.0f;
            timer = 0;
            Flip();
        }

    }
    void Flip()
    {

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    //This function is called when an object triggers the box collider of the ghost. It doesn't work if the object has a collider which is not a trigger.
    //Then, camera rays must be a trigger as collider.
    //Moreover, the ennemy must have a collider which is not a trigger, otherwise, the hero can just trigger the ghost to immobilize him.
    private void OnTriggerEnter(Collider collision)
    {
        if (Input.GetButtonDown("Fire1") && !isFlashed)
        {

            //We need to use startCoroutine to call the function "FlashGhost" because we need to measure the time of the immobilisation state
            StartCoroutine(FlashGhost());
        }

    }

    IEnumerator FlashGhost()
    {
        //Ghost is immobilized, then we put the value at false
        isFlashed = true;

        foreach (SpriteRenderer SpriteParts in GhostSprite)
        {
            //The sprite of the ennemy become red
            SpriteParts.color = FlashedColor;
        }

        //We wait for 3s
        yield return new WaitForSeconds(3.0f);

        //Ennemy is open to attack
        isFlashed = false;

        //The sprite of ennemy become normal
        foreach (SpriteRenderer SpriteParts in GhostSprite)
        {
            //The sprite of the ennemy become red
            SpriteParts.color = NormalColor;
        }
    }
}
