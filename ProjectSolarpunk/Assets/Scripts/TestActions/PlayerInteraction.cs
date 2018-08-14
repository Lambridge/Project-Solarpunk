using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    [SerializeField] float rayLength = 0.75f;

    enum FacingDirection { right, left }
    FacingDirection currentFacingDirection = FacingDirection.right;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Interact();
	}

    void Interact()
    {
        if(Input.GetButtonDown("Interact"))
        {
            //Get rayDirection for new raycast
            Vector3 rayDirection = new Vector3(rayLength, 0, 0);
            if (currentFacingDirection == FacingDirection.left)
                rayDirection = new Vector3(-rayLength, 0, 0);
            //Create new raycast
            //Find what it hits
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection);

            //Tell the hit object to do something
        }
    }
}
