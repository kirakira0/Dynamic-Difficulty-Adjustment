using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    bool isGrounded = false; 
    public Transform isGroundedChecker; 
    public float checkGroundRadius; 
    public LayerMask groundLayer;
    public float fallMultiplier = 2.5f; 
    public float lowJumpMultiplier = 2f;
    public float rememberGroundedFor; 
    float lastTimeGrounded;
    public int defaultAdditionalJumps = 1; 
    int additionalJumps;

    private Rigidbody2D rb;
    private Transform groundCheck;
    private Manager Manager; 

    public int remainingLives = 10; 

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GameObject.Find("Player/GroundCheck").GetComponent<Transform>();
        Manager = GameObject.Find("Manager").GetComponent<Manager>();
    }

    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || additionalJumps > 0)) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            additionalJumps--;
        }
    }

    void BetterJump() {
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }   
    }

    void CheckIfGrounded() {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (colliders != null) {
            isGrounded = true;
            additionalJumps = defaultAdditionalJumps;
        } else {
            if (isGrounded) {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

	void Update() {
        Jump();
        BetterJump();
        CheckIfGrounded();
    }

    void OnCollisionEnter2D(Collision2D other)
    { 
        if(other.gameObject.tag == "killbox") {
            remainingLives--;
            if (remainingLives > 0) {
                Manager.HandleFall(); 
            } else {
                // Manager.HandleDeath(); 
            }
        }
    }

    public void SetPlayerPosition(Vector3 position) {this.transform.position = position;}

    public void SetPlayerVelocity(Vector3 velocity) {this.rb.velocity = velocity;}

    public Vector3 GetPlayerTransform() {return this.transform.position;}

    public int GetLives() {return this.remainingLives;}


}
