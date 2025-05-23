using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    [SerializeField] private string keyID = "Key";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TryOpenChest()
    {
        if (isOpen)
            return;

        if(InVentoryManeger.Instance.HasItemWithID(keyID))
        {
            ToggleChest();
        }
        else
        {
            Debug.Log("鍵がないので開きません");
        }

    }

   
    public void ToggleChest()
    {
        isOpen = !isOpen;
        animator.SetBool("Open", isOpen);
        
       
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}
