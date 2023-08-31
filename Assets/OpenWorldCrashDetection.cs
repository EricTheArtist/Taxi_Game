using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldCrashDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
                
        if(other.gameObject.tag == "Obstruction")
        {
            OpenWorldGameOver.OWGOInstance.GameOver();
        }
        if(other.gameObject.tag == "XPcollider")
        {
            other.tag = "Pothole";
            PlayerLevelSystem.PLSinstance.AddXP(1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
