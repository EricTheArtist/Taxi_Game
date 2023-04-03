using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ShopZoomandRotate : MonoBehaviour
{
    // The speed of the zoom operation
    public float zoomSpeed = 0.5f;

    // The minimum and maximum distance between the camera and the target object
    public float minDistance = 1.0f;
    public float maxDistance = 10.0f;

    // The touch inputs to detect pinch gestures
    private Touch touchZero;
    private Touch touchOne;

    // The initial distance between the two touch points
    private float initialDistance = 0.0f;

    // The initial position of the camera
    private Vector3 initialPosition;

    // The GameObject to move towards and away from
    public GameObject targetObject;

    float LepAmt = 0; 
    void Start()
    {
        // Store the initial position of the camera
        initialPosition = transform.position;
    }

    void Update()
    {
        // Check if there are two touch points on the screen
        if (Input.touchCount == 2)
        {
            // Store the touch inputs
            touchZero = Input.GetTouch(0);
            touchOne = Input.GetTouch(1);

            // Check if the touch inputs have just started
            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                // Calculate the initial distance between the two touch points
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
            }

            // Check if the touch inputs have moved
            if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
            {
                // Calculate the new distance between the two touch points
                float newDistance = Vector2.Distance(touchZero.position, touchOne.position);

                // Calculate the zoom factor based on the new and initial distances
                float zoomFactor = newDistance / initialDistance;

                Debug.Log(zoomFactor);
                // Invert the zoom factor to move the camera in the opposite direction of the pinch
                //zoomFactor = 1 / zoomFactor;

                // Clamp the zoom factor between the minimum and maximum distance levels
                float distance = Vector3.Distance(transform.position, targetObject.transform.position);
                //float targetDistance = distance * zoomFactor;
                //targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);

                Debug.Log("Distance: " + distance);

                if (zoomFactor>1 && distance>minDistance)
                {
                    //Debug.Log("ZoomIN");
                    //float LepAmt = 0;
                    LepAmt += zoomSpeed * Time.deltaTime;
                    Vector3 newPosition = Vector3.Lerp(initialPosition, targetObject.transform.position, LepAmt);
                    // Move the camera to the new position
                    transform.position = newPosition;
                }
                else if (zoomFactor < 1 && distance < maxDistance)
                {
                    //Debug.Log("ZoomOUT");
                    //float LepAmt = 0;
                    LepAmt -= zoomSpeed * Time.deltaTime;
                    Vector3 newPosition = Vector3.Lerp(initialPosition, targetObject.transform.position, LepAmt);
                    // Move the camera to the new position
                    transform.position = newPosition;
                }
                // Calculate the new position of the camera based on the target object and zoom factor
                 //targetObject.transform.position + (transform.position - targetObject.transform.position).normalized * targetDistance;


            }

            // Check if the touch inputs have ended
            if (touchZero.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Ended)
            {
                // Reset the initial distance between the two touch points
                initialDistance = 0.0f;
                LepAmt = 0;
            }
        }
    }

}
