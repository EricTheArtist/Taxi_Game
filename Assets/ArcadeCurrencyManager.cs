using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArcadeCurrencyManager : MonoBehaviour
{
    public static ArcadeCurrencyManager ACMInstance;

    public TMP_Text Coins_Text;
    public TMP_Text Coins_Shadow;

    int coins;

    [SerializeField] private AudioClip CoinsCollectClip;

    // Start is called before the first frame update
    void Start()
    {
        if (ACMInstance == null)
        {
            ACMInstance = this;
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
    }

    public void IncrementCoins(int NumberOfCoins)
    {
        coins = PlayerPrefs.GetInt("Main Amount");
        coins += NumberOfCoins;
        PlayerPrefs.SetInt("Main Amount", coins);
        RefreshUIAmounts();
    }

    public void PlayCoinCollectSound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(CoinsCollectClip);
        }
    }



}
