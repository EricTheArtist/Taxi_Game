using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldPassenger : MonoBehaviour
{
    bool consumed = false;
    public GameObject loadbar;
    public int CoinsPayout;
    bool carInside;
    float t;
    public GameObject passenger;
    public GameObject passengerPickupIcon;
    public float cooldowntime = 15;
    public OpenWorldDropOff OWDO;

    public Animator PickupAnimator;
    public GameObject AnimatedPassenger;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && consumed == false)
        {
            carInside = true;
            
            //start timer
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && consumed == false)
        {
            //cancel timer
            carInside = false;
            loadbar.transform.localScale = new Vector3(0, 5, 5);
            t = 0.0f;
        }
    }

    private void Update()
    {
        if(carInside == true)
        {
            loadbar.transform.localScale = new Vector3(Mathf.Lerp(0, 25, t), 5, 5);


            t += 0.5f * Time.deltaTime;

            if (t > 1.0f)
            {
                PassengerCollected();
                consumed = true;
                carInside = false;
                t = 0.0f;
            }
        }
    }

    void PassengerCollected()
    {
        
        passengerPickupIcon.SetActive(false);
        OpenWorldCurrencyManager.OWCMInstance.IncrementCoins(CoinsPayout);
        OpenWorldCurrencyManager.OWCMInstance.PlayPassengerCollectDropVFX();
        Invoke("ReactivatePassenger", cooldowntime);
        Invoke("ResetAnimation", 1);
        OWDO.ReactivatePassenger();
        PickupAnimator.SetTrigger("isPassengerBoarding");

        if(PlayerLevelSystem.PLSinstance!= null)
        {
            PlayerLevelSystem.PLSinstance.AddXP(10);
        }
        
    }

    void ResetAnimation()
    {
        passenger.SetActive(false);
        AnimatedPassenger.transform.localPosition = new Vector3(0, 0, 0);
        AnimatedPassenger.transform.localRotation = Quaternion.identity;
    }

    void ReactivatePassenger()
    {
        loadbar.transform.localScale = new Vector3(0, 5, 5);
        passenger.SetActive(true);
        passengerPickupIcon.SetActive(true);
        consumed = false;
        
    }

}
