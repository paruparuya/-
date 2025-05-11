using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryToggle : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button firstSelectable; // �� �ŏ��ɑI�΂������{�^���iInventoryItemUI�̃{�^���j

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

        Debug.Log("�C���x���g����ԐؑցF" + inventoryOpen);

        if (inventoryOpen)
        {
            // �A�N�V�����}�b�v�� UI �ɐ؂�ւ�
            controls.Player.Disable();
            controls.UI.Enable();

            // �ŏ��̃{�^���Ƀt�H�[�J�X�𓖂Ă�i��q�j
            if (firstSelectable != null)
            {
                EventSystem.current.SetSelectedGameObject(firstSelectable.gameObject);
            }
        }
        else
        {
            // �A�N�V�����}�b�v�� Player �ɖ߂�
            controls.UI.Disable();
            controls.Player.Enable();
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
    void Update()
    {
        
    }
}
