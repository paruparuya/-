using System.Collections.Generic;
using UnityEngine;

public class InVentoryManeger : MonoBehaviour
{
    public static InVentoryManeger Instance; // シングルトンにして他スクリプトから呼びやすく

    public List<InventoryItem> items = new List<InventoryItem>(); // 保存するアイテムリスト

    [Header("UI 関連")]
    [SerializeField] private Transform itemGrid; // GridのTransform
    [SerializeField] private GameObject itemUIPrefab; // InventoryItemUIのプレハブ

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
        Debug.Log($"{item.itemName} をインベントリに追加しました");

        // UIに追加
        GameObject uiObj = Instantiate(itemUIPrefab, itemGrid);
        InventoryItemUI ui = uiObj.GetComponent<InventoryItemUI>();
        if (ui != null)
        {
            ui.Setup(item);
        }
        else
        {
            Debug.LogWarning("InventoryItemUI スクリプトがプレハブに付いてません！");
        }

    }
    void Update()
    {
        
    }
}
