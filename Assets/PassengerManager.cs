using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerManager : MonoBehaviour
{
    private void OnEnable()
    {

        Vector3 leftLane = new Vector3(-1.27f, transform.localPosition.y, transform.localPosition.z);
        Vector3 rightLane = new Vector3(1.27f, transform.localPosition.y, transform.localPosition.z);

        if (transform.parent.position.x == -4f)
        {
            transform.localPosition = leftLane;
            
           //Debug.Log(transform.localPosition.x);
        }


        if (transform.parent.position.x == 4f)
        {
            transform.localPosition = rightLane;
           
            //Debug.Log(transform.localPosition.x);
        }
    }
}
