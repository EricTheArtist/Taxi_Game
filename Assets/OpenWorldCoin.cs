using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldCoin : MonoBehaviour
{   
    public float rotationSpeed = 0.5f;
    public GameObject Coin;
    bool Active = true;
    public float cooldowntime;

    WaitForSeconds delay = new WaitForSeconds(10);

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Coin triggger " + other.tag.ToString());
        if (other.tag == "Player" && Active == true)
        {
            Active = false;
            Coin.SetActive(false);

            OpenWorldCurrencyManager.OWCMInstance.IncrementCoins(1);
            OpenWorldCurrencyManager.OWCMInstance.PlayCoinCollectVFX();

            StartCoroutine(WaitTenSeconds());
        }
    }



    // Update is called once per frame

    /*
    void Update()
    {
        if(Active == true)
        {
            Coin.transform.eulerAngles = new Vector3(Coin.transform.eulerAngles.x, Coin.transform.eulerAngles.y + rotationSpeed, Coin.transform.eulerAngles.z);
        }
        
    }
    */

    IEnumerator WaitTenSeconds()
    {
        yield return delay;
        Coin.SetActive(true);
        Active = true;
        // I waited five seconds!
    }
    /*
    void Reactivate()
    {
        Coin.SetActive(true);
        Active = true;
    }
    */
    
}
