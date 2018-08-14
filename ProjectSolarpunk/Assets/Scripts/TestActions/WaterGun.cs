using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour {

    [SerializeField] Transform playerTransform;
    [SerializeField] WaterObject bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] float timeBetweenBulletsInSeconds;
    float timeSinceLastBullet = 0;

    [SerializeField] Vector3[] gunPositionRelativeToPlayer;
    //array number is input direction akin to numpad
    //5 is no input direction
    //0 is aerial downward direction

    [SerializeField] Vector3[] shootingDirection;

    float lastHorizontalInput = 0;

    // Use this for initialization
    void Start () {
        playerTransform = transform.parent.transform;
	}
	
	// Update is called once per frame
	void Update () {
        ChangeGunPosition();
        HandleShootingInput();
        SetLastHorizontalInput();
	}

    private void SetLastHorizontalInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            lastHorizontalInput = horizontalInput;
        }
    }

    private void ChangeGunPosition()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");
        Vector3 newGunPosition;

        //No input; facing right
        //Default
        newGunPosition = transform.position + gunPositionRelativeToPlayer[6];

        //No input; facing left
        if (inputHorizontal == -1 && inputVertical == 0 && lastHorizontalInput == -1)
        {
            
            newGunPosition = transform.position + gunPositionRelativeToPlayer[4];
        }
        //left/right input
        else if(inputHorizontal != 0 && inputVertical == 0)
        {
            //Input right
            newGunPosition = transform.position + gunPositionRelativeToPlayer[6];

            if(inputHorizontal == -1)
            {
                //Input left
                newGunPosition = transform.position + gunPositionRelativeToPlayer[4];
            }
        }
        //up/down input
        else if (inputVertical != 0 && inputHorizontal == 0)
        {
            //Input up
            newGunPosition = transform.position + gunPositionRelativeToPlayer[8];

            if (inputVertical == -1)
            {
                //Input down
                newGunPosition = transform.position + gunPositionRelativeToPlayer[0];
            }
        }
        //diagonal input
        else if (inputHorizontal != 0 && inputVertical != 0)
        {
            //input diagonal up-right
            newGunPosition = transform.position + gunPositionRelativeToPlayer[9];

            if (inputHorizontal == 1 && inputVertical == -1)
            {
                //input diagonal down-right
                newGunPosition = transform.position + gunPositionRelativeToPlayer[3];
            }
            else if(inputHorizontal == -1)
            {
                //input diagonal up-left
                newGunPosition = transform.position + gunPositionRelativeToPlayer[7];

                //input diagonal down-left
                if (inputVertical == -1)
                {
                    newGunPosition = transform.position + gunPositionRelativeToPlayer[1];
                }
            }
        }

        transform.position = newGunPosition;
    }

    //private void ChangeGunPosition()
    //{
    //    Vector3 horizontalOffset = new Vector3(0.5f, 0, 0);
    //    Vector3 verticalOffset = new Vector3(0, 0.75f, 0);

    //    if (Input.GetAxisRaw("Horizontal") != 0)
    //    {
    //        if (Input.GetAxisRaw("Horizontal") == -1)
    //        {
    //            if(Input.GetAxisRaw("Vertical") == 1)
    //            {
    //                transform.position = 
    //                    playerTransform.position + (horizontalOffset * -1) + (verticalOffset * 0.5f);
    //            }
    //            else
    //                transform.position = playerTransform.position + (horizontalOffset * -1);
    //        }
    //        if (Input.GetAxisRaw("Horizontal") == 1)
    //        {
    //            if (Input.GetAxisRaw("Vertical") == 1)
    //            {
    //                transform.position =
    //                    playerTransform.position + horizontalOffset + (verticalOffset * 0.5f);
    //            }
    //            else
    //                transform.position = playerTransform.position + horizontalOffset;
    //        }
    //    }
    //    else if(Input.GetAxisRaw("Vertical") == 1)
    //    {
    //        transform.position = playerTransform.position + verticalOffset;
    //    }


    //}

    private void HandleShootingInput()
    {
        if (Input.GetButton("Action") && timeSinceLastBullet >= timeBetweenBulletsInSeconds)
        {
            timeSinceLastBullet = 0;
            //Shoot a bullet

            Vector2 directionToFire = GetDirectionToFire();
            //Create new water bullet, then set speed
            WaterObject newBullet =
                Instantiate(bullet, transform.position, transform.rotation);
            //Set the speed of the water bullet
            Rigidbody2D waterObjectRigidbody = newBullet.GetComponent<Rigidbody2D>();
            waterObjectRigidbody.velocity = (directionToFire * bulletSpeed);
        }

        timeSinceLastBullet += Time.deltaTime;
    }

    Vector2 GetDirectionToFire()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        //Make bullets fire straight if standing still
        if(horizontalInput == 0 && transform.position.y == playerTransform.position.y)
        {
            if (transform.position.x < playerTransform.position.x)
                horizontalInput = -1f;
            if (transform.position.x > playerTransform.position.x)
                horizontalInput = 1f;
        }

        Vector2 inputVector = new Vector2 (horizontalInput, verticalInput);
        return inputVector;
    }
}
