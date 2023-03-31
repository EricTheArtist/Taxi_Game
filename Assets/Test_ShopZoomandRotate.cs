using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ShopZoomandRotate : MonoBehaviour
{
    // The speed of the zoom and rotate operations
    public float zoomSpeed = 0.5f;
    public float rotateSpeed = 0.5f;

    // The minimum and maximum zoom levels
    public float minZoom = 1.0f;
    public float maxZoom = 10.0f;

    // The touch inputs to detect pinch and drag gestures
    private Touch touchZero;
    private Touch touchOne;

    // The initial distance between the two touch points
    private float initialDistance = 0.0f;

    // The initial rotation of the camera
    private Quaternion initialRotation;

    // The GameObject to zoom and rotate towards
    public GameObject targetObject;

    void Start()
    {
        // Store the initial rotation of the camera
        initialRotation = transform.rotation;
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

                // Clamp the zoom factor between the minimum and maximum zoom levels
                zoomFactor = Mathf.Clamp(zoomFactor, minZoom, maxZoom);

                // Calculate the new position of the camera based on the zoom factor and target object
                Vector3 newPosition = Vector3.Lerp(transform.position, targetObject.transform.position, zoomFactor);

                // Move the camera to the new position
                transform.position = newPosition;
            }

            // Check if the touch inputs have ended
            if (touchZero.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Ended)
            {
                // Reset the initial distance between the two touch points
                initialDistance = 0.0f;
            }
        }
        else if (Input.touchCount == 1)
        {
            // Get the touch input
            Touch touch = Input.GetTouch(0);

            // Check if the touch input has just started
            if (touch.phase == TouchPhase.Began)
            {
                // Store the initial rotation of the camera
                initialRotation = transform.rotation;
            }

            // Check if the touch input has moved
            if (touch.phase == TouchPhase.Moved)
            {
                // Calculate the rotation based on the touch input's movement
                float rotateX = touch.deltaPosition.x * rotateSpeed;
                float rotateY = touch.deltaPosition.y * rotateSpeed;

                // Apply the rotation to the camera's transform
                transform.rotation = initialRotation * Quaternion.Euler(-rotateY, rotateX, 0);
            }
        }
    }

}
