using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HorizontalModeManager : MonoBehaviour
{

    public Color32 ActiveColor;
    public Color32 InactiveColor;

    public Image SteerigActiveImage;
    public bool SteeringWheelActive = false;

    public UnityEvent Entersteering;

    public SimpleInputNamespace.TestCharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        if(Screen.orientation == ScreenOrientation.Portrait)
        {

        }
        else if(Screen.orientation == ScreenOrientation.LandscapeLeft)
        {
            ToggleSteerigwheel();
        }
        else if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            ToggleSteerigwheel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSteerigwheel()
    {
        controller.ToggleSteeringWheel();

        if (SteeringWheelActive == true)
        {
            Screen.orientation = ScreenOrientation.Portrait;
            SteeringWheelActive = false;
            SteerigActiveImage.color = InactiveColor;

        }
        else
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            Entersteering.Invoke();
            SteeringWheelActive = true;
            SteerigActiveImage.color = ActiveColor;
        }

    }

}
