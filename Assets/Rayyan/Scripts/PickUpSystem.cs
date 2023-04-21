using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
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

            other.gameObject.SetActive(false);
            AddPassenger();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUpPoint")
        {
            controller.breakButton.SetActive(true);
        }
        if (other.tag == "Robot")
        {
            controller.breakButton.SetActive(true);
            Test_Robot TR = other.gameObject.GetComponent<Test_Robot>();
            TR.CountoGreen();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickUpPoint")
        {
            controller.breakButton.SetActive(false);
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

    void AddPassenger()//Perhaps we can change this to read in a specific number of passengers as a point and add that same number
    {
        passengerCount++;//right now it one passenger per zone
        currencySystem.run_amount=currencySystem.Addition_Function(3, currencySystem.run_amount);
        currencySystem.main_amount += 3; //run amount is no longer being added to total at end of run, the total is being updated during the run and the run_amount is only being used on the game end screen
        Debug.Log("Passenger Picked Up");
        //pickUpPointAnimator.SetTrigger("isPassengerBoarding");
        animator.SetTrigger("isGettingPassenger");
    }
}
