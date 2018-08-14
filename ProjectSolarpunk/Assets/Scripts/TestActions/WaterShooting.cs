using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShooting : MonoBehaviour {

    [SerializeField] WaterObject waterObject;
    [SerializeField] float fireSpeed = 5;
    float facingDirection = 1;
	
	// Update is called once per frame
	void Update () {
        HandleShootingInput();
	}

    private void HandleShootingInput()
    {
        //Get horizontal input
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Action"))
        {
            //Get vert input
            float verticalInput = Input.GetAxisRaw("Vertical");

            //Shoot even when standing still
            if(horizontalInput == 0 && verticalInput == 0)
            {
                horizontalInput = facingDirection;
            }

            //Get total 8-way input direction
            Vector2 inputDirection = new Vector2(horizontalInput, verticalInput);
        
            //Get player movement speed
            Vector2 playerVelocity =
                GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity;
            
            //Create new water bullet
            WaterObject newWaterObject =
                Instantiate(waterObject, transform.position, transform.rotation);

            //Set the speed of the water bullet
            Rigidbody2D waterObjectRigidbody = newWaterObject.GetComponent<Rigidbody2D>();
            waterObjectRigidbody.velocity = (inputDirection * fireSpeed)
                + new Vector2(playerVelocity.x,0);

            //IMPORTANT NOTE: Shots need to be fired w/ different velocities depending
            //                on the direction they are travelling. ???? i think

        }

        facingDirection = horizontalInput;
    }

}
