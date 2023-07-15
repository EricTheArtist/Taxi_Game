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

        if (PlayerPrefs.GetInt("HighScore") > PlayerPrefs.GetInt("HighScoreSteering"))
        {
            longestDistance = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            longestDistance = PlayerPrefs.GetInt("HighScoreSteering");
        }

        #endregion
        
        //checks at end of run if run amount is greater than what is currently stored/recorded
        #region Set Most Coin Runs

        if (PlayerPrefs.GetInt("LongestCoinRun") < _currencySystem.run_amount)
        {
            PlayerPrefs.SetInt("LongestCoinRun", _currencySystem.run_amount);
            mostRunCoins = PlayerPrefs.GetInt("LongestCoinRun");
        }
        else
        {
            mostRunCoins = PlayerPrefs.GetInt("LongestCoinRun");
        }
        #endregion
        
        //Sets Most Passenger Pickups
        #region Set Most Passenger Pickups

        if (PlayerPrefs.GetInt("MostPassengers") < _pickUpSystem.passengerCount)
        {
            PlayerPrefs.SetInt("MostPassengers", _pickUpSystem.passengerCount);
            mostPassengers = PlayerPrefs.GetInt("MostPassengers");
        }
        else
        {
            mostPassengers = PlayerPrefs.GetInt("MostPassengers");
        }

        #endregion
        
        //SetsMostEscapes

        #region  Set Most Cop Escapes

        if (PlayerPrefs.GetInt("MostEscapes") < _pickUpSystem.copsEscaped)
        {
            PlayerPrefs.SetInt("MostEscapes", _pickUpSystem.copsEscaped);
            mostRunEscapes = PlayerPrefs.GetInt("MostEscapes");
        }
        else
        {
            mostRunEscapes = PlayerPrefs.GetInt("MostEscapes");
        }

        #endregion
        
        //SetsMostGreenLights

        #region Set Most Green Lights Passed

        if (PlayerPrefs.GetInt("MostGreenLights") < _pickUpSystem.greenLightPassed)
        {
            PlayerPrefs.SetInt("MostGreenLights", _pickUpSystem.greenLightPassed);
            mostGreenRobots = PlayerPrefs.GetInt("MostGreenLights");
        }
        else
        {
            mostGreenRobots = PlayerPrefs.GetInt("MostGreenLights");
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
        if (PlayerPrefs.GetInt("OverAllScore") < overAllScore)
        {
            //Debug.Log("Setting new over all score: ");
            PlayerPrefs.SetInt("OverAllScore", overAllScore);
            overAllScore = PlayerPrefs.GetInt("OverAllScore");
        }
        else
        {
            overAllScore = PlayerPrefs.GetInt("OverAllScore");
        }

        #endregion
        //PlayerPrefs.SetInt("OverAllScore",overAllScore );
       // Debug.Log("OverAll Calculations Done: " +PlayerPrefs.GetInt("OverAllScore") + "||" + overAllScore);
       
       
    }
}
