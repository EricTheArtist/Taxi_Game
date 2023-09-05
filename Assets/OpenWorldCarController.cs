using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SimpleInputNamespace
{
    public class OpenWorldCarController : MonoBehaviour
    {
        public float acceleration = 0.5f;
        public float breaking = 0.2f;
        float maxSpeed = 15f;
        float maxReverse = 3f;
        float moveSpeed = 0; // The speed at which the object will move
        private bool isMovingforward = false; // A flag to keep track of whether the object is moving
        private bool isMovingbackward = false;

        public float rotationSpeed = 0.1f; // The speed at which the object will rotate
        //private float arrowInput; // The input value from GetAxis()
        public SteeringWheel SWOW;

        public GameObject WheelR;
        public GameObject WheelL;

        public ParticleSystem DriftingSmokeR;
        public ParticleSystem DriftingSmokeL;
        bool SmokeR;
        bool SmokeL;





        void Update()
        {

            DriftingSmokeControl();

            if (Input.GetKeyDown(KeyCode.W)) // Check if the player pressed the 'w' key
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
            WheelL.transform.localEulerAngles = new Vector3(0, 180+(SWOW.Angle/5), WheelL.transform.localRotation.eulerAngles.z);
            WheelR.transform.localEulerAngles = new Vector3(0, 0+ (SWOW.Angle/5), WheelR.transform.localRotation.eulerAngles.z);


            if (isMovingforward) // Check input for forward
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
            else if (isMovingbackward) // check input for reverse
            {
                if (moveSpeed > -maxReverse)
                {
                    moveSpeed = moveSpeed - (Time.deltaTime + breaking);
                }
                else
                {
                    moveSpeed = -maxReverse;
                }

            }
            else
            {
                if(moveSpeed > 1)
                {
                    moveSpeed = moveSpeed - (Time.deltaTime + acceleration);

                }
                else if (moveSpeed < -1)
                {
                    moveSpeed = moveSpeed + (Time.deltaTime + acceleration);

                }
                else
                {
                    moveSpeed = 0;
                }
                
            }

        }



        void DriftingSmokeControl()
        {
            if (SWOW.Angle > 100 && SmokeR == false && isMovingforward == true)
            {
                DriftingSmokeR.Play();
                DriftingSmokeL.Stop();
                SmokeR = true;
                SmokeL = false;
            }
            if (SWOW.Angle < -100 && SmokeL == false && isMovingforward == true)
            {
                DriftingSmokeL.Play();
                DriftingSmokeR.Stop();
                SmokeR = false;
                SmokeL = true;
            }
            if (SWOW.Angle < 100 && SWOW.Angle > -100)
            {
                DriftingSmokeR.Stop();
                DriftingSmokeL.Stop();
                SmokeL = false;
                SmokeR = false;
            }
        }

        public void ButtonForward()
        {
            isMovingforward = true;
            //Debug.Log("Forward");
        }

        public void ButtonForwardUp()
        {
            isMovingforward = false;
        }

        public void ButtonBackwards()
        {
            isMovingbackward = true;
        }

        public void ButtonBackwardsUp()
        {
            isMovingbackward = false;
        }
    }
}
