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
    // storage variable
    private float tempFloat = 0;
    private int tempInt = 0;
    //-------------------------------------------------------------
    private bool speedAbilityActive = false;
    private bool armourAbilityActive = false;
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
    [Header("Ability Timers")]
    public float SpeedAbilityTimer;
    public float ArmourAbiltyTimer;
    public float ArmourAbilityTimer;
    //-------------------------------------------------------------
    private CurrencySystem _currencySystem;
    private TestCharacterController _controller;
    #endregion
    //-------------------------------------------------------------
    #region Start and Update

    private void Start()
    {
        _controller= player.GetComponent<TestCharacterController>();
        _currencySystem = player.GetComponent<CurrencySystem>();
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
            //--------------------------------------------------
            case AbilityType.SpeedBoost:
                Debug.Log("Speedster Vehicle");
                SpeedAbility();
                break;
            //--------------------------------------------------
            case AbilityType.Armour:
                Debug.Log("Tank Vehicle");
                ArmourAbilty();
                break;
            //--------------------------------------------------
            case AbilityType.DoubleCoins:
                Debug.Log("Double Money Vehicle");
                DoubleCoinsAbility();
                break;
            //--------------------------------------------------
            case AbilityType.Escapist:
                Debug.Log("Escape Artist Vehicle");
                break;
            //--------------------------------------------------
    
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
        tempFloat = _controller.movementSpeed;// Store current Movement speed to set back to when ability ends
        speedAbilityActive = true;
        Physics.IgnoreLayerCollision(6,3, true);//Disables collisions between player and obsticles
        _controller.movementSpeed = speedBoost; //set current speed to speedboost
        Invoke("EndSpeedAbility",SpeedAbilityTimer);
    }
    void EndSpeedAbility()
    {
        Debug.Log("End Speed Ability");
        _controller.movementSpeed = tempFloat;
        speedAbilityActive = false;
        Physics.IgnoreLayerCollision(6,3, false);
        //player.GetComponent<Collider>().enabled = true;
    }

    #endregion

    #region ArmourAbility

    // use physic layer ignore again
    void ArmourAbilty()
    {
        Debug.Log("Start Armour Ability");
        armourAbilityActive = true;
        Physics.IgnoreLayerCollision(6,3,true);
        Invoke("EndArmourAbility", ArmourAbiltyTimer);
    }

    void EndArmourAbility()
    {
        Debug.Log("End Armour Ability");
        armourAbilityActive = false;
        Physics.IgnoreLayerCollision(6,3, false);
    }

    #endregion

    #region DoubleCoinsAbility

    void DoubleCoinsAbility()
    {
        Debug.Log("Double Coins Active");
        tempInt = _currencySystem.multiplier;
        _currencySystem.multiplier = 2 * _currencySystem.multiplier;
        Invoke("EndDoubleCoinsAbility", ArmourAbilityTimer);
    }
    void EndDoubleCoinsAbility()
    {
        Debug.Log("Double Coins Inactive");
        _currencySystem.multiplier = tempInt;
    }

    #endregion
    #endregion
    //-------------------------------------------------------------

    #region Collision Control

    private void OnCollisionExit(Collision collision)
    {
        
    }

    #endregion
   
    
}
