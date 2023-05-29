using System;
using System.Collections;
using System.Collections.Generic;
using SimpleInputNamespace;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    #region Varibales

    public GameObject player;
    public bool canUseAbility;
    //-------------------------------------------------------------
    private TestCharacterController _controller;
    
    float temp = 0;// storage variable
    //-------------------------------------------------------------
    private bool speedAbilityActive = false;
    //-------------------------------------------------------------
    [Tooltip("current vehicle ability active")] 
    public enum AbilityType
    {
        Normal,
        SpeedBoost,
        Armour,
        DoubleCoins,
        Escapist
    }

    public AbilityType abilityType;
    //-------------------------------------------------------------

    #endregion
    //-------------------------------------------------------------
    #region Start and Update

    private void Start()
    {
        _controller= player.GetComponent<TestCharacterController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && canUseAbility)
        {
            canUseAbility = false;
            ActivateAbilty();
        }
        
    }

    #endregion
    //-------------------------------------------------------------
    #region AbilityHandler

    void ActivateAbilty()
    {
        int testFlag = 0;
        switch (abilityType)
        {
            case AbilityType.Normal:
                print("Normal Vehicle");
                Debug.Log("Normal Vehicle");
                break;
            case AbilityType.SpeedBoost:
                Debug.Log("Speedster Vehicle");
                SpeedAbility();
                break;
            case AbilityType.Armour:
                Debug.Log("Tank Vehicle");
                break;
            case AbilityType.DoubleCoins:
                Debug.Log("Double Money Vehicle");
                break;
            case AbilityType.Escapist:
                Debug.Log("Escape Artist Vehicle");
                break;
    
        }
    }

    #endregion
    //-------------------------------------------------------------
    #region AbilityFunctions

    #region SpeedAbility
    // Provide speed Boost | Enable Rockets GameObject | Disable Collider
    void SpeedAbility()
    {
        float speedBoost = _controller.maxMovementSpeed;//set speedboost to the maximum capable speed
        temp = _controller.movementSpeed;// Store current Movement speed to set back to when ability ends
        //player.GetComponent<Collider>().enabled = false; //deactive collider to ignore all obstacles
        speedAbilityActive = true;
        Physics.IgnoreCollision(tag[7], GetComponent<Collider>(),
        _controller.movementSpeed = speedBoost; //set current speed to speedboost
        Invoke("EndSpeedAbility",7);
    }
/// <summary>
/// Instead of disbaling the collider we should tell it to ignore certain layers
/// Right now we can not pick up anyway coins or spawn the next block
/// </summary>
    void EndSpeedAbility()
    {
        Debug.Log("End Speed Ability");
        _controller.movementSpeed = temp;
        speedAbilityActive = false;
        //player.GetComponent<Collider>().enabled = true;
    }
    #endregion

    #endregion
    //-------------------------------------------------------------
    
    #region CollsionControl

    /*private void OnCollisionEnter(Collision collision)
    {
        if (speedAbilityActive)// if active ignore layers called Obstacles
        {
            if (collision.gameObject.layer == 6)
            {
                Debug.Log("Obstacles Hit");
                GameObject temp = collision.gameObject;
                Debug.Log(temp.name + " Collided with " + gameObject.name);
                Physics.IgnoreCollision(temp.transform.GetComponent<Collider>(), GetComponent<Collider>(), true);
            }
        }
        if (!speedAbilityActive)// if active ignore layers called Obstacles
        {
            if (collision.gameObject.layer == 6)
            {
                Debug.Log("Obstacles Hit");
                GameObject temp = collision.gameObject;
                Debug.Log(temp.name + " Collided with " + gameObject.name);
                Physics.IgnoreCollision(temp.transform.GetComponent<Collider>(), GetComponent<Collider>(), false);
            }
        }
        
        
    }*/

    #endregion
    
}
