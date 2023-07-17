using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class Test_Robot : MonoBehaviour
{
    public GameObject RedLight;
    public GameObject GreenLight;
    public GameObject RobotTextObject;
    public bool RedLightON;
    private TMP_Text RobotText;
    float ChanceOfRed = 0.5f;
    float LightDuration;
    public GameObject ImageBAR;
    
    

    float Countdown;
    int CountdownInt;
    bool Donecounting = false;
    
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(Donecounting == false)
        {
            Countdown -= Time.deltaTime;
        }
        
        CountdownInt = (int)Countdown + 1;
        RobotText.SetText(CountdownInt.ToString());
        if(Countdown < 0 && Donecounting == false)
        {
            CountToGreenComplete();
            Donecounting = true;
            Countdown = LightDuration;

        }
    }



    void CountToGreenComplete()
    {
        
        RedLightON = false;
        SetLightVisuals(new Color32(0, 255, 0, 67));
        
    }

    void SetLightVisuals(Color32 ColorG)
    {
        if (RedLightON == true)
        {
            RedLight.SetActive(true);
            GreenLight.SetActive(false);
            ImageBAR.GetComponent<Image>().color = new Color32(255, 0, 0, 132);
            RobotTextObject.SetActive(true);

            Donecounting = false;
            Countdown = LightDuration;
        }
        else
        {
            Donecounting = true;
            RedLight.SetActive(false);
            GreenLight.SetActive(true);
            ImageBAR.GetComponent<Image>().color = ColorG;
            RobotTextObject.SetActive(false);
        }
    }

    private void OnEnable()
    {

        LightDuration = Random.Range(3,10);
        RobotText = RobotTextObject.GetComponent<TMP_Text>();
        if(Random.value > ChanceOfRed)
        {
            RedLightON = true;
        }
        else
        {
            RedLightON = false;
        }
        SetLightVisuals(new Color32(0, 255, 0, 20));
        
        
        //CountoGreen();
        
    }
}
