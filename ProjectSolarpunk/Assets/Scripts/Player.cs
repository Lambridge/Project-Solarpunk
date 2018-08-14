using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    #region Properties
    private bool IsOnGround
    {
        get
        {
            return UpdateIsOnGround();
        }
    }

    public bool BladeObtained
    {
        set
        {
            bladeObtained = value;
        }
    }

    public bool GunObtained
    {
        set
        {
            waterGun.SetActive(true);
            gunObtained = value;
        }
    }

    #endregion

    #region WeaponUnlocks
    bool bladeObtained;
    bool gunObtained;
    #endregion

    #region Actions
    public enum PlayerAction { none, attacking, shooting, takingDamage }
    [SerializeField] PlayerAction currentAction = PlayerAction.none;
    public PlayerAction CurrentAction
    {
        set
        {
            currentAction = value;
        }
    }
    #endregion

    #region Jumping
    [SerializeField] bool canJump = false;
    [SerializeField] float jumpTimeMax = 0.2f;
    float jumpTimeRemaining = 0;
    #endregion

    #region Slash Attack
    [SerializeField] GameObject hitboxPrefab;
    [SerializeField] GameObject hitbox;
    [SerializeField] float offsetFromParentPosition = 0.75f;
    [SerializeField] float attackCooldownTime = 0.18f;
    float cooldownTimeRemaining = 0;
    #endregion

    #region Movement
    Rigidbody2D myRigidbody2D;
    float facingDirection = 1;
    [SerializeField] float moveSpeed = 4.3f;
    [SerializeField] float jumpVelocity;
    #endregion

    #region Taking damage
    [SerializeField] float knockbackVelocity = 1.25f;
    [SerializeField] float hitstunTimeMax = 0.5f;
    float hitstunTimeRemaining = 0;
    #endregion

    #region Ground detection
    [SerializeField] Transform groundDetectPoint;
    [SerializeField] float groundDetectRadius = 0.4f;
    [SerializeField] LayerMask whatIsGround;
    bool isOnGround;
    #endregion

    Color defaultColor;
    [SerializeField] GameObject waterGun;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        defaultColor = gameObject.GetComponent<SpriteRenderer>().color;

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        BladeObtained = gameManager.BladeObtained;
        GunObtained = gameManager.GunObtained;
    }

    void Update () {
        if(currentAction == PlayerAction.none)
        {
            MovementInput();
            JumpInput();
            if(bladeObtained)
                SlashAttackInput();
            if(gunObtained)
                ShootingInput();
        }
        if(currentAction == PlayerAction.attacking)
        {
            if(cooldownTimeRemaining <= 0)
            {
                Destroy(hitbox);
                currentAction = PlayerAction.none;
            }

            //Stop slash speedup glitch
            //TODO: discover cause of this glitch
            if (myRigidbody2D.velocity.x > moveSpeed)
                myRigidbody2D.velocity =
                    new Vector2(moveSpeed, myRigidbody2D.velocity.y);
            if (myRigidbody2D.velocity.x < -moveSpeed)
                myRigidbody2D.velocity =
                    new Vector2(-moveSpeed, myRigidbody2D.velocity.y);

            cooldownTimeRemaining += Time.deltaTime * -1;
        }
        if (currentAction == PlayerAction.shooting)
        {
            ShootingInput();
        }
        if (currentAction == PlayerAction.takingDamage)
        {
            hitstunTimeRemaining -= Time.deltaTime;
            if(hitstunTimeRemaining <= 0)
                CurrentAction = PlayerAction.none;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(181, 0, 0);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
        }
        UpdateIsOnGround();
    }

    private void ShootingInput()
    {
        if (Input.GetButton("Shoot"))
        {
            //Declarations
            WaterGunA waterGunScript = gameObject.GetComponentInChildren<WaterGunA>();
            CurrentAction = PlayerAction.shooting;

            //Get input
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            //Create input vector
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
            if(inputVector == Vector2.zero)
                inputVector = new Vector2(facingDirection, 0);

            //Tell the gun to position itself and shoot
            //Based on our player input
            waterGunScript.PositionAndShootGun(inputVector.normalized);
        }
        else if(currentAction == PlayerAction.shooting)
        {
            currentAction = PlayerAction.none;
        }
    }

    private bool UpdateIsOnGround()
    {
        Collider2D[] groundColliders =
            Physics2D.OverlapCircleAll(groundDetectPoint.position, groundDetectRadius, whatIsGround);

        return groundColliders.Length > 0;
    }

    private void MovementInput()
    {
        //Get horizontal input
        float movementInput = Input.GetAxis("Horizontal");
        //Get velocities to set
        float xVelocity = movementInput * moveSpeed * Time.deltaTime * 75;
        float yVelocity = myRigidbody2D.velocity.y;
        Vector2 velocityToSet = new Vector2(xVelocity, yVelocity);
        //Set velocity
        myRigidbody2D.velocity = velocityToSet;

        //Set facing direction
        if (movementInput != 0)
            facingDirection = movementInput;
    }

    private void JumpInput()
    {
        if (IsOnGround)
        {
            canJump = true;
        }
        else if (!Input.GetButton("Jump"))
        {
            canJump = false;
        }
        else if (jumpTimeRemaining <= 0)
        {
            canJump = false;
        }

        if (canJump)
        {
            float xVelocity = myRigidbody2D.velocity.x;
            float yVelocity = jumpVelocity;
            Vector2 velocityToSet = new Vector2(xVelocity, yVelocity);
            
            if (Input.GetButtonDown("Jump"))
            {
                myRigidbody2D.velocity = velocityToSet;
                jumpTimeRemaining = jumpTimeMax;
            }
            if (Input.GetButton("Jump") && jumpTimeRemaining > 0)
            {
                myRigidbody2D.velocity = velocityToSet;
            }

            jumpTimeRemaining += Time.deltaTime * -1;
        }
    }

    void SlashAttackInput()
    {
        if (Input.GetButtonDown("Attack") && cooldownTimeRemaining <= 0)
        {
            //Set action to "attacking"
            currentAction = PlayerAction.attacking;
            if(IsOnGround)
                myRigidbody2D.velocity *= 0.5f;

            //Create the new hitbox
            hitbox =
                        Instantiate(hitboxPrefab, 
                        GetHitboxPosition(), 
                        new Quaternion(0, 0, 0, 0));
            hitbox.transform.parent = transform;

            //Reset the cooldown timer
            cooldownTimeRemaining = attackCooldownTime;
        }
    }

    Vector3 GetHitboxPosition()
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
            offsetVector =
                offsetVector.normalized * offsetFromParentPosition;
            //TODO: remove phantom number
        }

        //If no input direction
        //Rely on facing direction
        if (offsetVector == Vector3.zero)
        {
            offsetVector = new Vector3(facingDirection, 0) * offsetFromParentPosition;
        }

        //The position of the hitbox when instantiated
        //Default is to the right of pc
        return transform.position + offsetVector;
    }

    public void TakeDamage(float damageAmount)
    {
        cooldownTimeRemaining = 0;
        Destroy(hitbox);
        //if (hitbox != null)
        //{
            
        //}

        CurrentAction = PlayerAction.takingDamage;
        hitstunTimeRemaining = hitstunTimeMax;
        gameObject.GetComponent<PlayerHealth>().UpdateHealth(damageAmount);
        KnockPlayerBack();
    }

    public void KnockPlayerBack()
    {
        //Set knockback to opposite of current movement
        Vector2 knockbackVector = myRigidbody2D.velocity * -1;

        //Then set it to a velocity a certain magnitude
        knockbackVector = knockbackVector.normalized * knockbackVelocity;

        //Then apply knockback
        myRigidbody2D.velocity = knockbackVector;

        //TODO: consider shifting knockback so player bounces back in either just x/y and not both
    }

}
