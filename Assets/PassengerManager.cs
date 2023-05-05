using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerManager : MonoBehaviour
{

    private void OnEnable()
    {
        Vector3 leftLane = new Vector3(-1.27f, transform.GetChild(1).localPosition.y, transform.GetChild(1).localPosition.z);
        Vector3 rightLane = new Vector3(1.27f, transform.GetChild(1).localPosition.y, transform.GetChild(1).localPosition.z);

        Debug.Log(transform.GetChild(1).localPosition.x);
        //transform.GetChild(1).localPosition = new Vector3(transform.localPosition.x, 5, transform.localPosition.z);
        //transform.GetChild(1).localPosition = leftLane;
        /*if (transform.position.x == -4f)
        {
            
            //Debug.Log(transform.GetChild(1).position.x);
            transform.GetChild(1).localPosition = leftLane;
            //Debug.Log(transform.GetChild(1).localPosition.x);

        }

        if (transform.position.x == 4f)
        {

            //Debug.Log(transform.GetChild(1).position.x);
           transform.GetChild(1).localPosition = rightLane;
           //Debug.Log(transform.GetChild(1).localPosition.x);
        }*/
    }
}
