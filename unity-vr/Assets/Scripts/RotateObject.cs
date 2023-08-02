using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private Vector3 lastMousePosition; // To store the last position of the mouse
    public float rotationSpeed = 50f;  // Speed of rotation

    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition; // Store the initial mouse position
        }
        else if (Input.GetMouseButton(0))
        {
            // Calculate the difference in mouse movement
            Vector3 delta = Input.mousePosition - lastMousePosition;
            lastMousePosition = Input.mousePosition;

            // Rotate the object based on mouse movement
            transform.Rotate(Vector3.down * delta.x * rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right * delta.y * rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
