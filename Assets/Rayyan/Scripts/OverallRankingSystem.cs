using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallRankingSystem : MonoBehaviour
{

    //Distance | Passenger Count | Coin Run Collection | Most Escapes | Green Robots 
    private float longestDistance;
    private float mostPassengers;
    private float mostRunCoins;
    private float mostRunEscapes;
    private float mostGreenRobots;
    private float overAllScore;

    private CurrencySystem _currencySystem;
    private Mamello_PickUpSystem _pickUpSystem;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        _currencySystem = player.GetComponent<CurrencySystem>();
        _pickUpSystem = player.GetComponent<Mamello_PickUpSystem>();
    }

    public void ScoreCalculator()
    {
        //checks between the two distance scores and sets greatest one
        #region Set Longest Distance

        if (PlayerPrefs.GetInt("HighsScore") > PlayerPrefs.GetInt("HighSocreSteering"))
        {
            longestDistance = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            longestDistance = PlayerPrefs.GetInt("HighScoreSteeringt");
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
        overAllScore = (longestDistance + mostPassengers + mostRunCoins + mostRunEscapes + mostGreenRobots)/5;
        PlayerPrefs.SetFloat("OverAllScore",overAllScore );

       
       
    }
}
