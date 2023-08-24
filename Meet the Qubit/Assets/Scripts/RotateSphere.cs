using UnityEngine;

public class RotateAutomatically : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    public Vector3 rotationAxis = Vector3.up; // default rotation around the Y-axis

    private bool isUserRotating = false;

    void Update()
    {
        if (!isUserRotating)
            transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }

    public void UserStartedRotating()
    {
        isUserRotating = true;
    }

    public void UserStoppedRotating()
    {
        isUserRotating = false;
    }
}
