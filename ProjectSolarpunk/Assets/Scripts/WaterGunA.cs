using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunA : MonoBehaviour {

    [SerializeField] WaterObject bullet;
    [SerializeField] float bulletSpeed;

    float offsetFromParentPosition = 0.75f;
    [SerializeField] float gunCooldownTime = 0.5f;
    float cooldownTimeRemaining = 0;

    Vector3 offsetVector;
    Vector3 gunPointingDirection;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Cooldown timer goes down
        cooldownTimeRemaining += Time.deltaTime * -1;
    }

    public void PositionAndShootGun(Vector2 inputDirection)
    {
        PositionGun(inputDirection);
        ShootGun();
    }

    void PositionGun(Vector2 inputDirection)
    {
        //if(transform.position == transform.parent.position)
        {
            offsetVector = inputDirection;

            if (offsetVector.x != 0 && offsetVector.y != 0)
            {
                offsetVector.Normalize();
            }

            transform.position = transform.parent.position + offsetVector;
        }
        //else
        //{
        //    transform.RotateAround(transform.parent.position,
        //        Vector3.up, direction * Time.deltaTime);

        //}
    }

    void ShootGun()
    {
        

        if (Input.GetButton("Shoot") && cooldownTimeRemaining <= 0)
        {
            gunPointingDirection = transform.position - transform.parent.position;

            //Create new water bullet
            WaterObject newBullet =
                Instantiate(bullet, transform.position, transform.rotation);

            //Set the speed of the water bullet
            Rigidbody2D waterObjectRigidbody = newBullet.GetComponent<Rigidbody2D>();
            waterObjectRigidbody.velocity =
                (gunPointingDirection.normalized * bulletSpeed);

            //Set cooldown
            cooldownTimeRemaining = gunCooldownTime;
        }

        
    }

}
