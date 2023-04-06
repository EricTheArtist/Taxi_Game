using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{   
    [SerializeField] int passengerCount=0;
    //[SerializeField]
    //List<int> passengers = new List<int>();
    //[SerializeField] private GameObject pickUpPoint;

    BreakSystem breakSystem;
    CurrencySystem currencySystem;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        breakSystem = gameObject.GetComponent<BreakSystem>();
        currencySystem = gameObject.GetComponent<CurrencySystem>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)// If Player is within the pickup zone
    {
        if (breakSystem.isBraking && other.tag=="PickUpPoint")//check if the player has stoped in the zone
        {

            other.gameObject.SetActive(false);
            AddPassenger();
        }
    }

    void AddPassenger()//Perhaps we can change this to read in a specific number of passengers as a point and add that same number
    {
        passengerCount++;//right now it one passenger per zone
        currencySystem.run_amount=currencySystem.Addition_Function(3, currencySystem.run_amount);
        Debug.Log("Passenger Picked Up");
        //pickUpPointAnimator.SetTrigger("isPassengerBoarding");
        animator.SetTrigger("isGettingPassenger");
    }
}
