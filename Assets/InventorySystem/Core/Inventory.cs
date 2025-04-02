using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Core
{
    public abstract class Inventory
    {
        public event Action<Item, int> ItemAdded;
        public event Action<Item, int> ItemRemoved;

        public void AddItem(Item item, int count = 1)
        {
            if (item is null)
            {
                Debug.LogError("Cannot add null item.");
                return;
            }
            if (count <= 0)
            {
                Debug.LogError("Cannot add negative (" + count + ") amount of items. Use RemoveItem for removing.");
            }
            ProcessAddItem(item, count);
            ItemAdded?.Invoke(item, count);
        }

        protected abstract void ProcessAddItem(Item item, int count = 1);

        public void RemoveItem(Item item, int count = 1)
        {
            if (item is null)
            {
                Debug.LogError("Cannot remove null item.");
                return;
            }
            if (count <= 0)
            {
                Debug.LogError("Cannot remove negative (" + count + ") amount of items. Use AddItem for adding.");
            }
            ProcessRemoveItem(item, count);
            ItemRemoved?.Invoke(item, count);
        }

        protected abstract void ProcessRemoveItem(Item item, int count = 1);
        
        public abstract int ItemCount(Item item);
        
        public abstract IReadOnlyDictionary<string, int> Items { get; }
    }
}
