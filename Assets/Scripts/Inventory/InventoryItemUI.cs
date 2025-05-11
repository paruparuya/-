using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;  
    [SerializeField] private TMP_Text nameText;

    private InventoryItem storedItem;


    public void Setup(InventoryItem item)
    {
       
        storedItem = item;
        iconImage.sprite = item.icon;
        nameText.text = item.itemName;
    }
    public void OnClick()
    {
        Debug.Log("アイテムを選択しました：" + nameText.text);
        // 説明文を表示したり、使用したり
    }
}