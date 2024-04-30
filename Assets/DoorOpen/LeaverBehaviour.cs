using UnityEngine;

public class LeverBehaviour : MonoBehaviour
{
    // Load Animator components
    public Animator doorAnimator; 
    public Animator leverAnimator; 

    private bool hasActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasActivated)
        {

            doorAnimator.SetBool("Open", true);
            leverAnimator.SetBool("Switch", true);
            hasActivated = true;
        }
    }
}
