using ScriptableDatabase;
using UnityEngine;

namespace InventorySystem.Core.Recipes
{
    [CreateAssetMenu(fileName = "RecipeDatabase", menuName = "InventorySystem/RecipeDatabase")]
    public class RecipeDatabase : Database<RecipeData> { }
}