using UnityEngine;

[System.Serializable]
public class InventoryItem　　//オブジェクトにアタッチするものじゃないからMonoBehaviourはなし
{
    public string itemName;　　//アイテムの名前
    public string description;　　//アイテムの説明文
    public Sprite icon;　　　//アイテムのアイコン

    public int count = 1;　　//スタック数を数える変数
}