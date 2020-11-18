using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int lives = 3;
    public GameObject spawnPoint; 
    
    public bool flightMode = false; 
    
    public float moveSpeed;
    // public float speedMultiplier;
    // private float startSpeed;

    // public float speedIncreaseMilestone;
    // private float speedMilestoneCount;
    // private float startSpeedMilestoneCount;

    public float jumpForce;

    public float jumpTime;
    private float jumpTimeCounter;

    private Rigidbody2D myRigidBody;

    public bool grounded;
    public Transform groundCheck;
    private bool hasJumped;
    private bool canDoubleJump;

    public LayerMask whatIsGround;
    public float groundedRadius;

    private Animator myAnimator;

    public GameManager gameManager;

	void Awake ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        hasJumped = false;
    }
	
	void Update ()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);

        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (grounded)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                hasJumped = true;
                canDoubleJump = true;
            }

            if(!grounded && canDoubleJump)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                hasJumped = true;
                jumpTimeCounter = jumpTime;
                canDoubleJump = false;
            }
        }

        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if(jumpTimeCounter > 0 && hasJumped)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
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

        myAnimator.SetFloat("Speed", myRigidBody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "killbox")
        {
            this.lives--;
            if (this.lives <= 0) {
                gameManager.RestartGame();
            } else {
                this.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, 0);
                canDoubleJump = true;
            }
        }
    }
}
