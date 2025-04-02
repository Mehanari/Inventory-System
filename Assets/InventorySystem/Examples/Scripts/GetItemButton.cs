using InventorySystem.Core;
using InventorySystem.Core.Inventories;
using InventorySystem.Core.Items;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventorySystem.Examples.Scripts
{
    /// <summary>
    /// This is a script that makes a button add itemFile of a specified id to a given inventory. 
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class GetItemButton : MonoBehaviour
    {
        [SerializeField] private string itemId;
        [SerializeField] private Image icon;
        [SerializeField] private Texture2D defaultTexture;
        
        private Inventory _inventory;
        private ItemsDatabase _itemsDatabase;
        [CanBeNull] private ItemData _item;
        
        private Button _button;

        [Inject]
        private void Inject(Inventory inventory, ItemsDatabase itemsDatabase)
        {
            _inventory = inventory;
            _itemsDatabase = itemsDatabase;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            if (_itemsDatabase.TryGetData(itemId, out _item))
            {
                SetTextureToIcon(_item.Texture);
            }
            else
            {
                SetTextureToIcon(defaultTexture);
            }
            
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (_item is null)
            {
                Debug.LogError("Cannot process itemFile with id \"" + itemId + "\", no itemFile with this id in the database.");
            }
            else
            {
                _inventory.AddItem(_item.Id);
            }
        }

        private void SetTextureToIcon(Texture2D texture)
        {
            var rect = new Rect(0, 0, texture.width, texture.height);
            icon.sprite = Sprite.Create(texture, rect, icon.sprite.pivot);
        }
    }
}
