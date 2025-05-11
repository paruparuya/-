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
        Debug.Log("�A�C�e����I�����܂����F" + nameText.text);
        // ��������\��������A�g�p������
    }
}