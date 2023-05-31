using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mamello_PickUpSystem : MonoBehaviour
{   
    [SerializeField] int passengerCount=0;
    //[SerializeField]
    //List<int> passengers = new List<int>();
    //[SerializeField] private GameObject pickUpPoint;

    //BreakSystem breakSystem;
    private SimpleInputNamespace.TestCharacterController controller;
    CurrencySystem currencySystem;
    private Animator animator;

    float CopChaseDuration = 8;
    bool CopIsChasing = false;

    public GameObject Cops;
    bool used;
    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<SimpleInputNamespace.TestCharacterController>();
        currencySystem = gameObject.GetComponent<CurrencySystem>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckCopChase();   
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
    private void OnTriggerStay(Collider other)// If Player is within the pickup zone
    {
        if (controller.isBraking && other.tag=="PickUpPoint")//check if the player has stoped in the zone
        {
            if(used == false)
            {
                AddPassenger();
                used = true;
                other.gameObject.SetActive(false);
            }
           
        }
        if (other.tag == "Robot")
        {
            Test_Robot TR = other.gameObject.GetComponent<Test_Robot>();
            if (TR.RedLightON == false)
            {
                controller.BreakInstruction.SetText("GO!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUpPoint")
        {
            controller.breakButton.SetActive(true);
            controller.BreakInstruction.SetText("TAP!");
            used = false;
        }
        if (other.tag == "Robot")
        {
            Test_Robot TR = other.gameObject.GetComponent<Test_Robot>();
            if(TR.RedLightON == true)
            {
                controller.breakButton.SetActive(true);
                controller.BreakInstruction.SetText("HOLD!");
            }
            //Test_Robot TR = other.gameObject.GetComponent<Test_Robot>();
            //TR.CountoGreen();

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
            //other.gameObject.SetActive(false);
        }
        if (other.tag == "Robot")
        {
            controller.breakButton.SetActive(false);
            Test_Robot TR =  other.gameObject.GetComponent<Test_Robot>();
            if(TR.RedLightON == true)
            {
                controller.movementSpeed += 10;
                CopIsChasing = true;
                Cops.SetActive(true);
                Invoke("CopChase", CopChaseDuration);
            }
        }
    }

    public void CopChase()
    {
        CopIsChasing = false;
        controller.movementSpeed -= 10;
        Cops.SetActive(false);
    }

    public void CancelCopChase()
    {
        CancelInvoke("CopChase");
        CopIsChasing = false;
        Cops.SetActive(false);
    }

    void AddPassenger()//Perhaps we can change this to read in a specific number of passengers as a point and add that same number
    {
        passengerCount++;//right now it one passenger per zone
        currencySystem.run_amount=currencySystem.Addition_Function(3, currencySystem.run_amount);//this var is just used for display at the end of a run
        currencySystem.Eric_AddCoins(3); //adds 3 coins to the players saved coins
        Debug.Log("Passenger Picked Up");
        //pickUpPointAnimator.SetTrigger("isPassengerBoarding");
        animator.SetTrigger("isGettingPassenger");
    }
}
