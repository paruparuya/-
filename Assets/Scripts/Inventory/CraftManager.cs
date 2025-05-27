using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class CraftManager : MonoBehaviour
{
    public CraftRecipeDatabase recipeDatabase;
    public List<WorldItem> allItemPrefabs;　　
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void TryCraft(List<string> playerItemIDs)
    {
        foreach (CraftRecipe recipe in recipeDatabase.recipes)  //レシピを検索
        {
            bool canCraft = true;

            foreach (string requiredID in recipe.requiredItemIDs)  //素材を検索
            {
               
                if (!playerItemIDs.Contains(requiredID))　
                {
                    canCraft = false;
                    break;
                }
            }

            if (canCraft)  
            {
                foreach (string requiredID in recipe.requiredItemIDs)  //素材を検索
                {
                    InVentoryManeger.Instance.RemoveItemByID(requiredID);
                }

                WorldItem resultItem = allItemPrefabs.Find(item => item.id == recipe.resultItemID);
                if (resultItem != null)
                {
                    InventoryItem craftedItem = resultItem.CreateInventoryItem();
                    InVentoryManeger.Instance.AddItem(craftedItem);
                    Debug.Log("完成品をインベントリに追加: " + craftedItem.itemName);
                }
                else
                {
                    Debug.LogWarning("完成品のアイテムが見つかりませんでした: " + recipe.resultItemID);
                }

                return;
            }
        }

        Debug.Log("クラフトできません");
    }

    public void TryCraftFromInventory()
    {
        List<string> currentItemIDs = new List<string>();
        
        foreach (InventoryItem item in InVentoryManeger.Instance.items)
        {
            currentItemIDs.Add(item.id);
        }

        TryCraft(currentItemIDs);
    }
}
