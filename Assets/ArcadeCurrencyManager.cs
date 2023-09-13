using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ArcadeCurrencyManager : MonoBehaviour
{
    public static ArcadeCurrencyManager ACMInstance;

    public TMP_Text Coins_Text;
    public TMP_Text Coins_Shadow;
    public TMP_Text Score_Text;
    public TMP_Text Countdown_Text;

    public TMP_Text End_RecentScore;
    public TMP_Text End_HighScore;

    int coins;
    int Score = 0;
    int HighScore;

    float Countdown = 30;
    int CountdownInt;
    public GameObject GameEndScreen;
    public GameObject RentAgainGameEndScreen;
    public GameObject ReplayButton;

    [SerializeField] private AudioClip CoinsCollectClip;

    // Start is called before the first frame update
    void Start()
    {
        RefreshUIAmounts();
        HighScore = PlayerPrefs.GetInt("ShootingHighScore");
        
        if (ACMInstance == null)
        {
            ACMInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        
    }

    void Update()
    {

        Countdown -= Time.deltaTime;

        CountdownInt = (int)Countdown + 1;
        Countdown_Text.SetText(CountdownInt.ToString());

        if (Countdown < 0)
        {
            Countdown_Text.SetText("0");
            ShootingTimeEnded();
        }
    }

    void ShootingTimeEnded()
    {
        
        bool Owned = (PlayerPrefs.GetInt("Car02Premium") != 0);
        GameEndScreen.SetActive(true);
        if(Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("ShootingHighScore", Score);
        }
        End_RecentScore.SetText(Score.ToString());
        End_HighScore.SetText(HighScore.ToString());

        if(Owned == true)
        {
            ReplayButton.SetActive(true);
        }
        if(Owned == false)
        {
            RentAgainGameEndScreen.SetActive(true);
        }
        //Time.timeScale = 0;
    }

    void RefreshUIAmounts()
    {
        coins = PlayerPrefs.GetInt("Main Amount");
        Coins_Text.SetText(coins.ToString());
        Coins_Shadow.SetText(coins.ToString());

        Score_Text.SetText(Score.ToString());
    }

    public void IncrementCoins(int NumberOfCoins)
    {
        coins = PlayerPrefs.GetInt("Main Amount");
        coins += NumberOfCoins;
        PlayerPrefs.SetInt("Main Amount", coins);
        Score++;
        RefreshUIAmounts();
        PlayCoinCollectSound();
    }

    public void PlayCoinCollectSound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(CoinsCollectClip);
        }
    }

    public void LoadShop()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void LoadMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ReloadThis()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(3);
    }

    


}
