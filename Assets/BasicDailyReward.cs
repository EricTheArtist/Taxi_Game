using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class BasicDailyReward : MonoBehaviour
{
    public GameObject UnlockScreen;
    public GameObject RewardScreen;
    public TMP_Text RewardInfroText;
    public GameObject RewardUnlockButton;
    public GameObject LunchboxAnimationObject;
    GameObject LunchBoxOBJ;
    private DateTime lastRewardTime; // Time of the last rewarded day

    public GameObject RewardNotifiyerIcon;
    

    public TMP_Text CoinRewardAmountText;
    int CoinsReward;
    public GameObject CoinRewardIcon;
    public GameObject PaintRewardIcon;
    public Image PaintRewardImage;

    public Color[] RewardColour;

    public string[] PaintRewardPlayerPrefs;

    public CurrencySystem CS;
    
    int LifetimeRewardsGranted;

    private void Start()
    {
        if (PlayerPrefs.GetInt("RewardPendingOpen")==1)
        {
            RewardNotifiyerIcon.SetActive(true);
        }



        // Load the last rewarded time from PlayerPrefs or initialize it if it doesn't exist
        string lastRewardTimeString = PlayerPrefs.GetString("LastRewardTime", string.Empty);
        LifetimeRewardsGranted = PlayerPrefs.GetInt("LifetimeRewardsGranted");
        Debug.Log("Lifetime Rewards Granted: " + LifetimeRewardsGranted);
        if (!string.IsNullOrEmpty(lastRewardTimeString))
        {
            lastRewardTime = DateTime.Parse(lastRewardTimeString);
        }
        else
        {
            lastRewardTime = DateTime.MinValue;
        }


    }


    public void ButtonClaimReward()
    {
        GrantReward();
    }

    public bool CanClaimReward()
    {
        // Calculate the time difference between now and the last rewarded time
        TimeSpan timeSinceLastReward = DateTime.Now - lastRewardTime;

        // Check if at least 24 hours have passed
        return timeSinceLastReward.TotalHours >= 24;
    }

    private void GrantReward()
    {
        //Set reward claim button active 
        RewardNotifiyerIcon.SetActive(true);
        PlayerPrefs.SetInt("RewardPendingOpen", (true ? 1 : 0));
        // Update the last rewarded time to the current time
        lastRewardTime = DateTime.Now;
        PlayerPrefs.SetString("LastRewardTime", lastRewardTime.ToString());
    }

    public void OpenUnlockScreen()
    {
        if (UnlockScreen.activeInHierarchy)
        {
            UnlockScreen.SetActive(false);
        }
        else
        {
            UnlockScreen.SetActive(true);
            updateUnlockInfo();
        }
        
    }

    void updateUnlockInfo()
    {
        bool CanClaim = CanClaimReward();
        bool Pending = (PlayerPrefs.GetInt("RewardPendingOpen") != 0);
        if (CanClaim == true)
        {
            RewardInfroText.SetText("A luncbox is waiting for you somewhere on the road!");
            RewardUnlockButton.SetActive(false);
        }
        if (Pending == true)
        {
            RewardInfroText.SetText("Tap to get your random reward!");
            RewardUnlockButton.SetActive(true);
        }
        if (CanClaim == false && Pending == false)
        {
            RewardInfroText.SetText("Come back tomorrow to find another lunchbox!");
            RewardUnlockButton.SetActive(false);
        }
    }

    public void ButtonOpenLunchbox()
    {
        PlayerPrefs.SetInt("RewardPendingOpen", (false ? 1 : 0));
        RewardNotifiyerIcon.SetActive(false);
        updateUnlockInfo();
        OpenUnlockScreen();
        LunchBoxOBJ = Instantiate(LunchboxAnimationObject, new Vector3(0, 1.43f, -3.9f), Quaternion.identity);
        //grant reward of coins etc...
        Invoke("ShowRewards", 3);
        
    }

    public void ShowRewards()
    {
        Destroy(LunchBoxOBJ);
        OpenUnlockScreen();
        RewardScreen.SetActive(true);

        //generates coin reward
        float Reward = UnityEngine.Random.Range(300, 1000);
        CoinsReward = (int)Reward;

        CoinRewardAmountText.SetText(CoinsReward.ToString());
        PaintRewardIcon.SetActive(false);
        CoinRewardIcon.transform.localPosition = new Vector3(0, 51, 0);

        if (LifetimeRewardsGranted == 0 && PlayerPrefs.GetInt(PaintRewardPlayerPrefs[0]) == 0) //first lunchbox opening
        {
            GrantColourReward(0);
        }
        if(LifetimeRewardsGranted == 3 && PlayerPrefs.GetInt(PaintRewardPlayerPrefs[1]) == 0) //third lunchbox opening
        {
            GrantColourReward(1);
        }
        if (LifetimeRewardsGranted == 6 && PlayerPrefs.GetInt(PaintRewardPlayerPrefs[2]) == 0) //6th lunchbox opening
        {
            GrantColourReward(2);
        }
        if (LifetimeRewardsGranted == 12 && PlayerPrefs.GetInt(PaintRewardPlayerPrefs[3]) == 0) //12th lunchbox opening
        {
            GrantColourReward(3);
        }
        if (LifetimeRewardsGranted == 24 && PlayerPrefs.GetInt(PaintRewardPlayerPrefs[4]) == 0) //24th lunchbox opening
        {
            GrantColourReward(4);
        }
        

        LifetimeRewardsGranted++;
        PlayerPrefs.SetInt("LifetimeRewardsGranted",LifetimeRewardsGranted);



    }

    public void ButtonClaimRewards()
    {
        CS.Eric_AddCoins(CoinsReward);
        RewardScreen.SetActive(false);
        OpenUnlockScreen();
    }

    void GrantColourReward(int ColourIndex)
    {
        CoinRewardIcon.transform.localPosition = new Vector3(-115, 51, 0);
        PaintRewardIcon.SetActive(true);

        PaintRewardImage.color = RewardColour[ColourIndex];
        PlayerPrefs.SetInt(PaintRewardPlayerPrefs[ColourIndex], (true ? 1 : 0));
    }



}
