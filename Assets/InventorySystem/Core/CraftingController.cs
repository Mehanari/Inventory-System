using InventorySystem.Core.Inventories;
using InventorySystem.Core.Items;
using InventorySystem.Core.Recipes;
using UnityEngine;

namespace InventorySystem.Core
{
    public class CraftingController
    {
        private ItemsDatabase _itemsDatabase;

        public CraftingController(ItemsDatabase itemsDatabase)
        {
            _itemsDatabase = itemsDatabase;
        }

        public bool CanCraft(Inventory inventory, RecipeData recipe)
        {
            var enoughResources = true;
            foreach (var expense in recipe.Expenses)
            {
                if (_itemsDatabase.TryGetData(expense.Key, out var item))
                {
                    var owned = inventory.ItemCount(item.Id);
                    var needed = expense.Value;
                    if (needed > owned)
                    {
                        enoughResources = false;
                    }
                }
                else
                {
                    enoughResources = false;
                    Debug.LogError("Unknown item with id \"" + expense.Key + "\" in a recipe with id \"" + recipe.Id + "\".");
                }
            }

            return enoughResources;
        }

        /// <summary>
        /// Takes resources from the inventory if it is possible to craft a recipe and returns true.
        /// Otherwise, return false.
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public bool Craft(Inventory inventory, RecipeData recipe)
        {
            if (!CanCraft(inventory, recipe)) return false;
            foreach (var expense in recipe.Expenses)
            {
                var item = _itemsDatabase.GetData(expense.Key);
                inventory.RemoveItem(item.Id, expense.Value);
            }

            return true;
        }
    }
}