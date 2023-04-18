using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeIconAnimation : MonoBehaviour
{
    //public GameObject playerVehicle;

    [SerializeField] private float xCurrentRotation;
    private float xAngleChange = 50f;

    public Quaternion defultBrakeRotation;
    //public Quaternion currentBrakeRotation;


    //public 

    private void Start()
    {
        defultBrakeRotation = transform.rotation;
        //playerVehicle.GetComponent<TestCharacterController>();
        //currentBrakeRotation = defultBrakeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        xCurrentRotation = transform.rotation.eulerAngles.x;

        if (Input.GetKey(KeyCode.Space)) //&& (transform.rotation.eulerAngles.x > 0f && transform.rotation.eulerAngles.x < 60f))
        {
            xCurrentRotation = Mathf.Clamp(xCurrentRotation, 0f, 60f);
            transform.Rotate(xAngleChange * Time.deltaTime, 0f, 0f, Space.World);
            
            /*if (xCurrentRotation > 0f && xCurrentRotation < 60f)
            {
                transform.Rotate(xAngleChange * Time.deltaTime, 0f, 0f, Space.World);
            }*/

        }
        else
        {
            transform.rotation = defultBrakeRotation;
        }
    }
}
