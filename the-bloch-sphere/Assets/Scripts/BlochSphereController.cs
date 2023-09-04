using UnityEngine;

public class BlochSphereController : MonoBehaviour
{
    private Quaternion originalRotation;
    public float rotationSpeed = 20f; // Speed of rotation, adjust this in the inspector if needed.
    private bool isRotating = true; // By default, the sphere rotates.

    private void Start()
    {
        // Save the original rotation of the Bloch sphere.
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        // Continuously rotate the Bloch sphere.
        if (isRotating)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    // Link this method to your specific button in Unity.
    // When the button is clicked, it'll stop the rotation and reset it.
    public void StopAndResetRotation()
    {
        isRotating = false;
        transform.rotation = originalRotation;
    }
}
