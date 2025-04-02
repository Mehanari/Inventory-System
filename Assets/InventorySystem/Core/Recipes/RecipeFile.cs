using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Core.Recipes
{
    /// <summary>
    /// The purpose of this script is to provide a handy way to edit and create new recipes.
    /// It provides validation and can be used in recipes database.
    /// </summary>
    [CreateAssetMenu(menuName = "InventorySystem/Recipe", fileName = "DefaultRecipe")]
    public class RecipeFile : DataFile<RecipeData>, ISerializationCallbackReceiver
    {
        [Serializable]
        private class ItemToCount
        {
            public string itemId;
            public int count;
        }
        
        [SerializeField] private string id;
        [SerializeField] private List<ItemToCount> expenses = new();

        private Dictionary<string, int> _expensesDict = new();
        
        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            ResetExpensesDict();
        }
        
        private void ResetExpensesDict()
        {
            _expensesDict.Clear();
            for (int i = 0; i < expenses.Count; i++)
            {
                var expense = expenses[i];
                if (_expensesDict.ContainsKey(expense.itemId))
                {
                    Debug.LogError("ItemFile with id \"" + expense.itemId + "\" is already in the recipe. New instance with same id and count " + expense.count + " will be ignored.\nEntry number in list: " + (i+1) + ".");
                }
                else
                {
                    _expensesDict.Add(expense.itemId, expense.count);
                }
            }
        }

        public override RecipeData Data => new(id, _expensesDict);
    }
}