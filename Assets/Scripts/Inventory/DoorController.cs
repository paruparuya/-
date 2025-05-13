using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        animator.SetBool("Open", isOpen);
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}
