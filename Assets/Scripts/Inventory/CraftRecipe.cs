using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CraftRecipe
{
    public List<string> requiredItemIDs;  //必要素材
    public string resultItemID;  　       //クラフト結果
}