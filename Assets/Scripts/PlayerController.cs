using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float speedMultiplier;
    private float startSpeed;

    public float speedIncreaseMilestone;
    private float speedMilestoneCount;
    private float startSpeedMilestoneCount;

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


	// Use this for initialization
	void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;

        startSpeed = moveSpeed;
        startSpeedMilestoneCount = speedMilestoneCount;
        hasJumped = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);

        if(transform.position.x > speedIncreaseMilestone)
        {
            moveSpeed = moveSpeed * speedMultiplier;
            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
        }

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
            gameManager.RestartGame();
            moveSpeed = startSpeed;
            speedMilestoneCount = startSpeedMilestoneCount;
        }
    }
}
