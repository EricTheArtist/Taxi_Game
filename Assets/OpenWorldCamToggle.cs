using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldCamToggle : MonoBehaviour
{
    public Camera MainCam;
    public Camera CinemaCam;
    // Start is called before the first frame update
    void Start()
    {
        MainCam.enabled = true;
        CinemaCam.enabled = false;
    }

    // Update is called once per frame
    public void TogggleCameras()
    {
        if(MainCam.enabled == true)
        {
            MainCam.enabled = false;
            CinemaCam.enabled = true;
        }
        else{
            MainCam.enabled = true;
            CinemaCam.enabled = false;
        }
    }
}
