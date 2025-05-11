using System.Collections.Generic;
using UnityEngine;

public class InVentoryManeger : MonoBehaviour
{
    public static InVentoryManeger Instance; // �V���O���g���ɂ��đ��X�N���v�g����Ăт₷��

    public List<InventoryItem> items = new List<InventoryItem>(); // �ۑ�����A�C�e�����X�g

    [Header("UI �֘A")]
    [SerializeField] private Transform itemGrid; // Grid��Transform
    [SerializeField] private GameObject itemUIPrefab; // InventoryItemUI�̃v���n�u

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(InventoryItem item)
    {
        items.Add(item);
        Debug.Log($"{item.itemName} ���C���x���g���ɒǉ����܂���");

        // UI�ɒǉ�
        GameObject uiObj = Instantiate(itemUIPrefab, itemGrid);
        InventoryItemUI ui = uiObj.GetComponent<InventoryItemUI>();
        if (ui != null)
        {
            ui.Setup(item);
        }
        else
        {
            Debug.LogWarning("InventoryItemUI �X�N���v�g���v���n�u�ɕt���Ă܂���I");
        }

    }
    void Update()
    {
        
    }
}
