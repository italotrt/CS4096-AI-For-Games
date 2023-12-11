using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    CharacterController controller; // Reference to the CharacterController component

    // Vectors to represent movement in the forward, strafe, and vertical directions
    Vector3 forward;
    Vector3 strafe;
    Vector3 vertical;

    // Movement speeds
    float forwardSpeed = 5f;
    float strafeSpeed = 5f;

    // Variables for gravity and jumping
    float gravity;
    float jumpSpeed;
    float maxJumpHeight = 2f;
    float timeToMaxHeight = 0.5f;

    // Player health and goal achievement status
    public int playerHealth;
    public bool goalAchieved = false;

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Get the CharacterController component attached to the GameObject

        // Calculate gravity and jump speed based on max jump height and time to reach max height
        gravity = (-2 * maxJumpHeight) / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed = (2 * maxJumpHeight) / timeToMaxHeight;

        playerHealth = 100; // Set the initial player health
    }

    void Update()
    {
        // Get input for forward and strafe movement
        float forwardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");

        // Calculate movement vectors based on input and speed
        forward = forwardInput * forwardSpeed * transform.forward;
        strafe = strafeInput * strafeSpeed * transform.right;

        // Combine movement vectors to get the final velocity
        Vector3 finalvelocity = forward + strafe + vertical;
        // Move the character controller based on the final velocity and deltaTime
        controller.Move(finalvelocity * Time.deltaTime);
        // Apply gravity to the vertical movement
        vertical += gravity * Time.deltaTime * Vector3.up;

        // Check if the character is grounded, reset vertical movement if grounded
        if(controller.isGrounded) {
            vertical = Vector3.down;
        }

        // Check for jump input and perform the jump if grounded
        if(Input.GetKeyDown(KeyCode.Space) && controller.isGrounded) {
            vertical = jumpSpeed * Vector3.up;
        }

        // Prevent upward movement if the player hits a ceiling
        if (vertical.y > 0 && (controller.collisionFlags & CollisionFlags.Above) != 0) {
            vertical = Vector3.zero;
        }

        // Check if player health is zero or below, restart the game if true
        if (playerHealth <= 0) {
            Debug.Log("Restartig the Game");
            SceneManager.LoadScene(0);
        }
    }

    void OnTriggerEnter(Collider other) {
        // If the player collides with the goal object
        // it deletes the object and sets the goalAchieved variable to true
        if(other.tag == "Finish") {
            goalAchieved = true;
            Destroy(other.gameObject);
            Debug.Log("Goal achieved, now escape");
        }

        // If the player has collected the goal object
        // and it collides with the Exit object
        // it restarts the game
        if(other.tag == "Respawn" && goalAchieved) {
            Debug.Log("Game completed");
            SceneManager.LoadScene(0);
        }
    }
}