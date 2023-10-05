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

    public ParticleSystem CoinSpark;
    public ParticleSystem MultipleCoinsEffect;
    public ParticleSystem SingleCoinEffect;
    [SerializeField] private AudioClip CoinCollectClip;
    [SerializeField] private AudioClip CoinsCollectClip;

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
        coins = PlayerPrefs.GetInt("MLMain Amount");
        Coins_Text.SetText(coins.ToString());
        Coins_Shadow.SetText(coins.ToString());

        passengers = PlayerPrefs.GetInt("MLTotalPassengerCount");
        Passenger_Count.SetText(passengers.ToString());
    }

    public void IncrementCoins(int NumberOfCoins)
    {
        coins = PlayerPrefs.GetInt("MLMain Amount");
        coins += NumberOfCoins;
        PlayerPrefs.SetInt("MLMain Amount", coins);
        RefreshUIAmounts();
    }

    public void PlayCoinCollectVFX()
    {
        CoinSpark.Play();
        SingleCoinEffect.Play();
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(CoinCollectClip);
        }
    }

    public void PlayPassengerCollectDropVFX()
    {
        MultipleCoinsEffect.Play();
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(CoinsCollectClip);
        }
    }

    public void IncrementPassengers(int NumberOfPassengers)
    {
        passengers = PlayerPrefs.GetInt("MLTotalPassengerCount");
        passengers += NumberOfPassengers;
        PlayerPrefs.SetInt("MLTotalPassengerCount", passengers);
        RefreshUIAmounts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
