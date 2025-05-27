using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftRecipeDatabase", menuName = "Crafting/Recipe Database")]
public class CraftRecipeDatabase : ScriptableObject
{
    public List<CraftRecipe> recipes = new List<CraftRecipe>();
}