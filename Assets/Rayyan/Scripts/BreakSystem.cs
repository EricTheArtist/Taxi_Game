using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakSystem : MonoBehaviour
{

    public GameObject brakePads;
    
    private int brakesAmount = 3;
    private Image brakeImage;
    float _movementSpeed;

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
        ManageBrakeSystem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pothole")
        {
            ReduceBrakes();
            //play animation
        }
    }

    void ManageBrakeSystem()
    {
        if (Input.GetKeyDown(KeyCode.B) && brakesAmount>0)
        {
            //Debug.Log("You Are Braking");
            _movementSpeed = 0f;
            _movementSpeed = controller.movementSpeed;
            controller.movementSpeed = 0f;
            ReduceBrakes();        
        }
        if (Input.GetKeyUp(KeyCode.B) && brakesAmount>=0)
        {
            //Debug.Log("You Are Not Braking");
            controller.movementSpeed = _movementSpeed;
        }
    }
    void ManageBrakePads()
    {
        if (brakesAmount == 3)
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
            brakeImage.gameObject.SetActive(false);
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
