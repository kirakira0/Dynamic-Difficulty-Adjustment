using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public PlatformGenerator2 PLATFORM_GENERATOR; 
    public Transform PLAYER_SPAWN_POINT;
    public Transform STARTING_PLATFORM_SPAWN_POINT; 
    public GameObject STARTING_PLATFORM; 

    private float GRAVITY_FLOAT = 3;
    public float MOVE_SPEED = 6;

    public int lives = 3;
    public GameObject spawnPoint; 
    
    public bool flightMode = false; 
    
    public float moveSpeed;

    public float jumpForce;

    public float jumpTime;
    private float jumpTimeCounter;

    public Rigidbody2D myRigidBody;

    public bool grounded;
    public Transform groundCheck;
    private bool hasJumped;
    private bool canDoubleJump;

    public bool inDeathcatcher = false;

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

    public void findPlatform() {
        moveSpeed = 0;  
        transform.position = new Vector3(transform.position.x, transform.position.y + 9.0f, 0);
        myRigidBody.gravityScale = 0;
        while (myRigidBody.gravityScale == 0) {
            Vector2 position = transform.position;
            Vector2 direction = transform.TransformDirection(Vector2.down);
            RaycastHit2D hit = Physics2D.Raycast(position, direction, 1000f);
            if (hit.collider.tag == "Platform") {
                myRigidBody.gravityScale = GRAVITY_FLOAT;
            } else {
                myRigidBody.position = new Vector2(myRigidBody.position.x + 0.1f, myRigidBody.position.y);            
            }
        }
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

        // myAnimator.SetFloat("Speed", myRigidBody.velocity.x);
        // myAnimator.SetBool("Grounded", grounded);
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "killbox") {
            canDoubleJump = true;
            PLATFORM_GENERATOR.paused = true; 
            StartCoroutine(ExecuteAfterTime(5f));
        }

        // if(other.gameObject.tag == "killbox")
        // {
        //     Debug.Log("caught");
        //     this.lives--;
        //     if (this.lives <= 0) {
        //         gameManager.RestartGame();
        //     } else {
        //         inDeathcatcher = true;
        //         // this.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, 0);
        //         // canDoubleJump = true;
        //     }
        // }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(STARTING_PLATFORM, new Vector3(STARTING_PLATFORM_SPAWN_POINT.transform.position.x, STARTING_PLATFORM_SPAWN_POINT.transform.position.y, 0), Quaternion.identity);

        transform.position = new Vector3(PLAYER_SPAWN_POINT.transform.position.x, PLAYER_SPAWN_POINT.transform.position.y, 0);


        Debug.Log("resume");
        PLATFORM_GENERATOR.paused = false; 
    }
}
