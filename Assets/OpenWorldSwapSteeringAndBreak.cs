using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldSwapSteeringAndBreak : MonoBehaviour
{


    bool PadOnRight = true;
    public RectTransform BrakePad;
    public RectTransform SteeringWheel;

    private void Start()
    {
        int setup = PlayerPrefs.GetInt("MLPadLocation");
        if(setup == 1)
        {
            ToggleBrakeAndWheel();
        }

    }

    public void ToggleBrakeAndWheel()
    {
        if (PadOnRight == true)
        {
            PlayerPrefs.SetInt("MLPadLocation", 1);
            BrakePad.anchorMin = new Vector2(0, 0);
            BrakePad.anchorMax = new Vector2(0, 0);
            BrakePad.pivot = new Vector2(0, 0);

            SteeringWheel.anchorMin = new Vector2(1, 0);
            SteeringWheel.anchorMax = new Vector2(1, 0);
            SteeringWheel.pivot = new Vector2(1, 0);

            PadOnRight = false;
        }
        else
        {
            PlayerPrefs.SetInt("MLPadLocation", 0);
            BrakePad.anchorMin = new Vector2(1, 0);
            BrakePad.anchorMax = new Vector2(1, 0);
            BrakePad.pivot = new Vector2(1, 0);

            SteeringWheel.anchorMin = new Vector2(0, 0);
            SteeringWheel.anchorMax = new Vector2(0, 0);
            SteeringWheel.pivot = new Vector2(0, 0);

            PadOnRight = true;
        }
    }
}
