using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OpenWorldRobot : MonoBehaviour
{
    public GameObject RedLight;
    public GameObject GreenLight;
    public GameObject RobotTextObject;
    public bool RedLightON;
    private TMP_Text RobotText;
    float LightDuration;
    public GameObject ImageBAR;



    float Countdown;
    int CountdownInt;



    private void OnEnable()
    {
        //Set values
        LightDuration = 5;
        RobotText = RobotTextObject.GetComponent<TMP_Text>();
        SetLightVisuals(new Color32(0, 255, 0, 20));


    }

    void Update()
    {

        Countdown -= Time.deltaTime;

        CountdownInt = (int)Countdown + 1;
        RobotText.SetText(CountdownInt.ToString());

        if (Countdown < 0)
        {   
            Countdown = LightDuration;
            if(RedLightON == true)
            {
                CountToGreenComplete();
            }
            else
            {
                CountToRedComplete();
            }

        }
    }



    void CountToGreenComplete()
    {

        RedLightON = false;
        SetLightVisuals(new Color32(0, 255, 0, 67));

    }

    void CountToRedComplete()
    {
        RedLightON = true;
        SetLightVisuals(new Color32(0, 255, 0, 20));
    }

    void SetLightVisuals(Color32 ColorG)
    {
        if (RedLightON == true)
        {
            RedLight.SetActive(true);
            GreenLight.SetActive(false);
            ImageBAR.GetComponent<Image>().color = new Color32(255, 0, 0, 132);
            RobotTextObject.SetActive(true);

        }
        else
        {

            RedLight.SetActive(false);
            GreenLight.SetActive(true);
            ImageBAR.GetComponent<Image>().color = ColorG;
            RobotTextObject.SetActive(false);
        }
    }

}
