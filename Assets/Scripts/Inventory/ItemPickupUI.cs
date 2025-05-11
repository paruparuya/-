using TMPro;
using UnityEngine;

public class ItemPickupUI : MonoBehaviour
{
    [SerializeField] private Canvas pickupCanvas; // ���[���h�X�y�[�XCanvas
    [SerializeField] private TextMeshProUGUI pickupText;

    private void Start()
    {
        ShowPickupUI(false); // �����͔�\��
    }

    public void ShowPickupUI(bool show)
    {
        if (pickupCanvas != null)
        {
            pickupCanvas.gameObject.SetActive(show);
        }
    }
}