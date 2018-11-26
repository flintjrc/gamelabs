using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym13_PlayerPush : MonoBehaviour {


    public KeyCode grabInput;
    public KeyCode releaseInput;

    bool isAttached = false;
    Collider attachedCollider = null;


    private void Update()
    {
        if (isAttached && Input.GetKeyDown(releaseInput))
        {            
            attachedCollider.GetComponent<FixedJoint>().connectedBody = null;
            Destroy(attachedCollider.GetComponent<FixedJoint>());
            attachedCollider = null;
            isAttached = false;
        }
    }

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
    
}
