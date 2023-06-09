using System;
using System.Collections;
using System.Collections.Generic;
using GleyGameServices;
using UnityEngine;

public class AchievmentHandler : MonoBehaviour
{
    [SerializeField] private AchievementNames[] allAchievements;
    private int indexNumberAchievements;

    private bool isLoggedInCheck = false;
    [SerializeField] private GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!GameServices.Instance.IsLoggedIn())
        {
            //login to game service
            GameServices.Instance.LogIn(LoginResult);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region CheckLoginSuccess
    private void LoginResult(bool success)
    {
        if (success == true)
        {
            //Login was successful
        }
        else
        {
            //Login failed
        }
        Debug.Log("Login success: " + success);
        GleyGameServices.ScreenWriter.Write("Login success: " + success);
    }
    

    #endregion

    #region CheckAchievementSubmissionSuccess

    private void AchievementSUbmitted(bool success, GameServicesError error)
    {
        if (success)
        {
            //achievement was submitted
        }
        else
        {
            //an error occurred
            Debug.LogError("Achivement failed to submit: " + error);

        }
        Debug.Log("Submit achievement result: " + success + " message:" + error);
        GleyGameServices.ScreenWriter.Write("Submit achievement result: " + success + " message:" + error);
    }

    #endregion
    
    //test Achievement function
    public void TestAchievement()
    {
        GameServices.Instance.SubmitAchievement(AchievementNames.TheBusBoy, AchievementSUbmitted);
    }
    
    
}
