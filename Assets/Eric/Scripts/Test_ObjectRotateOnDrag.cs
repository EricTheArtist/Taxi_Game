using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ObjectRotateOnDrag : MonoBehaviour
{
    // The rotation speed of the object
    public float rotationSpeed = 1f;

    // The touch input to detect drag gestures
    private Touch touch;

    // The initial position of the touch input
    private Vector2 initialTouchPosition;

    // The current position of the touch input
    private Vector2 currentTouchPosition;

    // The horizontal distance between the initial and current touch positions
    private float horizontalDragDistance = 0f;

    //float draglimit = 0; 

    void Update()
    {
        // Check if there is one touch point on the screen
        if (Input.touchCount == 1)
        {
            // Store the touch input
            touch = Input.GetTouch(0);

            // Check if the touch input has just started
            if (touch.phase == TouchPhase.Began)
            {
                // Store the initial touch position
                initialTouchPosition = touch.position;
            }

            // Check if the touch input has moved
            if (touch.phase == TouchPhase.Moved)
            {
                // Store the current touch position
                currentTouchPosition = touch.position;

                // Calculate the horizontal drag distance between the initial and current touch positions
                horizontalDragDistance = currentTouchPosition.x - initialTouchPosition.x;

                // Calculate the rotation angle based on the horizontal drag distance
                float rotationAngle = horizontalDragDistance * rotationSpeed * Time.deltaTime;

                //Debug.Log(horizontalDragDistance);
                // Rotate the object around its Y-axis based on the rotation angle
                transform.Rotate(new Vector3(0, -rotationAngle, 0));
            }
        }
    }
}
