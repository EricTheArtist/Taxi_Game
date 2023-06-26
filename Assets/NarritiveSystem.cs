using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NarritiveSystem : MonoBehaviour
{
    public Image PromptImage;
    public Sprite C_TaxiBoss;
    public Sprite C_ShopOwner;
    public Sprite C_Police;
    public TMP_Text DialougeText;
    public GameObject NarritiveInterface;
    public string NI_Welcome; //has function
    public string NI_PassengerTut; //has function
    public string NI_RobotTut; //has function
    public string NI_SteerringWheelTut; //has function
    public string NI_1stCrash; //has function
    public string crash;
    public string NI_ShopWelcome;
    public string NI_CaughtByCop; //has function
    public string NI_1stHighScoreUploadedToLeaderboard; //this currently happens when they crash for first time
    public string NI_1st5KCoins; 


    // Start is called before the first frame update
    void Start()
    {
        
        if (PlayerPrefs.GetInt("NI_Welcome")== 0)
        {
            PlayerPrefs.SetInt("NI_Welcome", 1);
            OpenInterface(NI_Welcome,C_TaxiBoss);
        }
    }

    // Update is called once per frame

    public void NI_PassengerTutFunction()
    {
        if (PlayerPrefs.GetInt("NI_PassengerTut") == 0)
        {
            OpenInterface(NI_PassengerTut,C_TaxiBoss);
            PlayerPrefs.SetInt("NI_PassengerTut", 1);
        }

    }

    public void NI_EnterRobotEvent()
    {

        if (PlayerPrefs.GetInt("NI_EnterRobot") == 0)
        {
            OpenInterface(NI_RobotTut, C_Police);
            PlayerPrefs.SetInt("NI_EnterRobot", 1);
        }

    }

    public void NI_OpenSteeringwheel()
    {
        if (PlayerPrefs.GetInt("NI_SteeringWheeltut") == 0)
        {
            OpenInterface(NI_SteerringWheelTut, C_TaxiBoss);
            PlayerPrefs.SetInt("NI_SteeringWheeltut", 1);
        }
    }

    public void NI_Crashed()
    {
        if (PlayerPrefs.GetInt("NI_1stCrash") == 0)
        {
            OpenInterface(NI_1stCrash,C_ShopOwner);
            PlayerPrefs.SetInt("NI_1stCrash", 1);
        }
        
    }

    public void NI_CaughtAtRobot()
    {
        OpenInterface(NI_CaughtByCop,C_Police);
    }


    public void OpenInterface(string Text, Sprite Character)
    {
        Time.timeScale = 0f;
        NarritiveInterface.SetActive(true);
        DialougeText.SetText(Text);
        PromptImage.sprite = Character;
    }

    public void CloseInterface()
    {
        Time.timeScale = 1;
        NarritiveInterface.SetActive(false);
    }
}
