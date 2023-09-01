using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PickUpSystem : MonoBehaviour
{   
   public int passengerCount=0;
   public int greenLightPassed = 0;
   public int copsEscaped = 0;

    public int TotalPassengerCount = 0;
    public TMP_Text PassengerCountText;
    //[SerializeField]
    //List<int> passengers = new List<int>();

    //BreakSystem breakSystem;
    private SimpleInputNamespace.TestCharacterController controller;
    CurrencySystem currencySystem;
    private Animator animator;
    private GameObject pickUpPoint;

    float CopChaseDuration = 8;
    public bool CopIsChasing = false;

    public GameObject Cops;
    bool used;
    public bool stoppedAtRed = false; //set true when the character stops at a red light while being chased by the police

    public UnityEvent EnterPassengerCollect;
    public UnityEvent EnterRobot;
    public UnityEvent CaughtAtRobot;

    public ParticleSystem CoinEffect;
    public ParticleSystem CoinsEffect;
    public ParticleSystem LunchboxEffect;

    BasicDailyReward BDR;
    AbilitySystem ABS;
    GameEndSystem GES;

    [SerializeField] private AudioClip _coinCollectClip;
    [SerializeField] private AudioClip _passengerCollectClip;

    // Start is called before the first frame update
    void Start()
    {
        TotalPassengerCount = PlayerPrefs.GetInt("TotalPassengerCount");
        PassengerCountText.SetText(TotalPassengerCount.ToString());
        passengerCount = 0;
        copsEscaped = 0;
        greenLightPassed = 0;
        ABS = gameObject.GetComponent<AbilitySystem>();
        controller = GetComponent<SimpleInputNamespace.TestCharacterController>();
        currencySystem = gameObject.GetComponent<CurrencySystem>();
        animator = GetComponent<Animator>();
        GES = gameObject.GetComponent<GameEndSystem>();
        BDR = GameObject.FindGameObjectWithTag("RewardCheck").GetComponent<BasicDailyReward>();
    }

    private void Awake()
    {
        passengerCount = 0;
        copsEscaped = 0;
        greenLightPassed = 0;
    }

    private void Update()
    {
        //CheckCopChase();
    }

    private void OnEnable()
    {
        
    }

    void CheckCopChase()
    {
        if (CopIsChasing == true)
        {
            //PlayPoliceAnimation here
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUpPoint")
        {
            //AudioCoincollect.Play();
            SoundManager.Instance.PlaySound(_coinCollectClip);
            controller.breakButton.SetActive(true);
            controller.BreakInstruction.SetText("TAP!");
            used = false;
            pickUpPoint = other.transform.parent.gameObject;

            EnterPassengerCollect.Invoke();

            currencySystem.run_amount = currencySystem.Addition_Function(3, currencySystem.run_amount);//this var is only used for display purposes at the end of a run
            currencySystem.Eric_AddCoins(3); //adds 3 coins to the players saved coins
            CoinEffect.Play();
            PlayerLevelSystem.PLSinstance.AddXP(5);
            MissionsSystem.MSinstance.AddPassenger();
        }

        if (other.tag == "Robot")
        {
            ABS.CancelSpeedAbility();
            Test_Robot TR = other.gameObject.GetComponent<Test_Robot>();
            if(TR.RedLightON == true && CopIsChasing == false)
            {
                controller.breakButton.SetActive(true);
                controller.BreakInstruction.SetText("HOLD!");
                EnterRobot.Invoke(); //event used for tutorial
                
            }
            

        }

        if (other.tag == "LunchBox")
        {
            BDR.ButtonClaimReward();
            other.gameObject.SetActive(false);
            LunchboxEffect.Play();
            PlayerLevelSystem.PLSinstance.AddXP(45);
        }
    }

    private void OnTriggerStay(Collider other)// If Player is within the pickup zone
    {
        if (controller.isBraking && other.tag == "PickUpPoint")//check if the player has stoped in the zone
        {
           //right now it one passenger per zone
            if (used == false)
            {
                AddPassenger();
                used = true;
                controller.BreakPadUp();

            }
        }

        if (other.tag == "Robot")
        {
            Test_Robot TR = other.gameObject.GetComponent<Test_Robot>();
            if (TR.RedLightON == false) //if the light is green 
            {
                controller.BreakInstruction.SetText("GO!");
            }
            if (controller.isBraking == false && TR.RedLightON == false) //if the player is moving and the light is green 
            {
                controller.breakButton.SetActive(false);
            }
            if (controller.isBraking == false && stoppedAtRed == false && TR.RedLightON == true && !GES.endgame) // if the player is moving and the red light is on
            {
                controller.breakButton.SetActive(true);
            }
            else if (controller.isBraking == true && stoppedAtRed == false && CopIsChasing == true) // if the player brakes and they are being chased by the cop
            {
                GES.Collision_CopCatch();
                stoppedAtRed = true;
                CaughtAtRobot.Invoke();
                //invoke get caught by cop for narritive
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickUpPoint")
        {
            
            controller.breakButton.SetActive(false);
            if (controller.isBraking) // if the player is still holding down the break pad after exiting the pickup zone, they are forced to continue driving
            {
                controller.BreakPadUp();
            }
            Invoke("DeactivatePickUpPoint", 0.5f);
        }

        if (other.tag == "Robot")
        {
            PlayerLevelSystem.PLSinstance.AddXP(10);
            MissionsSystem.MSinstance.AddRobotPassed();
            controller.breakButton.SetActive(false);
            Test_Robot TR =  other.gameObject.GetComponent<Test_Robot>();
            if(TR.RedLightON == true && CopIsChasing == false && ABS.CanEscapeCop == false) // starts the cop chase if the player exits the robot while it it is red
            {

                controller.movementSpeed += 10;
                CopIsChasing = true;
                Cops.SetActive(true);
                Invoke("CopChase", CopChaseDuration);
                Invoke("Escaped", CopChaseDuration);
            }
            if (ABS.CanEscapeCop == true && TR.RedLightON == true)
            {
                ABS.UseEscapeCopsAbility();
            }
            else
            {
                greenLightPassed++;
            }

        }
    }

    void Escaped()
    {
        copsEscaped++;
    }
    public void CopChase()
    {
        CopIsChasing = false;
        controller.movementSpeed -= 10;
        Cops.SetActive(false);
        if (!GES.endgame)
        {
            PlayerLevelSystem.PLSinstance.AddXP(20);
            MissionsSystem.MSinstance.AddCopsEscaped();
        }

    }

    public void CancelCopChase()
    {
        CancelInvoke("CopChase");
        CopIsChasing = false;
        Cops.SetActive(false);
    }

    void AddPassenger()//Perhaps we can change this to read in a specific number of passengers as a point and add that same number
    {
        passengerCount++;
        TotalPassengerCount++;
        PlayerPrefs.SetInt("TotalPassengerCount", TotalPassengerCount);
        PassengerCountText.SetText(TotalPassengerCount.ToString());
        SoundManager.Instance.PlaySound(_passengerCollectClip);
        currencySystem.run_amount=currencySystem.Addition_Function(7, currencySystem.run_amount);//this var is only used for display purposes at the end of a run
        currencySystem.Eric_AddCoins(7); //adds 7 coins to the players saved coins
        Debug.Log("Passenger Picked Up");
        CoinsEffect.Play();
        PlayerLevelSystem.PLSinstance.AddXP(10);
        pickUpPoint.transform.GetChild(1).GetComponent<PassengerManager>().PlayCharacterJump(); //gets the passenger if it is the second child object of the pickup box

    }

    void DeactivatePickUpPoint()
    {

        pickUpPoint.SetActive(false);
    }
}
