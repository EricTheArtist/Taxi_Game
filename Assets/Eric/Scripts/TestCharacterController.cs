using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace SimpleInputNamespace
{
    public class TestCharacterController : MonoBehaviour
    {
        public SpawnManager spawnManager;

        public bool SteeringWheel;
        public bool game_over;
        public float movementSpeed = 10f; // speed of forward movement and of horizontal movement when using steering wheel
        public float maxMovementSpeed = 40f;
        public float MovementIncrement = 4;
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
        //private Vector2 fingerUpPosition;
        //private float swipeDistance;
        private bool allowSwipe = true;

        public ParticleSystem DriftingSmokeL;
        public ParticleSystem DriftingSmokeR;

        public ParticleSystem SteeringDriftingSmokeL;
        public ParticleSystem SteeringDriftingSmokeR;

        public ParticleSystem Speedlines;


        [Header("Breaking")]
        private int brakesAmount = 100;
        public float _movementSpeed; //is used by lane spawner if the taxi is busy breaking
        public bool isBraking = false;
        float BreakingSpeed = 3f; // used for lerping the break
        float LerpOfBreak;
        bool CanLerp = false; // used to enable the lerp once the break is pressed
        bool breakfinished = true; // set true when the player releases the break button
        public Image image_brakepad;
        public GameObject breakButton;
        public TMP_Text BreakInstruction;

        public Animator animator;
        public GameObject SteeringCar;
        public GameObject SwipingCarBody;
        public GameObject SwipingCarWF;
        public GameObject SwipingCarWB;
        public GameObject SwipingShadow;


        private void Start()
        {
            animator = GetComponent<Animator>();
            breakButton.SetActive(false);

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

                SteeringCar.SetActive(true);
                SwipingCarBody.SetActive(false);
                SwipingCarWB.SetActive(false);
                SwipingCarWF.SetActive(false);
                SwipingShadow.SetActive(false);
            }
            else
            {
                SteeringwheelUI.SetActive(false);
                SteeringWheel = false;
                SteeringCar.SetActive(false);
                SwipingCarBody.SetActive(true);
                SwipingCarWB.SetActive(true);
                SwipingCarWF.SetActive(true);
                SwipingShadow.SetActive(true);
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

            //LerpBreaking(); // checks for lerping if the break has been pressed 

        }

        void IncrementSpeed()
        {
            if (!game_over && (movementSpeed < maxMovementSpeed) && isBraking == false)
            {
                
                
                    movementSpeed += Time.deltaTime/MovementIncrement;
                    //Debug.Log("Speed: " + movementSpeed);
                    

            }
            if(movementSpeed > (maxMovementSpeed / 2))
            {
                Speedlines.Play();
            }
            if(movementSpeed < (maxMovementSpeed / 2))
            {
                Speedlines.Stop();
            }

        }

        void SteeringWheelUpdate()
        {
            //Code for steering wheel
            if (SteeringWheel == true)
            {
                if (transform.position.x > 4.2f && SW.Angle >= 0) //the 4.2 is the cordinqate position of the edge of the road
                {
                    hMovement = 0;
                    SteeringCar.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else if (transform.position.x < -4.2f && SW.Angle <= 0)
                {
                    hMovement = 0;
                    SteeringCar.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else if(SW.Angle!=0)
                {
                    hMovement = SW.Angle * movementSpeed/150; 
                    //Debug.Log("Steering angle " + SW.Angle.ToString());
                    SteeringCar.transform.localEulerAngles = new Vector3(0, -SW.Angle/3, 0);
                    if (SW.Angle > 0)
                    {
                        SteeringDriftingSmokeR.Pause();
                        SteeringDriftingSmokeL.Play();
                    }
                    else
                    {

                        SteeringDriftingSmokeR.Play();
                        SteeringDriftingSmokeL.Pause();
                    }


                }
                else if(SW.Angle == 0)
                {
                    hMovement = 0;
                }
            }
        }

        void SwipingUpdate()
        {

            // Check for touch input
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Check for finger down
                if (touch.phase == TouchPhase.Began)
                {
                    fingerDownPosition = touch.position;
                }
                    
                
                // Calculate swipe distance
                float swipeDistance = touch.position.x - fingerDownPosition.x;

                // Check for swipe
                if (Mathf.Abs(swipeDistance) > swipeThreshold && allowSwipe == true)
                {
                    // Swipe detected, handle it here
                    if (swipeDistance > 0 && LerpSwipe == false)
                    {
                        if (MoveFromLane < 4) //checks to make sure there is a lane to the right and that you are not busy moving lanes
                        {
                            MoveToLane = MoveFromLane + 1;
                            LerpSwipe = true;
                            animator.SetTrigger("isSwervingRight 0");
                            
                            DriftingSmokeR.Play();

                        }
                        //Debug.Log("Swipe Right");
                    }
                    else if (MoveFromLane > 0 && LerpSwipe == false)
                    {
                        MoveToLane = MoveFromLane - 1;
                        LerpSwipe = true;
                        animator.SetTrigger("isSwervingLeft 0");
                        //Debug.Log("Swiping Left");
                        DriftingSmokeL.Play();
                        

                    }
                    //Debug.Log("Swipe Distance: " + swipeDistance.ToString());
                    allowSwipe = false;
                }
                // Check for finger up
                if (touch.phase == TouchPhase.Ended)
                {
                    allowSwipe = true;

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
                    //transform.RotateAround(transform.position, Vector3.up, SW.Angle/10 * Time.deltaTime);


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
                //Debug.Log("Swiped Right");
                MoveToLane = MoveFromLane + 1;
                LerpSwipe = true;
                DriftingSmokeL.Play();
                


            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && MoveFromLane > 0 && LerpSwipe == false)
            {
                //Debug.Log("Swiped Left");
                MoveToLane = MoveFromLane - 1;
                LerpSwipe = true;
                DriftingSmokeR.Play();

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
                //play animation
            }

        }

        public void BreakPadDown() //called on button press
        {

            isBraking = true;
            _movementSpeed = 0f;
            _movementSpeed = movementSpeed; // saves the players speed
            movementSpeed = 0;

        }

        public void BreakPadUp() //called on button release
        {


            breakButton.SetActive(false); //break pad is re-enabled in the Pickup system
            resumedriving();

        }

        void resumedriving()
        {

            movementSpeed = _movementSpeed;
            isBraking = false;
         
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
                    if(breakfinished == true) // if the player is no longer holding down the break button
                    {
                        resumedriving();
                    }
                }
            }
        }





    }
}
