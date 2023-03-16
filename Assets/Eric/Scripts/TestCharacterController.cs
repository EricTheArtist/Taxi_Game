using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleInputNamespace
{
    public class TestCharacterController : MonoBehaviour
    {

        public SpawnManager spawnManager;

        public bool SteeringWheel;

        [Header("Steering Wheel")]
        public float movementSpeed = 10f;
        public SteeringWheel SW;
        private float hMovement;
        public GameObject SteeringwheelUI;

        [Header("Swiping")]
        public float[] LanesX;
        public int MoveToLane = 1;
        public int MoveFromLane = 2;
        public float hPosition = 0;

        public float swipeThreshold = 20f;

        private Vector2 fingerDownPosition;
        private Vector2 fingerUpPosition;


        private void Start()
        {
            if(SteeringWheel == false)
            {
                SteeringwheelUI.SetActive(false);
            }
        }
        void Update()
        {
            //Code for steering wheel
            if (SteeringWheel== true) 
                {
                    if (transform.position.x > 4.2f && SW.Angle > 0) //the 4.2 is the cordinqate position of the edge of the road
                {
                    hMovement = 0 * movementSpeed / 100;
                }
                else if (transform.position.x < -4.2f && SW.Angle < 0)
                {
                    hMovement = 0 * movementSpeed / 100;
                }
                else
                {
                    hMovement = SW.Angle * movementSpeed / 100;
                }  
            }

            //Code for swiping controls 

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Check for finger down
                if (touch.phase == TouchPhase.Began)
                {
                    fingerDownPosition = touch.position;
                }

                // Check for finger up
                if (touch.phase == TouchPhase.Ended)
                {
                    fingerUpPosition = touch.position;

                    // Check for swipe
                    if (Mathf.Abs(fingerUpPosition.x - fingerDownPosition.x) > swipeThreshold)
                    {
                        // Swipe detected, handle it here
                        if (fingerUpPosition.x - fingerDownPosition.x > 0)
                        {
                            Debug.Log("Swiped Right");
                            if (MoveFromLane < 4)
                            {
                                MoveToLane = MoveFromLane + 1;
                                hPosition = LanesX[MoveToLane];
                                MoveFromLane = MoveToLane;
                            }
                        }
                        else if(MoveFromLane > 0)
                        {
                            Debug.Log("Swiped Left");
                            MoveToLane = MoveFromLane - 1;
                            hPosition = LanesX[MoveToLane];
                            MoveFromLane = MoveToLane;
                        }
                    }
                }
            }


            // set The forward movement
            float vMovement = 1 * movementSpeed;
            
            //updates the position of the car with steering wheel included
            transform.Translate(new Vector3(hMovement, 0, vMovement) * Time.deltaTime);

            //updates the position of the care with swiping
            this.transform.position = new Vector3(hPosition,0.5f,transform.position.z);

        }



        private void OnTriggerEnter(Collider other)
        {
            spawnManager.SpawnTriggerEnter();
        }

    }
}
