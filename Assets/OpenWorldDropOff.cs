using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenWorldDropOff : MonoBehaviour
{
    bool consumed = true;
    public GameObject Dropoffloadbar;
    public int CoinsPayout;
    bool carInside;
    float t;
    public GameObject passenger;
    public GameObject passengerDropoffIcon;
    public Animator PassengerDropOffAnim;
    public GameObject PassengerLocal;

    public UnityEvent OWEnterDropOffEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && consumed == false)
        {
            carInside = true;
            OWEnterDropOffEvent.Invoke();
            //start timer
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && consumed == false)
        {
            //cancel timer
            carInside = false;
            Dropoffloadbar.transform.localScale = new Vector3(0, 1, 1);
            t = 0.0f;
        }
    }

    private void Update()
    {
        if (carInside == true)
        {
            Dropoffloadbar.transform.localScale = new Vector3(Mathf.Lerp(0, 5, t), 1, 1);


            t += 0.5f * Time.deltaTime;

            if (t > 1.0f)
            {
                PassengerDropped();
                consumed = true;
                carInside = false;
                t = 0.0f;
            }
        }
    }

    void PassengerDropped()
    {
        passenger.SetActive(true);
        PassengerDropOffAnim.SetTrigger("isPassengerBoarding");
        passengerDropoffIcon.SetActive(false);
        Invoke("ResetAnimation", 1);
        OpenWorldCurrencyManager.OWCMInstance.IncrementCoins(CoinsPayout);
        OpenWorldCurrencyManager.OWCMInstance.PlayPassengerCollectDropVFX();
        OpenWorldMissionSystem.OWMSinstance.AddPassengerDrop();
        if (PlayerLevelSystem.PLSinstance != null)
        {
            PlayerLevelSystem.PLSinstance.AddXP(10);
        }
    }

    void ResetAnimation()
    {
        passenger.SetActive(false);
        PassengerLocal.transform.localPosition = new Vector3(0, 0, 0);
        PassengerLocal.transform.localRotation = Quaternion.identity;
    }


    public void ReactivatePassenger()
    {
        Dropoffloadbar.transform.localScale = new Vector3(0, 1, 1);
        passenger.SetActive(false);
        passengerDropoffIcon.SetActive(true);
        consumed = false;

    }
}
