using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 20f; 
    public float maxJumpValue = 10f; 
    public float jumpSpeed = 0.01f;
    public LayerMask whatIsGround;
    public float groundedRadius;
    public int remainingLives = 3; 

    private Rigidbody2D rigidbody;
    private Transform groundCheck;
    private Manager Manager; 

    private bool grounded;
    private int consecutiveJumps = 0;

    void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        groundCheck = GameObject.Find("Player/GroundCheck").GetComponent<Transform>();
        Manager = GameObject.Find("Manager").GetComponent<Manager>();
    }

	void Update() {
        // Check for collision with ground. 
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
        // If the player touches the ground, reset their ability to jump.
        if (grounded) {
            consecutiveJumps = 0; 
        }
   
        if(Input.GetKeyDown(KeyCode.Space) && consecutiveJumps < 2 && !Manager.GetPaused()) {
            consecutiveJumps++; 

            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0); 
            
            while (rigidbody.velocity.y < maxJumpValue) {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y + jumpSpeed);
            }
        }

        // Stop moving upwards on key jumps: lets player make shorter jumps. 
        if(Input.GetKeyUp(KeyCode.Space)) {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0); 
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    { 
        if(other.gameObject.tag == "killbox") {
            remainingLives--;
            if (remainingLives > 0) {
                Manager.HandleFall(); 
            } else {
                Manager.HandleDeath(); 
            }
        }
    }

    public void SetPlayerPosition(Vector3 position) {this.transform.position = position;}

    public Vector3 GetPlayerTransform() {return this.transform.position;}

}
