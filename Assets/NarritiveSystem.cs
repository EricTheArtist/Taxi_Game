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
    public string NI_CrashWithCop;
    public GameObject GameModePointer;


    // Start is called before the first frame update
    void Start()
    {
        
        if (PlayerPrefs.GetInt("MLNI_Welcome") == 0)
        {
            PlayerPrefs.SetInt("MLNI_Welcome", 1);
            OpenInterface(NI_Welcome,C_TaxiBoss);
        }
        if (PlayerPrefs.GetInt("MLNI_GameModePointer") == 1)
        {
            Destroy(GameModePointer);
        }
    }

    public void NI_selectgamemode()
    {
        Destroy(GameModePointer);
        PlayerPrefs.SetInt("MLNI_GameModePointer", 1);

    }

    // Update is called once per frame

    public void NI_PassengerTutFunction()
    {
        if (PlayerPrefs.GetInt("MLNI_PassengerTut") == 0)
        {
            OpenInterface(NI_PassengerTut,C_TaxiBoss);
            PlayerPrefs.SetInt("MLNI_PassengerTut", 1);
        }

    }

    public void NI_EnterRobotEvent()
    {

        if (PlayerPrefs.GetInt("MLNI_EnterRobot") == 0)
        {
            OpenInterface(NI_RobotTut, C_Police);
            PlayerPrefs.SetInt("MLNI_EnterRobot", 1);
        }

    }

    public void NI_OpenSteeringwheel()
    {
        if (PlayerPrefs.GetInt("MLNI_SteeringWheeltut") == 0)
        {
            OpenInterface(NI_SteerringWheelTut, C_TaxiBoss);
            PlayerPrefs.SetInt("MLNI_SteeringWheeltut", 1);
        }
    }

    public void NI_Crashed()
    {
        if (PlayerPrefs.GetInt("MLNI_1stCrash") == 0)
        {
            OpenInterface(NI_1stCrash,C_ShopOwner);
            PlayerPrefs.SetInt("MLNI_1stCrash", 1);
        }
        
    }

    public void NI_CaughtAtRobot()
    {
        OpenInterface(NI_CaughtByCop,C_Police);
    }

    public void NI_CrashWithCopChase()
    {
        OpenInterface(NI_CrashWithCop,C_Police);
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
