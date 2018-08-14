using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun2 : MonoBehaviour {

    [SerializeField] WaterObject bullet;
    [SerializeField] float bulletSpeed;

    float offsetFromParentPosition = 0.75f;
    [SerializeField] float gunCooldownTime = 0.5f;
    float cooldownTimeRemaining = 0;

    float facingDirection = 1;
    float distanceReductionMultiplier = 0.8f;

    Vector2 offsetVector;

	void Update () {
        PositionGun();
        ShootInput();
	}

    private void PositionGun()
    {
        //Get axis input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        //Set where hitbox will be instantiated
        offsetVector = 
            new Vector3(horizontalInput, verticalInput) 
            * offsetFromParentPosition;

        //When attacking diagonally
        //Hitbox does not reach full horizontal and vertical axis distance
        if (offsetVector.x != 0 && offsetVector.y != 0)
        {
            offsetVector =
                new Vector3(horizontalInput, verticalInput) 
                * (offsetFromParentPosition * distanceReductionMultiplier);
            //TODO: remove phantom number
        }

        //If no input direction
        //Rely on facing direction
        if (offsetVector == Vector2.zero)
        {
            offsetVector = new Vector3(facingDirection, 0) 
                * offsetFromParentPosition;
        }

        transform.position = 
            new Vector2(transform.parent.position.x, transform.parent.position.y)
            + offsetVector;
    }

    private void ShootInput()
    {
        //Cooldown timer goes down
        cooldownTimeRemaining += Time.deltaTime * -1;

        if (Input.GetButton("Shoot") && cooldownTimeRemaining <= 0)
        {
            //Create new water bullet
            WaterObject newBullet =
                Instantiate(bullet, transform.position, transform.rotation);

            //Set the speed of the water bullet
            Rigidbody2D waterObjectRigidbody = newBullet.GetComponent<Rigidbody2D>();
            waterObjectRigidbody.velocity = 
                (offsetVector.normalized * bulletSpeed);

            //Set cooldown
            cooldownTimeRemaining = gunCooldownTime;
        }
    }

}
