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
        public bool game_over;
        public float movementSpeed = 10f; // speed of forward movement and of horizontal movement when using steering wheel
        public float maxMovementSpeed = 40f;
        [Header("Steering Wheel")]
        public SteeringWheel SW;
        private float hMovement; //horixontal movement, the speed at which the x value of the taxi is shifted 
        public GameObject SteeringwheelUI;

        [Header("Swiping")]
        public float[] LanesX; //X value of each lane stored in an array
        public int MoveToLane = 1; //array index
        public int MoveFromLane = 2; //arry index
        public float LaneTransitionSpeed = 4f; //fraction of 1 second (so if the LaneTransitionSpeed = 4 then the time to switch lanes will be 0.25s)
        float LaneLep; // ratio of 0-1 of position between each lane when switching lanes
        public float hPosition = 0;  //horizontal position, this is used as the X value of the taxi's position 
        public float swipeThreshold = 20f;
        private bool LerpSwipe = false;
        private Vector2 fingerDownPosition;
        private Vector2 fingerUpPosition;

        [Header("Breaking")]
        private int brakesAmount = 100;
        public float _movementSpeed; //is used by lane spawner if the taxi is busy breaking
        public bool isBraking = false;
        public float BreakingSpeed = 3f; // used for lerping the break
        float LerpOfBreak;
        bool CanLerp = false;

        private Animator animator;


        private void Start()
        {
            animator = GetComponent<Animator>();

            game_over = false;
            if(SteeringWheel == false)
            {
                SteeringwheelUI.SetActive(false);
            } 
        }

        public void ToggleSteeringWheel()
        {
            if(SteeringWheel == false)
            {
                SteeringwheelUI.SetActive(true);
                SteeringWheel = true;
                
            }
            else
            {
                SteeringwheelUI.SetActive(false);
                SteeringWheel = false;
                
            }

        }
        void Update()
        {
            MovementTest(); // testing input for PC (Remove later)

            //Code for steering wheel
            SteeringWheelUpdate();

            //Code for swiping controls 
            SwipingUpdate();

            //Code For forward and side to side movement
            MovementUpdate();    

            //code for interpolating when swiping side to side
            InterpolateLanes();

            //increases the movement speed over time
            IncrementSpeed();


            ManageBrakeSystem(); // temporary system for Input of testing breaks on PC 

            LerpBreaking(); // checks for lerping if the break has been pressed 

        }

        void IncrementSpeed()
        {
            if (!game_over && movementSpeed < maxMovementSpeed)
            {
                
                
                    movementSpeed += Time.deltaTime/4;
                    //Debug.Log("Speed: " + movementSpeed);
                

            }

        }

        void SteeringWheelUpdate()
        {
            //Code for steering wheel
            if (SteeringWheel == true)
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
        }

        void SwipingUpdate()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Check for finger down
                if (touch.phase == TouchPhase.Began)
                {
                    fingerDownPosition = touch.position;
                }

                // Check for finger up this means this logic 
                if (touch.phase == TouchPhase.Ended)
                {
                    fingerUpPosition = touch.position;

                    // Check for swipe
                    if (Mathf.Abs(fingerUpPosition.x - fingerDownPosition.x) > swipeThreshold)
                    {
                        // Swipe detected, handle it here
                        if (fingerUpPosition.x - fingerDownPosition.x > 0 && LerpSwipe == false)
                        {
                            //Debug.Log("Swiped Right");
                            if (MoveFromLane < 4) //checks to make sure there is a lane to the right and that you are not busy moving lanes
                            {
                                MoveToLane = MoveFromLane + 1;
                                LerpSwipe = true;
                                animator.SetTrigger("isSwervingRight 0");
                                
                            }

                        }
                        else if (MoveFromLane > 0 && LerpSwipe == false) //checks to make sure there is a lane to the left and that you are not busy moving lanes
                        {
                            //Debug.Log("Swiped Left");
                            MoveToLane = MoveFromLane - 1;
                            LerpSwipe = true;
                            animator.SetTrigger("isSwervingLeft 0");
                            
                        }
                    }
                }
            }
        }

        void MovementUpdate() //updates X and Z position based on if steeringwheel or swiping is being used
        {
            if (!game_over)
            {
                // set The forward movement
                float vMovement = 1 * movementSpeed;
                if (SteeringWheel == true)
                {
                    //updates the position of the car with steering wheel included
                    transform.Translate(new Vector3(hMovement, 0, vMovement) * Time.deltaTime);
                }
                else
                {
                    //updates the position of the car with swiping
                    transform.Translate(new Vector3(0, 0, vMovement) * Time.deltaTime);
                    this.transform.position = new Vector3(hPosition, 0.5f, transform.position.z);
                }

            }
        }

        void InterpolateLanes() // called in update
        {
            if(LerpSwipe == true) // this is set true when a succesfull swipe is dectected in SwipingUpdate
            {
                LaneLep += LaneTransitionSpeed * Time.deltaTime;
                hPosition = Mathf.Lerp(LanesX[MoveFromLane], LanesX[MoveToLane], LaneLep);
                if (LaneLep > 1f)
                {
                    LerpSwipe = false;
                    LaneLep = 0;
                    SwitchLanes(); // completes the lane switching by assigning the lane that the player moved into as their new current lane
                }

            }
        }


        void SwitchLanes() //called once when the lerping of lanes is finished
        {
            //Debug.Log("Lane switch complete");
            hPosition = LanesX[MoveToLane];
            MoveFromLane = MoveToLane;
        }

        void MovementTest() //input for tseting lane switching on PC
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && MoveFromLane < 4 && LerpSwipe == false)
            {
                Debug.Log("Swiped Right");
                MoveToLane = MoveFromLane + 1;
                LerpSwipe = true;

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && MoveFromLane > 0 && LerpSwipe == false)
            {
                Debug.Log("Swiped Left");
                MoveToLane = MoveFromLane - 1;
                LerpSwipe = true;
            }
        }

        private void OnTriggerEnter(Collider other) //calls the spawn manger that moves road segments from behind the player to the front of the stack
        {
            if(other.tag=="Module Collision")
            {
                spawnManager.SpawnTriggerEnter();
            }
            if (other.gameObject.tag == "Pothole")
            {
                ReduceBrakes();
                //play animation
            }

        }

        public void BreakPadDown() //called on button press
        {
            if (brakesAmount > 0) //check that the player has not used all their breaks
            {
                isBraking = true;
                _movementSpeed = 0f;
                _movementSpeed = movementSpeed;
                game_over = true; // prevents movment from being incremented
                CanLerp = true; // will trigger the lerp of the break that is being checkd for in update
                                //controller.movementSpeed = 0f;
                ReduceBrakes();
            }

        }

        public void BreakPadUp() //called on button release
        {
            CanLerp = false;
            isBraking = false;
            game_over = false;
            movementSpeed = _movementSpeed;
        }

        void ReduceBrakes()
        {
            int brakeDamage = 1;
            if (brakesAmount >= 0)
            {
                brakesAmount -= brakeDamage;
                //ManageBrakePads(); this function was used to change the UI colour of the break pad
            }
            else
            {
                brakesAmount = 0;
            }
        }

        void IncreaseBrake()
        {
            int restoreDamage = 1;
            if (brakesAmount <= 3)
            {
                brakesAmount += restoreDamage;
                //ManageBrakePads();
            }

        }

        void LerpBreaking()
        {
            if (CanLerp == true)
            {
                LerpOfBreak += BreakingSpeed * Time.deltaTime;
                movementSpeed = Mathf.Lerp(_movementSpeed, 0, LerpOfBreak);

                if (LerpOfBreak > 1)
                {
                    CanLerp = false;
                    LerpOfBreak = 0;
                }
            }
        }

        void ManageBrakeSystem() // temporary system
        {
            if (Input.GetKeyDown(KeyCode.B) && brakesAmount > 0)
            {
                //Debug.Log("You Are Braking");
                BreakPadDown();
            }
            if (Input.GetKeyUp(KeyCode.B) && brakesAmount >= 0)
            {
                //Debug.Log("You Are Not Braking");
                BreakPadUp();
            }
        }


    }
}
