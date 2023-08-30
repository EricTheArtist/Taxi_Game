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
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
