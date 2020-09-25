using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float moveSpeedOriginal;
    public float speedMultiplier;

    public float speedUpDistance;
    private float speedUpDistanceOriginal;
    private float speedUpDistanceCount;

    public float jumpForce;
    private Rigidbody2D myRigidbody;
   
    public bool grounded;
    public LayerMask WhatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    public AudioSource jumpSound;
    public AudioSource deathSound;

    //private Collider2D myCollider;

    private Animator myAnimator;

    public float jumpTime;
    private float jumpTimeCounter;
    private bool stoppedJumping;
    private bool canDoubleJump;
    private bool startedJumping;

    public GameManager theGameManager;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        //myCollider = GetComponent<Collider2D>();

        myAnimator = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;

        speedUpDistanceCount = speedUpDistance;

        moveSpeedOriginal = moveSpeed;

        speedUpDistanceOriginal = speedUpDistanceCount;

        stoppedJumping = true;
        startedJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        //grounded = Physics2D.IsTouchingLayers(myCollider, WhatIsGround);

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);

        if (transform.position.x > speedUpDistanceCount)
        {
            speedUpDistanceCount += speedUpDistance;

            speedUpDistance = speedUpDistance * speedMultiplier;

            moveSpeed = moveSpeed * speedMultiplier;
        }

        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                stoppedJumping = false;
                startedJumping = true;
                jumpSound.Play();
            }
            if (!grounded && canDoubleJump)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime;
                stoppedJumping = false;
                canDoubleJump = false;
                jumpSound.Play();
            }

        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
        {
            if(jumpTimeCounter > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }

        }

        if (Input.GetKeyUp (KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
            startedJumping = false;
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if(other.gameObject.tag == "killbox")
        {
            theGameManager.RestartGame();
            moveSpeed = moveSpeedOriginal;
            speedUpDistanceCount = speedUpDistanceOriginal;
            speedUpDistance = speedUpDistanceOriginal;
            deathSound.Play();

        }
    }
}
