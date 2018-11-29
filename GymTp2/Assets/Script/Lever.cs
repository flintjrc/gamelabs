using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {
    [SerializeField] bool pulled = false;
    [SerializeField] float timeToReset = 10f;
    [SerializeField] GameObject doorLever;
    DoorWithLever doorLeverScript;

    void Start()
    {
        doorLeverScript = doorLever.GetComponent<DoorWithLever>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !pulled)
        {
            toggleLever();
            StartCoroutine(timedLever());
        }

    }

    void toggleLever()
    {
        pulled = !pulled;
        doorLeverScript.toggleDoor(pulled);
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    IEnumerator timedLever()
    {
        yield return new WaitForSeconds(timeToReset);
        toggleLever();
    }
}
