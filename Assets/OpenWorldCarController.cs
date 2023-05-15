using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleInputNamespace
{
    public class OpenWorldCarController : MonoBehaviour
    {
        public float moveSpeed = 10f; // The speed at which the object will move
        private bool isMoving = false; // A flag to keep track of whether the object is moving

        public float rotationSpeed = 0.1f; // The speed at which the object will rotate
        private float arrowInput; // The input value from GetAxis()
        public SteeringWheel SWOW;



        void Start()
        {

        }
        void Update()
        {
            arrowInput = SimpleInput.GetAxis("Vertical"); // Get the horizontal input value from the player



            if (Input.GetKeyDown(KeyCode.W)||arrowInput == 1) // Check if the player pressed the 'w' key
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

                arrowInput = SimpleInput.GetAxis("Vertical"); // Get the horizontal input value from the player
                transform.Rotate(Vector3.up * (SWOW.Angle/100) * rotationSpeed); // Rotate the object around its up axis

            }



        }

        public void ButtonForward()
        {
            isMoving = true;
            Debug.Log("Forward");
        }

        public void ButtonForwardUp()
        {
            isMoving = false;
        }
    }
}
