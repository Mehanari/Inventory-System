using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Core
{
    [CreateAssetMenu(menuName = "InventorySystem/Recipe", fileName = "DefaultRecipe")]
    public class Recipe : ScriptableObject, ISerializationCallbackReceiver
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

        public IReadOnlyDictionary<string, int> Expenses => _expensesDict;
        public string Id => id;
        
        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            ResetExpensesDict();
        }
        
        private void ResetExpensesDict()
        {
            _expensesDict.Clear();
            foreach (var expense in expenses)
            {
                if (_expensesDict.ContainsKey(expense.itemId))
                {
                    Debug.LogError("Item with id \"" + expense.itemId + "\" is already in the recipe. New instance with same id and count " + expense.count + " will be ignored.");
                }
                else
                {
                    _expensesDict.Add(expense.itemId, expense.count);
                }
            }
        }
    }
}