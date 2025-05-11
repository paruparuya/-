using TMPro;
using UnityEngine;

public class ItemPickupUI : MonoBehaviour
{
    [SerializeField] private Canvas pickupCanvas; // ワールドスペースCanvas
    [SerializeField] private TextMeshProUGUI pickupText;

    private void Start()
    {
        ShowPickupUI(false); // 初期は非表示
    }

    public void ShowPickupUI(bool show)
    {
        if (pickupCanvas != null)
        {
            pickupCanvas.gameObject.SetActive(show);
        }
    }
}