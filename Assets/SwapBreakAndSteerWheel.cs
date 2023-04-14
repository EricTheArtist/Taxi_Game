using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapBreakAndSteerWheel : MonoBehaviour
{
    bool PadOnRight = true;
    public RectTransform BrakePad;
    public RectTransform SteeringWheel;

    public void ToggleBrakeAndWheel()
    {
        if (PadOnRight == true)
        {
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
