using UnityEngine;

public class QuboController : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void MoveToZeroState()
    {
        anim.Play("MoveToZeroState");
    }
}
