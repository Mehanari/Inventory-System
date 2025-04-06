using System;
using System.Collections.Generic;
using ScriptableDatabase;
using UnityEngine;

namespace InventorySystem.Core.Recipes
{
    [Serializable]
    public class RecipeData : IUnique
    {
        [Serializable]
        private class IdToCount
        {
            public string id;
            public int count;
        }
        
        [SerializeField, HideInInspector] private string id;
        //Saving expenses dictionary as a list of tuples so Unity can serialize and deserialize it properly.
        [SerializeField, HideInInspector] private List<IdToCount> expenses;

        //This dictionary is lazily initialized. We do not mark it as SerializeField, because Unity cannot do serialize it
        private Dictionary<string, int> _expensesDict;

        public RecipeData(string id, Dictionary<string, int> expenses)
        {
            this.id = id;
            this.expenses = new();
            _expensesDict = expenses;
            foreach (var pair in expenses)
            {
                this.expenses.Add(new IdToCount{id = pair.Key, count = pair.Value});
            }
        }

        private Dictionary<string, int> InitializeDict()
        {
            var dict = new Dictionary<string, int>();
            foreach (var pair in expenses)
            {
                dict.Add(pair.id, pair.count);
            }

            _expensesDict = dict;
            return dict;
        }
        
        public string Id => id;
        public IReadOnlyDictionary<string, int> Expenses => _expensesDict ?? InitializeDict();

    }
}