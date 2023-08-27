using UnityEngine;

public class QuboController : MonoBehaviour
{
    private Animator anim;
    public Transform quboTransform; // Reference to the Qubo's transform
    public float blochSphereRadius = 12.95124f;  // The radius of the Bloch sphere

    private void Start()
    {
        anim = GetComponent<Animator>();
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
}
