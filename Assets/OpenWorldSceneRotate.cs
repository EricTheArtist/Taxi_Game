using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldSceneRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
