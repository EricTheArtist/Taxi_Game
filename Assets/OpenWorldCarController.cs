using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleInputNamespace
{
    public class OpenWorldCarController : MonoBehaviour
    {
        public float acceleration = 0.5f;
        float maxSpeed = 10f;
        float maxReverse = 3f;
        float moveSpeed = 1f; // The speed at which the object will move
        private bool isMovingforward = false; // A flag to keep track of whether the object is moving
        private bool isMovingbackward = false;

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
                isMovingforward = true; // Set the flag to true to indicate that the object is moving
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                isMovingbackward = true;
            }

            if (Input.GetKeyUp(KeyCode.W)) // Check if the player released the 'w' key
            {
                isMovingforward = false; // Set the flag to false to indicate that the object is no longer moving
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                isMovingbackward = false;
            }
        }
        void FixedUpdate()
        {
            if (moveSpeed != 0)
            {
                arrowInput = SimpleInput.GetAxis("Vertical"); // Get the horizontal input value from the player
                transform.Rotate(Vector3.up * (SWOW.Angle/100) * rotationSpeed); // Rotate the object around its up axis
            }
            if (isMovingforward) // Check if the object is moving
            {
                if (moveSpeed < maxSpeed)
                {
                    moveSpeed = moveSpeed + (Time.deltaTime + acceleration);
                }
                else
                {
                    moveSpeed = maxSpeed;
                }
                


            }
            else if (isMovingbackward)
            {
                if (moveSpeed > -maxReverse)
                {
                    moveSpeed = moveSpeed - (Time.deltaTime + acceleration);
                }
                else
                {
                    moveSpeed = -maxReverse;
                }
            }
            else
            {
                if(moveSpeed > 0)
                {
                    moveSpeed = moveSpeed - (Time.deltaTime + acceleration); 
                }
                else if (moveSpeed < 0)
                {
                    moveSpeed = moveSpeed + (Time.deltaTime + acceleration);
                }
                else
                {
                    moveSpeed = 0;
                }
                
            }

            Vector3 movement = transform.forward * moveSpeed; // Calculate the movement vector in the object's forward direction
            movement *= Time.fixedDeltaTime; // Scale the movement by the fixed time step
            transform.position += movement; // Move the object in the calculated direction

 


        }

        public void ButtonForward()
        {
            isMovingforward = true;
            Debug.Log("Forward");
        }

        public void ButtonForwardUp()
        {
            isMovingforward = false;
        }
    }
}
