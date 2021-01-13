using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // CONSTANTS
    // --------------------------------------------------------------
    public float NORMAL_MOVE_SPEED = 6;
    public int TOTAL_LIVES = 3;

    // CHANGING VARIABLES
    // --------------------------------------------------------------
    private float playerSpeed;
    private int remainingLives;
    private bool fallen;

    public GameObject FADE;

    public PlatformGenerator2 PLATFORM_GENERATOR; 
    public Transform PLAYER_SPAWN_POINT;
    public Transform STARTING_PLATFORM_SPAWN_POINT; 
    public GameObject STARTING_PLATFORM; 


    private float GRAVITY_FLOAT = 3;

    public int lives = 3;
    public GameObject spawnPoint; 
        
    public float jumpForce;

    public float jumpTime;
    private float jumpTimeCounter;

    private Rigidbody2D playerRigidbody;

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
        remainingLives = TOTAL_LIVES;
        fallen = false;
        playerSpeed = NORMAL_MOVE_SPEED;


        playerRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        hasJumped = false;
    }

    // public void findPlatform() {
    //     moveSpeed = 0;  
    //     transform.position = new Vector3(transform.position.x, transform.position.y + 9.0f, 0);
    //     playerRigidbody.gravityScale = 0;
    //     while (playerRigidbody.gravityScale == 0) {
    //         Vector2 position = transform.position;
    //         Vector2 direction = transform.TransformDirection(Vector2.down);
    //         RaycastHit2D hit = Physics2D.Raycast(position, direction, 1000f);
    //         if (hit.collider.tag == "Platform") {
    //             playerRigidbody.gravityScale = GRAVITY_FLOAT;
    //         } else {
    //             playerRigidbody.position = new Vector2(playerRigidbody.position.x + 0.1f, playerRigidbody.position.y);            
    //         }
    //     }
    // }
	
	void Update ()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);

        playerRigidbody.velocity = new Vector2(playerSpeed, playerRigidbody.velocity.y);

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
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

        if(Input.GetKeyUp(KeyCode.V))
        {
            SetPlayerSpeed(0);
        }

        // myAnimator.SetFloat("Speed", playerRigidbody.velocity.x);
        // myAnimator.SetBool("Grounded", grounded);
	}

    void OnCollisionEnter2D(Collision2D other)
    { 
        if(other.gameObject.tag == "killbox") {
            Debug.Log("player has fallen");
            fallen = true;

            // canDoubleJump = true;
            // PLATFORM_GENERATOR.paused = true; 
            // FADE.SetActive(true);
            // StartCoroutine(ExecuteAfterTime(5f));
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
        PLATFORM_GENERATOR.paused = false; 
        FADE.SetActive(false);

    }

    public void DecrementRemainingLives() {
        remainingLives--;
    }


    // GETTERS
    // --------------------------------------------------------------
    public int GetRemainingLives() {return remainingLives;}
    public bool HasFallen() {return fallen;}
    public bool IsDead() {return remainingLives < 1;}

    // SETTERS
    // --------------------------------------------------------------
    public void SetHasFallen(bool value) {fallen = value;}
    public void SetPlayerSpeed(int speed) {playerSpeed = speed;}
    public void SetPlayerPosition(Vector3 position) {this.transform.position = position;}

}
