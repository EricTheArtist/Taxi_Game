 using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleInputNamespace;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilitySystem : MonoBehaviour
{
    #region Varibales

    public GameObject player;
    public GameObject AbilityButton;
    public bool canUseAbility;
    public GameObject CooldownBar;
    //-------------------------------------------------------------
    // storage variabes
    private float tempFloat = 0;
    private int tempInt = 0;
    //-------------------------------------------------------------
    private bool speedAbilityActive = false;
    private bool armourAbilityActive = false;
    //-------------------------------------------------------------
    //AdditionalAbilityButton
    public GameObject[] AdditionalAbilities;
    int AdditionalAbilityIndex;
    public GameObject SirensVFX;
    public ParticleSystem ShootEffect;
    [SerializeField] private AudioClip HootSound;
    [SerializeField] private AudioClip ShootSound;
    [SerializeField] private AudioClip SirenSound;
    //-------------------------------------------------------------
    public enum AbilityType
    {
        Normal,
        SpeedBoost,
        Armour,
        DoubleCoins,
        Escapist, //for colddrink 
        Flying
    }
    [Tooltip("current vehicle ability active | Set here to change ability ")] 
    public AbilityType abilityType;
    //-------------------------------------------------------------
    [Header("Ability Timers")] 
    public bool timerActive = false;
    public float AbilityTimeLeft = 0;
    public float SpeedAbilityTimer;
    public float ArmourAbiltyTimer;
    public float DoubleCoinAbilityTimer;
    public bool CanEscapeCop = false;
    float totalTime;//used to calculate timer bar
    //-------------------------------------------------------------
    private CurrencySystem _currencySystem;
    private TestCharacterController _controller;
    private PickUpSystem _PickupSystem;
    //-------------------------------------------------------------
    //for checking which car is selected
    int Carindex;
    public TMP_Text Ability_Text;
    public bool CanSpawnPickup;
    #endregion
    //-------------------------------------------------------------
    #region Start and Update

    private void Start()
    {
        _PickupSystem = gameObject.GetComponent<PickUpSystem>();
        //setting which ability should be acive based on what car is selected
        Carindex = PlayerPrefs.GetInt("MLActiveCar");

        if(Carindex == 2 || Carindex == 5 || Carindex == 7 || Carindex == 9 || Carindex == 13) //golf or BMW or delorian or golf7
        {
            abilityType = AbilityType.SpeedBoost;
            Ability_Text.SetText("GALAKAN");
            CanSpawnPickup = true;

            //sets value of ability timer
            SpeedAbilityTimer = PlayerPrefs.GetFloat("MLAbility_Speed");
            AdditionalAbilityIndex = 0;

        }
        if(Carindex == 4 || Carindex == 11) //police truck
        {
            abilityType = AbilityType.Escapist;
            Ability_Text.SetText("TIADA POLIS");
            CanSpawnPickup = true;
            AdditionalAbilityIndex = 2;

        }
        if(Carindex == 3 || Carindex == 6) //landcruiser or tank
        {
            abilityType = AbilityType.Armour;
            Ability_Text.SetText("PERISAI");
            CanSpawnPickup = true;
            //sets value of ability timer
            ArmourAbiltyTimer = PlayerPrefs.GetFloat("MLAbility_Armour");
            if (Carindex == 6)
            {
                AdditionalAbilityIndex = 1;
            }
            else
            {
                AdditionalAbilityIndex = 0;
            }
        }
        if (Carindex == 1 || Carindex == 8 || Carindex == 10 || Carindex == 12) //Quantum or hance
        {
            abilityType = AbilityType.DoubleCoins;
            Ability_Text.SetText("2 x SYILING");
            CanSpawnPickup = true;
            AdditionalAbilityIndex = 0;
            //sets value of ability timer
            DoubleCoinAbilityTimer = PlayerPrefs.GetFloat("MLAbility_Double");
        }
        if (Carindex == 0)
        {
            CanSpawnPickup = false;
            Debug.Log("NoAcive Ability, Car Index: " + Carindex);
        }

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.StopSound();

        }

        canUseAbility = false;
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

        if (timerActive)
        {
            StartTimer();
        }
        
    }

    #endregion
    //-------------------------------------------------------------
    #region AbilityHandler

    public void ActivateAbilty()
    {
       
        if (canUseAbility)
        {
            canUseAbility = false;

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
                    EscapeCopsAbility();
                    break;
                //--------------------------------------------------
    
            }
        }
        else
        {
            Debug.Log("Can Not Use Ability");
        }
        
    }

    #endregion
    //-------------------------------------------------------------
    #region AbilityFunctions

    #region SpeedAbility
    // Provide speed Boost | Enable Rockets GameObject | Disable Collider
    void SpeedAbility()
    {
        float speedBoost = _controller.movementSpeed + 15;//set speedboost to current speed plus an amount
        tempFloat = _controller.movementSpeed;// Store current Movement speed to set back to when ability ends
        speedAbilityActive = true;
        Physics.IgnoreLayerCollision(6,3, true);//Disables collisions between player and obsticles
        _controller.movementSpeed = speedBoost; //set current speed to speedboost
        AbilityTimeLeft = SpeedAbilityTimer;
        timerActive = true;
        totalTime = SpeedAbilityTimer;
        Debug.Log("This is Start of Speed Ab move speed: "+_controller._movementSpeed);
        Invoke("EndSpeedAbility",AbilityTimeLeft);
    }

    public void CancelSpeedAbility()
    {
        if(speedAbilityActive == true)
        {
            CancelInvoke("EndSpeedAbility");
            //EndSpeedAbility();

            EndSpeedAbility();
        }

    }
    void EndSpeedAbility()
    {
        //Debug.Log("End Speed Ability");
         _controller.movementSpeed = tempFloat;//lerp this value
       //_controller.movementSpeed = Mathf.Lerp(_controller.movementSpeed, tempFloat, 0.25f);//here is the lerp;
       Debug.Log("This is End of Speed Ab move speed: "+_controller._movementSpeed);
        speedAbilityActive = false;
        Physics.IgnoreLayerCollision(6,3, false);
        //player.GetComponent<Collider>().enabled = true;
        AbilityButton.SetActive(false);
        CanSpawnPickup = true;
    }

    #endregion

    #region ArmourAbility

    // use physic layer ignore again
    void ArmourAbilty()
    {
        Debug.Log("Start Armour Ability");
        armourAbilityActive = true;
        Physics.IgnoreLayerCollision(6,3,true);
        AbilityTimeLeft = ArmourAbiltyTimer;
        timerActive = true;
        totalTime = ArmourAbiltyTimer;
        Invoke("EndArmourAbility",AbilityTimeLeft );
    }

    void EndArmourAbility()
    {
        Debug.Log("End Armour Ability");
        armourAbilityActive = false;
        Physics.IgnoreLayerCollision(6,3, false);
        AbilityButton.SetActive(false);
        CanSpawnPickup = true;
    }

    #endregion

    #region DoubleCoinsAbility

    void DoubleCoinsAbility()
    {
        Debug.Log("Double Coins Active");
        tempInt = _currencySystem.multiplier;
        _currencySystem.multiplier = 2 * _currencySystem.multiplier;
        AbilityTimeLeft = DoubleCoinAbilityTimer;
        timerActive = true;
        totalTime = DoubleCoinAbilityTimer;
        Invoke("EndDoubleCoinsAbility", AbilityTimeLeft);

        CanSpawnPickup = true;
    }
    void EndDoubleCoinsAbility()
    {
        Debug.Log("Double Coins Inactive");
        _currencySystem.multiplier = tempInt;
        AbilityButton.SetActive(false);
    }

    #endregion
    #region EscapCops

    void EscapeCopsAbility()
    {
        CanEscapeCop = true;
        if(_PickupSystem.CopIsChasing == true)
        {
            _PickupSystem.CancelCopChase();
            UseEscapeCopsAbility();
        }
    }

    public void UseEscapeCopsAbility()
    {
        CanEscapeCop = false;
        AbilityButton.SetActive(false);
    }

    #endregion
    #endregion
    //-------------------------------------------------------------

    #region Collision Control

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AbilityPickup"))
        {
            if (timerActive == false)
            {
                PlayerLevelSystem.PLSinstance.AddXP(15);
                canUseAbility = true;
                AbilityButton.SetActive(true);
                CanSpawnPickup = false;
                ActivateAbilty();
            }

            other.gameObject.SetActive(false);
        }
    }

    #endregion

    #region Timer

    void StartTimer()
    {
        if (AbilityTimeLeft > 0)
        {
            AbilityTimeLeft -= Time.deltaTime;
            CooldownBar.transform.localScale = new Vector3(AbilityTimeLeft/totalTime,1,1);
        }
        else
        {
            CooldownBar.transform.localScale = new Vector3(1, 1, 1);
            timerActive = false;
        }
        //            AbilityTimeLeft -= Mathf.Lerp(startTime, 0, Time.deltaTime);

    }

    #endregion

    public void AdditionalAbilityButton()
    {
        foreach(GameObject obj in AdditionalAbilities)
        {
            if(System.Array.IndexOf(AdditionalAbilities,obj) == AdditionalAbilityIndex)
            {
                obj.SetActive(true);
            }
            else
            {
                Destroy(obj);
            }
        }
    }

    public void AdditinalAbility()
    {
        if(AdditionalAbilityIndex == 0)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySound(HootSound);
                Handheld.Vibrate();
            }
        }
        if(AdditionalAbilityIndex == 1)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySound(ShootSound);
                Handheld.Vibrate();
                if (ShootEffect != null)
                {
                    ShootEffect.Play();
                }
                
            }
        }
        if(AdditionalAbilityIndex == 2)
        {
            if (SoundManager.Instance != null)
            {
                if (SirensVFX.activeInHierarchy)
                {
                    if (SirensVFX != null)
                    {
                        SirensVFX.SetActive(false);
                    }
                    
                    SoundManager.Instance.StopSound();
                }
                else
                {
                    SoundManager.Instance.PlaySound(SirenSound);
                    if (SirensVFX != null)
                    {
                        SirensVFX.SetActive(true);
                    }
                    
                }

            }


        }
    }
    
}
