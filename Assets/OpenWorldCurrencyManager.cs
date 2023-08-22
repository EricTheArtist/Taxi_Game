using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenWorldCurrencyManager : MonoBehaviour
{
    public static OpenWorldCurrencyManager OWCMInstance;


    public TMP_Text Coins_Text;
    public TMP_Text Coins_Shadow;

    public TMP_Text Passenger_Count;

    int coins;
    int passengers;

    // Start is called before the first frame update
    void Start()
    {
        if (OWCMInstance == null)
        {
            OWCMInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        RefreshUIAmounts();
    }

    void RefreshUIAmounts()
    {
        coins = PlayerPrefs.GetInt("Main Amount");
        Coins_Text.SetText(coins.ToString());
        Coins_Shadow.SetText(coins.ToString());

        passengers = PlayerPrefs.GetInt("TotalPassengerCount");
        Passenger_Count.SetText(passengers.ToString());
    }

    public void IncrementCoins(int NumberOfCoins)
    {
        coins = PlayerPrefs.GetInt("Main Amount");
        coins += NumberOfCoins;
        PlayerPrefs.SetInt("Main Amount",coins);
        RefreshUIAmounts();
    }

    public void IncrementPassengers(int NumberOfPassengers)
    {
        passengers = PlayerPrefs.GetInt("TotalPassengerCount");
        passengers += NumberOfPassengers;
        PlayerPrefs.SetInt("TotalPassengerCount", passengers);
        RefreshUIAmounts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
