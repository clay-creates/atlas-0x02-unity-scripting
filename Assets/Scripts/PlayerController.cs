using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; // RigidBody of the Player
    private float movementX; // Movement along X Axis
    private float movementY; // Movement along Y axis
    public float speed = 20; // Speed of player
    private int score = 0; // Amount of coins that player has collected
    public int health = 5; // Player health

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get and store RigidBody component
    }

    void OnMove(InputValue movementValue) // Called when movement is detected
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); // Convert input value into Vector2 for movement
        movementX = movementVector.x; // Store X component of movement
        movementY = movementVector.y; // Store Y component of movement
    }

    private void FixedUpdate() // Called once per fixed frame-rate frame
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY); // Create a 3d movement vectore using X and Y inputs
        rb.AddForce(movement * speed); // Apply force to the Rigidbody to move the player
    }

    void Update() // Called once per frame
    {
        if (health == 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("maze");
            score = 0;
            health = 5;
        }
    }

    void OnTriggerEnter(Collider other) // Called when player colides with trigger item
    {
        

        if (other.gameObject.CompareTag("Pickup")) // Checks collision item tag (Pickup)
        {
            other.gameObject.SetActive(false); // Disables / Destroys object
            score++; // Increment score
            Debug.Log($"Score: {score}"); // Print updated score to console
        }

        if (other.gameObject.CompareTag("Trap")) // Checks for Trap tag
        {
            health--; // Decrement health
            Debug.Log($"Health: {health}"); // Print new health
        }

        if (other.gameObject.CompareTag("Goal")) // Checks for Goal tag
        {
            Debug.Log("You win!"); // Prints victory message
        }
    }
}
