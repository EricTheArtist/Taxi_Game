using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BasicDailyReward : MonoBehaviour
{
    public GameObject UnlockScreen;
    public TMP_Text RewardInfroText;
    public GameObject RewardUnlockButton;
    private DateTime lastRewardTime; // Time of the last rewarded day

    private void Start()
    {
        // Load the last rewarded time from PlayerPrefs or initialize it if it doesn't exist
        string lastRewardTimeString = PlayerPrefs.GetString("LastRewardTime", string.Empty);
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

        //grant reward of coins etc...

        updateUnlockInfo();
    }



}
