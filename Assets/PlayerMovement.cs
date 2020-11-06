using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float horizontalMovementSpeed = 5f;
    public LayerMask platformLayer;
    
    private Rigidbody2D playerRigidbody;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();        
    }
    
    void Update() {        
        Vector2 position = transform.position;
        Vector2 direction = transform.TransformDirection(Vector2.down);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, 1000f);
        if (hit.collider.tag == "Platform") {
            Debug.Log("PLATFORM AVAILIBLE");
        } else {
            Debug.Log("no hit");
            Debug.Log(playerRigidbody.position);
            playerRigidbody.position = new Vector2(playerRigidbody.position.x + 0.05f, playerRigidbody.position.y);
            
        }
    }
    
    void FixedUpdate()
    {
        // Player jumps with Z or Up Arrow key.
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
        } 
        // Horizontal movement controlled with left and right arrow keys.
        if (Input.GetKey(KeyCode.RightArrow)) {
            playerRigidbody.velocity = new Vector2 (horizontalMovementSpeed, playerRigidbody.velocity.y);          
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            playerRigidbody.velocity = new Vector2 (-1 * horizontalMovementSpeed, playerRigidbody.velocity.y);          
        } else {
            playerRigidbody.velocity = new Vector2 (0, playerRigidbody.velocity.y);          
        }
        
    }

    /**
     * If the player falls off of a platform, use raycasting to determine where they
     * should be dropped into the scene.
     */
    private void DropPlayer() {

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
