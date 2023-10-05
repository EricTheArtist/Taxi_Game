using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallRankingSystem : MonoBehaviour
{

    //Distance | Passenger Count | Coin Run Collection | Most Escapes | Green Robots 
    private int longestDistance;
    private int mostPassengers;
    private int mostRunCoins;
    private int mostRunEscapes;
    private int mostGreenRobots;
    private int overAllScore;

    private CurrencySystem _currencySystem;
    private PickUpSystem _pickUpSystem;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        _currencySystem = player.GetComponent<CurrencySystem>();
        _pickUpSystem = player.GetComponent<PickUpSystem>();
    }

    public void ResetValues()
    {
        _pickUpSystem.passengerCount = 0;
        _pickUpSystem.copsEscaped = 0;
        _pickUpSystem.greenLightPassed = 0;
    }
    public void ScoreCalculator()
    {
        //checks between the two distance scores and sets greatest one
        #region Set Longest Distance

        if (PlayerPrefs.GetInt("MLHighScore") > PlayerPrefs.GetInt("MLHighScoreSteering"))
        {
            longestDistance = PlayerPrefs.GetInt("MLHighScore");
        }
        else
        {
            longestDistance = PlayerPrefs.GetInt("MLHighScoreSteering");
        }

        #endregion
        
        //checks at end of run if run amount is greater than what is currently stored/recorded
        #region Set Most Coin Runs

        if (PlayerPrefs.GetInt("MLLongestCoinRun") < _currencySystem.run_amount)
        {
            PlayerPrefs.SetInt("MLLongestCoinRun", _currencySystem.run_amount);
            mostRunCoins = PlayerPrefs.GetInt("MLLongestCoinRun");
        }
        else
        {
            mostRunCoins = PlayerPrefs.GetInt("MLLongestCoinRun");
        }
        #endregion
        
        //Sets Most Passenger Pickups
        #region Set Most Passenger Pickups

        if (PlayerPrefs.GetInt("MLMostPassengers") < _pickUpSystem.passengerCount)
        {
            PlayerPrefs.SetInt("MLMostPassengers", _pickUpSystem.passengerCount);
            mostPassengers = PlayerPrefs.GetInt("MLMostPassengers");
        }
        else
        {
            mostPassengers = PlayerPrefs.GetInt("MLMostPassengers");
        }

        #endregion
        
        //SetsMostEscapes

        #region  Set Most Cop Escapes

        if (PlayerPrefs.GetInt("MLMostEscapes") < _pickUpSystem.copsEscaped)
        {
            PlayerPrefs.SetInt("MLMostEscapes", _pickUpSystem.copsEscaped);
            mostRunEscapes = PlayerPrefs.GetInt("MLMostEscapes");
        }
        else
        {
            mostRunEscapes = PlayerPrefs.GetInt("MLMostEscapes");
        }

        #endregion
        
        //SetsMostGreenLights

        #region Set Most Green Lights Passed

        if (PlayerPrefs.GetInt("MLMostGreenLights") < _pickUpSystem.greenLightPassed)
        {
            PlayerPrefs.SetInt("MLMostGreenLights", _pickUpSystem.greenLightPassed);
            mostGreenRobots = PlayerPrefs.GetInt("MLMostGreenLights");
        }
        else
        {
            mostGreenRobots = PlayerPrefs.GetInt("MLMostGreenLights");
        }

        #endregion
        
        //-----------------------------------------------------------------------------
        #region Sets Overall Highscore
        
        overAllScore = (longestDistance + mostPassengers + mostRunCoins + mostRunEscapes + mostGreenRobots)/5;
       /* Debug.Log("This is Green Lights: " +mostGreenRobots +" = "+ PlayerPrefs.GetInt("MostGreenLights") +
                  "This is Escapes: " + mostRunEscapes +" = "+ PlayerPrefs.GetInt("MostEscapes")+
                  "This Passengers: " + mostPassengers +" = "+ PlayerPrefs.GetInt("MostPassengers")+
                  "This is Coins: " + mostRunCoins +" = "+  PlayerPrefs.GetInt("LongestCoinRun")+
                  "This is Distance: " + longestDistance +" = "+ PlayerPrefs.GetInt("HighScore")
        );
        Debug.Log("OverAll Calculations Done: " +PlayerPrefs.GetInt("OverAllScore") + "||" + overAllScore);
*/
        if (PlayerPrefs.GetInt("MLOverAllScore") < overAllScore)
        {
            //Debug.Log("Setting new over all score: ");
            PlayerPrefs.SetInt("MLOverAllScore", overAllScore);
            overAllScore = PlayerPrefs.GetInt("MLOverAllScore");
        }
        else
        {
            overAllScore = PlayerPrefs.GetInt("MLOverAllScore");
        }

        #endregion
        //PlayerPrefs.SetInt("OverAllScore",overAllScore );
       // Debug.Log("OverAll Calculations Done: " +PlayerPrefs.GetInt("OverAllScore") + "||" + overAllScore);
       
       
    }
}
