using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoinRotator : MonoBehaviour
{


    public GameObject[] objectsToRotate;   // Array of objects to rotate

    void Start()
    {

    }

    void Update()
    {

        foreach (GameObject obj in objectsToRotate)
        {

            float distance = Vector3.Distance(transform.position, obj.transform.position);

            float distanceclap = Mathf.Clamp(distance, 0, 1f);

            if(distance < 10)
            {
                obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x,obj.transform.eulerAngles.y + distanceclap, obj.transform.eulerAngles.z);
            }
            else if(distance < 50)
            {
                obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y + 0.5f, obj.transform.eulerAngles.z);
            }
            else
            {
                obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, 0, obj.transform.eulerAngles.z);
            }
            
        }
    }
}
