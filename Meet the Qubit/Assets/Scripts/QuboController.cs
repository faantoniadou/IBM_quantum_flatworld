using UnityEngine;
using System.Collections;

public class QuboController : MonoBehaviour
{
    private Animator anim;
    public Transform quboTransform;
    public GameObject blochSphere;
    private float blochSphereRadius;

    private int jumpCounter = 0; // Added this to count the number of jumps
    private const int maxJumps = 2; // Maximum allowed jumps

    private void Start()
    {
        anim = GetComponent<Animator>();
        blochSphereRadius = blochSphere.transform.localScale.x / 2;
    }

    public void TriggerJump()
    {
        if (jumpCounter < maxJumps)
        {
            StartCoroutine(DelayedJump());
        }
    }

    private IEnumerator DelayedJump()
    {
        yield return new WaitForSeconds(0.2f);  // wait for half a second

        if (gameObject.activeInHierarchy)
        {
            if (anim != null)
            {
                anim.SetTrigger("Jump");
                jumpCounter++;
            }
        }
    }

    public void MoveToZeroState()
    {
        anim.Play("MoveToZeroState");
    }

    public void OverrideQuboPosition()
    {
        Vector3 targetPosition = quboTransform.position.normalized * blochSphereRadius;
        quboTransform.position = targetPosition;
    }

    public void DisableAnimator()
    {
        anim.enabled = false;
    }

    // Call this function when you want to reset the jump counter, perhaps when Qubo reappears
    public void ResetJumpCounter()
    {
        jumpCounter = 0;
    }
}
