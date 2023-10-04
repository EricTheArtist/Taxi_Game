using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenWorldCrashDetection : MonoBehaviour
{
    public UnityEvent OWCrashEvent;
    private void OnTriggerEnter(Collider other)
    {
                
        if(other.gameObject.tag == "Obstruction")
        {
            OpenWorldGameOver.OWGOInstance.GameOver();
            OWCrashEvent.Invoke();
        }
        if(other.gameObject.tag == "XPcollider")
        {
            other.tag = "Pothole";
            PlayerLevelSystem.PLSinstance.AddXP(1);
            OpenWorldMissionSystem.OWMSinstance.AddObjHit();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
