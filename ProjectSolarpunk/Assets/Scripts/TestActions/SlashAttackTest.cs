using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttackTest : MonoBehaviour {

    [SerializeField] GameObject hitboxPrefab;
    float offsetFromParentPosition = 0.75f;
    [SerializeField] float attackCooldownTime = 0.18f;
    float cooldownTimeRemaining = 0;

    float facingDirection = 1;

    // Update is called once per frame
    void Update()
    {
        ChangeFacingDirection();
        SlashAttack();
    }

    private void ChangeFacingDirection()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if(horizontalInput != 0)
            facingDirection = horizontalInput;
    }

    void SlashAttack()
    {
        if (Input.GetButtonDown("Action") && cooldownTimeRemaining <= 0)
        {
            //Get axis input
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            //Set where hitbox will be instantiated
            Vector3 offsetVector = new Vector3(horizontalInput, verticalInput) * offsetFromParentPosition;

            //When attacking diagonally
            //Hitbox does not reach full horizontal and vertical axis distance
            if (offsetVector.x != 0 && offsetVector.y != 0)
            {
                //offsetVector = offsetVector * 0.75f;
                //offsetVector = new Vector3(offsetVector.x, offsetVector.y) * 0.5f;
                offsetVector =
                    new Vector3(horizontalInput, verticalInput) * (offsetFromParentPosition * 0.8f);
                //TODO: remove phantom number
                Debug.Log("test of x/y components works");
            }

            //If no input direction
            //Rely on facing direction
            if (offsetVector == Vector3.zero)
            {
                offsetVector = new Vector3(facingDirection, 0) * offsetFromParentPosition;
                Debug.Log("zero direction");
            }

            //The position of the hitbox when instantiated
            //Default is to the right of pc
            Vector3 hitboxPosition = transform.position + offsetVector;

            //Create the new hitbox
            GameObject hb =
                        Instantiate(hitboxPrefab, hitboxPosition, new Quaternion(0, 0, 0, 0));
            hb.transform.parent = transform;

            //Reset the cooldown timer
            cooldownTimeRemaining = attackCooldownTime;
        }

        cooldownTimeRemaining += Time.deltaTime * -1;
    }
}
