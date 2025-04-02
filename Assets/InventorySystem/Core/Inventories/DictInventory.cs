using System.Collections.Generic;
using InventorySystem.Core.Inventories;

namespace InventorySystem.Core
{
    public class DictInventory : Inventory
    {
        private Dictionary<string, int> _items = new();

        public override IReadOnlyDictionary<string, int> Items => _items;

        public DictInventory(Dictionary<string, int> items)
        {
            foreach (var pair in items)
            {
                _items.Add(pair.Key, pair.Value);
            }
        }
        
        public DictInventory() { }
        
        protected override void ProcessAddItem(string itemId, int count = 1)
        {
            if (_items.ContainsKey(itemId))
            {
                _items[itemId] += count;
            }
            else
            {
                _items.Add(itemId, count);
            }
        }

        protected override void ProcessRemoveItem(string itemId, int count = 1)
        {
            if (_items.ContainsKey(itemId))
            {
                _items[itemId] -= count;
                if (_items[itemId] < 0)
                {
                    _items[itemId] = 0;
                }
            }
        }

        public override int ItemCount(string itemId)
        {
            if (_items.TryGetValue(itemId, out var count))
            {
                return count;
            }
            else
            {
                return 0;
            }
        }
    }
}