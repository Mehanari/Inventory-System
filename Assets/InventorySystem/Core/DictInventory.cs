using System.Collections.Generic;

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
        
        protected override void ProcessAddItem(Item item, int count = 1)
        {
            if (_items.ContainsKey(item.Id))
            {
                _items[item.Id] += count;
            }
            else
            {
                _items.Add(item.Id, count);
            }
        }

        protected override void ProcessRemoveItem(Item item, int count = 1)
        {
            if (_items.ContainsKey(item.Id))
            {
                _items[item.Id] -= count;
                if (_items[item.Id] < 0)
                {
                    _items[item.Id] = 0;
                }
            }
        }

        public override int ItemCount(Item item)
        {
            if (_items.TryGetValue(item.Id, out var count))
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