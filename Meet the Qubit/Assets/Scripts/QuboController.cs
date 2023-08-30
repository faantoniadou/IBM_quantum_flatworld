using UnityEngine;

public class QuboController : MonoBehaviour
{
    private Animator  anim;
    public Transform  quboTransform; // Reference to the Qubo's transform
    public GameObject blochSphere; // Reference to the Bloch Sphere GameObject, set this in the Unity Editor

    private float blochSphereRadius;

    private void Start()
    {
        anim = GetComponent<Animator>();
        blochSphereRadius = blochSphere.transform.localScale.x / 2; // Assuming the sphere is uniformly scaled
    }

    public void MoveToZeroState()
    {
        anim.Play("MoveToZeroState");
    }

    // This function will be called as an animation event
    public void OverrideQuboPosition()
    {
        Vector3 targetPosition = quboTransform.position.normalized * blochSphereRadius;
        quboTransform.position = targetPosition;
    }

    public void DisableAnimator()
    {
        anim.enabled = false;
    }

    // Function to trigger the jump animation
    public void TriggerJump()
    {
        anim.SetTrigger("Jump");
    }
}
