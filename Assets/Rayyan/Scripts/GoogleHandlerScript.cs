using System;
using System.Collections;
using System.Collections.Generic;
using GleyGameServices;
using UnityEngine;

public class GoogleHandlerScript : MonoBehaviour
{
    private LeaderboardNames[] allLeaderboards;//Stores all LeaderBaords
    private AchievementNames[] allAchievements;//Stores all Achievements
    private int indexNumberAchievements;
    private int indexNumberLeaderboards;

    private Test_HighScore highScoreHandler;

    private long steeringHighscore = 0;
    private long swipingHighscore = 0;

    //check login status
    private bool isLoggedInCheck = false;
    [SerializeField] private GameObject Player;
    private void Start()
    {   
        //call login method
        if (!GameServices.Instance.IsLoggedIn())
        {
            //Login to Game Servicies
                GameServices.Instance.LogIn(LoginResult);
        }
        else
        {
            isLoggedInCheck = true;
        }
        
        //add all leaderbaords to the list
        int nrOfLeaderboards = System.Enum.GetValues(typeof(LeaderboardNames)).Length;
        allLeaderboards = new LeaderboardNames[nrOfLeaderboards];
        for (int i = 0; i < nrOfLeaderboards; i++)
        {
            allLeaderboards[i] = ((LeaderboardNames)i);
        }
    }

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

        public void ShowLeaderboards()
        {
            GameServices.Instance.ShowLeaderboadsUI();
            //GameServices.Instance.ShowSpecificLeaderboard(LeaderboardNames.TaxiRank);
        }
        //show on new highscore

        public void SwippingScoreHandler()
        {
            if (GameServices.Instance.IsLoggedIn())
            {
                swipingHighscore = PlayerPrefs.GetInt("HighScore");
                GameServices.Instance.SubmitScore(swipingHighscore,allLeaderboards[indexNumberLeaderboards], ScoreSubmitted);
            }
            
        } void SteeringScoreHandler()
        {
            if (GameServices.Instance.IsLoggedIn())
            {
                swipingHighscore = PlayerPrefs.GetInt("HighScoreSteering");
                GameServices.Instance.SubmitScore(steeringHighscore,allLeaderboards[indexNumberLeaderboards], ScoreSubmitted);
            }
            
        }
        
        private void ScoreSubmitted(bool success, GameServicesError error)
        {
            if (success)
            {
                //score successfully submitted
            }
            else
            {
                //an error occurred
                Debug.LogError("Score failed to submit: " + error);
            }
            Debug.Log("Submit score result: " + success + " message:" + error);
            GleyGameServices.ScreenWriter.Write("Submit score result: " + success + " message:" + error);
        }
}
