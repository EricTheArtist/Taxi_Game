using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoinRotator : MonoBehaviour
{

    //public string objectTag;   // The tag of the objects to rotate
    //float rotationSpeed = 0f;   // The speed at which to rotate the objects
    public GameObject[] objectsToRotate;   // Array of objects to rotate

    void Start()
    {
        // Find all objects with the specified tag and add them to the array
        //objectsToRotate = GameObject.FindGameObjectsWithTag(objectTag);
    }

    void Update()
    {
        // Loop through the array and rotate each object
        foreach (GameObject obj in objectsToRotate)
        {
            // Calculate the distance between this object and the object to rotate
            float distance = Vector3.Distance(transform.position, obj.transform.position);

            float distanceclap = Mathf.Clamp(distance, 0, 1f);
            // Set the rotation of the object
            //obj.transform.Rotate(0f, rotationSpeed + distanceclap/10, 0f, Space.World);
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
            

            //obj.transform.localRotation = Quaternion.Euler(0f, distance * rotationSpeed, 0f);
        }
    }
}
