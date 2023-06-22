using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NarritiveSystem : MonoBehaviour
{
    public TMP_Text DialougeText;
    public GameObject NarritiveInterface;
    public string NI_Welcome;
    public string NI_PassengerTut;
    public string NI_RobotTut;
    public string NI_SteerringWheelTut;
    public string NI_1stCrash;
    public string crash;
    public string NI_ShopWelcome;
    public string NI_CaughtByCop;
    public string NI_1stHighScoreUploadedToLeaderboard;
    public string NI_1st5KCoins; 


    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("NI_Welcome")== 0)
        {
            PlayerPrefs.SetInt("NI_Welcome", 1);
            OpenInterface(NI_Welcome);
        }
    }

    // Update is called once per frame

    public void NI_PassengerTutFunction()
    {
        if (PlayerPrefs.GetInt("NI_PassengerTut") == 0)
        {
            OpenInterface(NI_PassengerTut);
            PlayerPrefs.SetInt("NI_PassengerTut", 1);
        }

    }

    public void NI_EnterRobotEvent()
    {

        if (PlayerPrefs.GetInt("NI_EnterRobot") == 0)
        {
            OpenInterface(NI_RobotTut);
            PlayerPrefs.SetInt("NI_EnterRobot", 1);
        }

    }

    public void NI_OpenSteeringwheel()
    {
        if (PlayerPrefs.GetInt("NI_SteeringWheeltut") == 0)
        {
            OpenInterface(NI_SteerringWheelTut);
            PlayerPrefs.SetInt("NI_SteeringWheeltut", 1);
        }
    }


    public void OpenInterface(string Text)
    {
        Time.timeScale = 0f;
        NarritiveInterface.SetActive(true);
        DialougeText.SetText(Text);
    }

    public void CloseInterface()
    {
        Time.timeScale = 1;
        NarritiveInterface.SetActive(false);
    }
}
