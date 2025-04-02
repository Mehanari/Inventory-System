using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Core.Inventories
{
    public abstract class Inventory
    {
        public event Action<string, int> ItemAdded;
        public event Action<string, int> ItemRemoved;

        public void AddItem(string itemId, int count = 1)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                Debug.LogError("Cannot add item without id.");
                return;
            }
            if (count <= 0)
            {
                Debug.LogError("Cannot add negative (" + count + ") amount of items. Use RemoveItem for removing.");
            }
            ProcessAddItem(itemId, count);
            ItemAdded?.Invoke(itemId, count);
        }

        protected abstract void ProcessAddItem(string itemId, int count = 1);

        public void RemoveItem(string itemId, int count = 1)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                Debug.LogError("Cannot remove item without id.");
                return;
            }
            if (count <= 0)
            {
                Debug.LogError("Cannot remove negative (" + count + ") amount of items. Use AddItem for adding.");
            }
            ProcessRemoveItem(itemId, count);
            ItemRemoved?.Invoke(itemId, count);
        }

        protected abstract void ProcessRemoveItem(string itemId, int count = 1);
        
        public abstract int ItemCount(string itemId);
        
        public abstract IReadOnlyDictionary<string, int> Items { get; }
    }
}
