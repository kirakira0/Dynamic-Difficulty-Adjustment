using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 1f;
    public LayerMask platformLayer;
    
    private float GRAVITY_FLOAT = 3;
    private Rigidbody2D playerRigidbody;

    public bool grounded = false;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float groundedRadius;
    private bool hasJumped;
    private bool canDoubleJump;
    public float jumpTime;
    private float jumpTimeCounter;

    void Awake()
    {
        jumpTimeCounter = jumpTime;
        playerRigidbody = GetComponent<Rigidbody2D>();        
    }
    
    void Update() { 
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);       
        // Vector2 position = transform.position;
        // Vector2 direction = transform.TransformDirection(Vector2.down);
        // RaycastHit2D hit = Physics2D.Raycast(position, direction, 1000f);
        // if (hit.collider.tag == "Platform") {
        //     DropPlayer();
        // } else {
        //     playerRigidbody.position = new Vector2(playerRigidbody.position.x + 0.1f, playerRigidbody.position.y);            
        // }
    }
    
    void FixedUpdate()
    {
        // Player jumps with Z or Up Arrow key or Space bar.
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) {
            if (grounded)
            {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
                hasJumped = true;
                canDoubleJump = true;
            }
            if(!grounded && canDoubleJump)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
                hasJumped = true;
                jumpTimeCounter = jumpTime;
                canDoubleJump = false;
            }
        }
        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if(jumpTimeCounter > 0 && hasJumped)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
            grounded = false;
            hasJumped = false;
        }

        if(grounded)
        {
            jumpTimeCounter = jumpTime;
        }
    }

    /**
     * If the player falls off of a platform, use raycasting to determine where they
     * should be dropped into the scene.
     */
    private void DropPlayer() {
        playerRigidbody.gravityScale = GRAVITY_FLOAT;
    }

    /**
     * Helper method that allows player to float above the game scene until raycasting
     * has determined where they should be placed.
     */
    private void SuspendPlayerGravity() {

    }

     void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "killbox")
        {
            Debug.Log("caught");
        }


    }
}
