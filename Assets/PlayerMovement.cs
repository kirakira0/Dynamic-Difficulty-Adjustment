using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // CONSTANTS
    // --------------------------------------------------------------
    public float NORMAL_MOVE_SPEED = 6;
    public int TOTAL_LIVES = 3;

    // CHANGING VARIABLES
    // --------------------------------------------------------------
    private float playerSpeed = 0;
    private int remainingLives;
    private bool fallen;
    private bool paused = true;

        //------------------------------

    public Transform PLAYER_SPAWN_POINT;
    public Transform STARTING_PLATFORM_SPAWN_POINT; 
    public GameObject STARTING_PLATFORM; 

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

        playerRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        hasJumped = false;
    }

	void Update ()
    {
        if (paused && Input.GetKeyDown(KeyCode.V)) {
            paused = false;
        }

        if (paused) {
            playerSpeed = 0;
        } else {
            playerSpeed = NORMAL_MOVE_SPEED;
        }

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

        // IS DEAD
        if (IsDead()) {
            Debug.Log("Player is dead.");
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Additive);
            remainingLives = 3; 
        }

        // myAnimator.SetFloat("Speed", playerRigidbody.velocity.x);
        // myAnimator.SetBool("Grounded", grounded);
	}

    void OnCollisionEnter2D(Collision2D other)
    { 
        if(other.gameObject.tag == "killbox") {
            Debug.Log("Player has fallen.");
            fallen = true;
        }
    }

    // IEnumerator ExecuteAfterTime(float time)
    // {
    //     yield return new WaitForSeconds(time);
    //     Instantiate(STARTING_PLATFORM, new Vector3(STARTING_PLATFORM_SPAWN_POINT.transform.position.x, MID_Y_POS, 0), Quaternion.identity);
    //     transform.position = new Vector3(PLAYER_SPAWN_POINT.transform.position.x, MID_Y_POS + 2, 0);
    // }

    public void DecrementRemainingLives() {
        remainingLives--;
    }


    // GETTERS
    // --------------------------------------------------------------
    public int GetRemainingLives() {return remainingLives;}
    public bool HasFallen() {return fallen;}
    public bool IsDead() {return remainingLives < 1;}
    public bool IsPaused() {return paused;}

    // SETTERS
    // --------------------------------------------------------------
    public void SetHasFallen(bool value) {fallen = value;}
    public void SetPlayerSpeed(int speed) {playerSpeed = speed;}
    public void SetPlayerPosition(Vector3 position) {this.transform.position = position;}
    public void SetPaused(bool v) {paused = v;}

}
