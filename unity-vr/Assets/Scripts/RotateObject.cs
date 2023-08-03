using UnityEngine;
using UnityEngine.EventSystems;

public class RotateObject : MonoBehaviour
{
    private Vector3 lastMousePosition; // To store the last position of the mouse
    public float rotationSpeed = 50f;  // Speed of rotation
    public float autoRotateSpeed = 20f; // Speed of automatic rotation

    private bool autoRotate = true; // Flag to check if the object should rotate automatically

    void Update()
    {
        // Rotate the object automatically along its x-axis
        if (autoRotate)
        {
            transform.Rotate(Vector3.up * autoRotateSpeed * Time.deltaTime, Space.World);
        }

        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Transform clickedTransform = hit.transform;
                Transform parentTransform = clickedTransform.parent;

                // Check if the clicked object's parent is "quantum_0"
                if (parentTransform != null && parentTransform.name == "quantum_0")
                {
                    autoRotate = false; // Stop automatic rotation
                    lastMousePosition = Input.mousePosition; // Store the initial mouse position
                }
            }
        }
        else if (Input.GetMouseButton(0) && !autoRotate && !EventSystem.current.IsPointerOverGameObject())
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
