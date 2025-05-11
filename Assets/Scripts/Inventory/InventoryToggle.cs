using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryToggle : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button firstSelectable; // ← 最初に選ばせたいボタン（InventoryItemUIのボタン）

    private MyControls controls;
    

    private bool inventoryOpen = false;

    private void Awake()
    {
        controls = new MyControls();
        controls.Player.Inventory.performed += _ => ToggleInventory();
        controls.UI.Inventory.performed += _ => ToggleInventory();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    private void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;
        inventoryPanel.SetActive(inventoryOpen);

        Debug.Log("インベントリ状態切替：" + inventoryOpen);

        if (inventoryOpen)
        {
            // アクションマップを UI に切り替え
            controls.Player.Disable();
            controls.UI.Enable();

            // 最初のボタンにフォーカスを当てる（後述）
            if (firstSelectable != null)
            {
                EventSystem.current.SetSelectedGameObject(firstSelectable.gameObject);
            }
        }
        else
        {
            // アクションマップを Player に戻す
            controls.UI.Disable();
            controls.Player.Enable();
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
    void Update()
    {
        
    }
}
