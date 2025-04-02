using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem.Core
{
    /// <summary>
    /// This class defines a set of items that can be used in the game.
    /// </summary>
    [CreateAssetMenu(fileName = "ItemsDatabase", menuName = "InventorySystem/ItemsDatabase")]
    public class ItemsDatabase : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<Item> items = new List<Item>();
        
        private Dictionary<string, Item> _itemsDict = new();

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            ResetItemsDictionary();
        }

        private void ResetItemsDictionary()
        {
            _itemsDict.Clear();
            foreach (var item in items)
            {
                if (item is null)
                {
                    continue;
                }
                if (!_itemsDict.TryAdd(item.Id, item))
                {
                    Debug.LogError("Item with ID \"" + item.Id + "\" already exists in the database with name  \"" + _itemsDict[item.Id].ItemName + "\".\nOther item with name \"" + item.ItemName + "\" won't be added!");
                }
            }
        }

        public string[] GetItemIds()
        {
            return _itemsDict.Keys.ToArray();
        }
        
        public Item GetItem(string id)
        {
            return _itemsDict[id];
        }
        
        public bool ContainsItem(string id)
        {
            return _itemsDict.ContainsKey(id);
        }

        public bool TryGetItem(string id, out Item item)
        {
            item = null;
            if (_itemsDict.TryGetValue(id, out var value))
            {
                item = value;
                return true;
            }

            return false;
        }
    }
}