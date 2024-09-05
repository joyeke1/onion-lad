using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    //good practice to make variables private, it helps to make the inspector cleaner
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed; // Speed at which the player moves
    [SerializeField] private float jumpForce;// Force applied when the player jumps
    public float xInput;// Horizontal input value
    public float yInput; // Vertical input value (currently unused)

    [Header("Collision check")]
    [SerializeField] private float groundCheckRadius;  // Radius for ground check detection
    [SerializeField] private Transform groundCheck; // Transform object for ground check position
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;// Boolean to store if the player is on the ground


    // Start is called before the first frame update
    // called only once at the start of the game
    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
    
    }

    // Update is called once per frame
    //called all the time, for ex: a 60 fps game, update will be called 60 times per second
    private void Update()
    {
        // Perform collision checks, including ground detection
        CollisionChecks();
        // Get horizontal input value (left/right arrow keys or A/D keys)
        xInput = Input.GetAxisRaw("Horizontal");
        //yInput = Input.GetAxisRaw("Vertical");
      
        Movement(); //put "variables" to make it look cleaner

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }
    // Perform collision checks, including ground detection
    private void CollisionChecks()
    {
        // Check if the player is touching the ground
        // Physics2D.OverlapCircle checks if there are any colliders within the circle defined by groundCheck.position and groundCheckRadius
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    // Apply a vertical force to make the player jump
    private void Jump()
    {
        if(isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
           
    }
    // Move the player based on horizontal input
    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }
    // Draw a wireframe sphere in the scene view for the ground check radius
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
// unity hotkeys, option + up/down arrow moves the line up or down
// command + r changes the variable for all variables so you don't have to type multiple times