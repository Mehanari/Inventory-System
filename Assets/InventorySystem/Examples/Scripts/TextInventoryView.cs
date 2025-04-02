using System.Text;
using InventorySystem.Core;
using InventorySystem.Core.Inventories;
using InventorySystem.Core.Items;
using TMPro;
using UnityEngine;
using Zenject;

namespace InventorySystem.Examples.Scripts
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextInventoryView : MonoBehaviour
    {
        private ItemsDatabase _itemsDatabase;
        private TextMeshProUGUI _textMesh;
        private Inventory _inventory;

        [Inject]
        private void Inject(Inventory inventory, ItemsDatabase itemsDatabase)
        {
            _inventory = inventory;
            _itemsDatabase = itemsDatabase;
        }

        private void Start()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
            _inventory.ItemAdded += OnItemAdded;
            _inventory.ItemRemoved += OnItemRemoved;
        }

        private void OnDestroy()
        {
            _inventory.ItemAdded -= OnItemAdded;
            _inventory.ItemRemoved -= OnItemRemoved;
        }

        private void OnItemAdded(string itemId, int count)
        {
            UpdateText();
        }

        private void OnItemRemoved(string itemId, int count)
        {
            UpdateText();
        }

        private void UpdateText()
        {
            var stringBuilder = new StringBuilder();
            foreach (var pair in _inventory.Items)
            {
                if (_itemsDatabase.ContainsData(pair.Key))
                {
                    var item = _itemsDatabase.GetData(pair.Key);
                    stringBuilder.Append(item.ItemName + ": " + pair.Value + "\n");
                }
                else
                {
                    Debug.LogWarning("Unknown itemFile with id: " + pair.Value);
                    stringBuilder.Append("UNKNOWN_ITEM: " + pair.Value + "\n");
                }
            }

            _textMesh.text = stringBuilder.ToString();
        }
    }
}
