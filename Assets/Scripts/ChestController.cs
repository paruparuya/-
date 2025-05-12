using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false;

    public InventoryItem itemToGive; // �󔠂ɓ����Ă���A�C�e��
    public GameObject interactUI; // �u�J����v�Ȃǂ̕\���pUI

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (interactUI != null) interactUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            if (interactUI != null) interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactUI != null) interactUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isOpened && interactUI != null && interactUI.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        animator.SetTrigger("Open");
        isOpened = true;
        interactUI.SetActive(false);

        // �A�C�e���ǉ������iInventoryManager �o�R�j
        if (itemToGive != null)
        {
            InVentoryManeger.Instance.AddItem(itemToGive);
        }

        Debug.Log("�󔠂��J���܂����I");
    }
}