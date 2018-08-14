using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBulletShooting_Two : MonoBehaviour {

    [SerializeField] WaterObject bullet;
    [SerializeField] float bulletSpeed;
	
	void Update () {
        if (Input.GetButtonDown("Action"))
            HandleShooting();
    }
    
    void HandleShooting()
    {
        //Get raw input axese
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        //Get total 8-way input direction
        Vector2 inputDirection = new Vector2(horizontalInput, verticalInput);

        Vector2 fireVector = GetDirectionalFireVector(inputDirection);

        if(inputDirection != Vector2.zero)
        {
            //Create new water bullet, then set speed
            WaterObject newBullet =
                Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody2D bulletRigidbody = newBullet.GetComponent<Rigidbody2D>();
            //bulletRigidbody.velocity = inputDirection * (bulletSpeed * directionalMagnitudeChange) ;
        }

        
    }

    private Vector2 GetDirectionalFireVector(Vector2 inputDirection)
    {
        Vector2 fireVector = new Vector2(0.5f, 0.15f); //no input

        if(inputDirection.x != 0)
        {
            if(inputDirection.y == 0)
            {
                if (inputDirection.x == -1)
                    fireVector = new Vector2(-1, 0); //left
                if (inputDirection.x == 1)
                    fireVector = new Vector2(1, 0);  //right
            }
            else if(inputDirection.y == 1)
            {
                if (inputDirection.x == -1)
                    fireVector = new Vector2(-1, 1); //up, left
                if(inputDirection.x == 1)
                    fireVector = new Vector2(1, 1);  //up, right
            }
            else if (inputDirection.y == -1)
            {
                if (inputDirection.x == -1)
                    fireVector = new Vector2(-1, -1); //down, left
                if (inputDirection.x == 1)
                    fireVector = new Vector2(-1, 1);  //down, right
            }
        }
        else if(inputDirection.y != 0)
        {
            if (inputDirection.y == 1)
                fireVector = new Vector2(0, 1.5f);  //Upward
            else
                fireVector = new Vector2(0, 1);     //Downward
        }

        return fireVector;
    }

    private float GetDirectionalMagnitudeChange(Vector2 inputDirection)
    {
        float dmc = 2; //left/right and default

        if(inputDirection.y > 0)
        {
            dmc = 1.25f; //upward
            if (inputDirection.x != 0)
                dmc = 1f; //diagonal upward
        }
        else if(inputDirection.y < 0)
        {
            dmc = 0.5f; //downward
            if (inputDirection.x != 0)
                dmc = 1f; //diagonal downward
        }
        else if(inputDirection.x == 0)
        {
            dmc = 0.5f; //no input
        }

        return dmc;
    }
}
