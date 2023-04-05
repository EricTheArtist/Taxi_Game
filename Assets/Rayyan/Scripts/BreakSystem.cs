using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakSystem : MonoBehaviour
{

    public GameObject brakePads;
    
    private int brakesAmount = 100;
    private Image brakeImage;
    float _movementSpeed;
    public bool isBraking = false;
    // used for lerping the break
    public float BreakingSpeed = 3f;
    float LerpOfBreak;
    bool CanLerp = false;

    private SimpleInputNamespace.TestCharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        brakeImage = brakePads.gameObject.GetComponent<Image>();
        controller = GetComponent<SimpleInputNamespace.TestCharacterController>();
        ManageBrakePads();
    }

    // Update is called once per frame
    void Update()
    {
        ManageBrakeSystem(); // temporary system for Input of testing breaks on PC 

        LerpBreaking(); // checks for lerping if the break has been pressed 
    }


    void LerpBreaking()
    {
        if(CanLerp == true)
        {
            LerpOfBreak += BreakingSpeed * Time.deltaTime;
            controller.movementSpeed = Mathf.Lerp(_movementSpeed, 0, LerpOfBreak);

            if(LerpOfBreak > 1)
            {
                CanLerp = false;
                LerpOfBreak = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pothole")
        {
            ReduceBrakes();
            //play animation
        }
    }

    void ManageBrakeSystem() // temporary system
    {
        if (Input.GetKeyDown(KeyCode.B) && brakesAmount>0)
        {
            //Debug.Log("You Are Braking");
            BreakPadDown();
        }
        if (Input.GetKeyUp(KeyCode.B) && brakesAmount>=0)
        {
            //Debug.Log("You Are Not Braking");
            BreakPadUp();
        }
    }

    public void BreakPadDown() //called on button press
    {
        if (brakesAmount > 0) //check that the player has not used all their breaks
        {
            isBraking = true;
            _movementSpeed = 0f;
            _movementSpeed = controller.movementSpeed;
            controller.game_over = true; // prevents movment from being incremented
           //controller.movementSpeed = 0f;
            CanLerp = true; // will trigger the lerp of the break that is being checkd for in update
            //controller.movementSpeed = 0f;
            ReduceBrakes();
        }

    }

    public void BreakPadUp() //called on button release
    {
        CanLerp = false;
        isBraking = false;
        controller.game_over = false;
        controller.movementSpeed = _movementSpeed;
    }
    
    void ManageBrakePads() //let's replace this system with one that interpolates the colour based on the percentage of breaks left
    {
        if (brakesAmount >= 3)
        {
            brakeImage.color = Color.green;
        }
        if (brakesAmount == 2)
        {
            brakeImage.color = Color.yellow;
        }
        if (brakesAmount == 1)
        {
            brakeImage.color = Color.red;
        }
        if (brakesAmount == 0)
        {
            brakeImage.color = Color.black;
            //brakeImage.gameObject.SetActive(false);
        }
    }
    void ReduceBrakes()
    {
        int brakeDamage = 1;
        if (brakesAmount >= 0)
        {
            brakesAmount -= brakeDamage;
            ManageBrakePads();
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
            ManageBrakePads();
        }
       
    }
}
