using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldCarController : MonoBehaviour
{
    public float moveSpeed = 10f; // The speed at which the object will move
    private bool isMoving = false; // A flag to keep track of whether the object is moving

    public float rotationSpeed = 0.1f; // The speed at which the object will rotate
    private float rotateInput; // The input value from GetAxis()


    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) // Check if the player pressed the 'w' key
        {
            isMoving = true; // Set the flag to true to indicate that the object is moving
        }

        if (Input.GetKeyUp(KeyCode.W)) // Check if the player released the 'w' key
        {
            isMoving = false; // Set the flag to false to indicate that the object is no longer moving
        }
    }
    void FixedUpdate()
    {

            if (isMoving) // Check if the object is moving
            {
                Vector3 movement = transform.forward * moveSpeed; // Calculate the movement vector in the object's forward direction
                movement *= Time.fixedDeltaTime; // Scale the movement by the fixed time step
                transform.position += movement; // Move the object in the calculated direction

                rotateInput = SimpleInput.GetAxis("Horizontal"); // Get the horizontal input value from the player
                transform.Rotate(Vector3.up * rotateInput * rotationSpeed); // Rotate the object around its up axis

            }
        
            

    }


}
