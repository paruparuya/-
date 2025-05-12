using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false;

    public InventoryItem itemToGive; // 宝箱に入っているアイテム
    public GameObject interactUI; // 「開ける」などの表示用UI

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

        // アイテム追加処理（InventoryManager 経由）
        if (itemToGive != null)
        {
            InVentoryManeger.Instance.AddItem(itemToGive);
        }

        Debug.Log("宝箱を開けました！");
    }
}