using InventorySystem.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventorySystem.Examples.Scripts
{
    /// <summary>
    /// This is a script that makes a button add item of a specified id to a given inventory. 
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class GetItemButton : MonoBehaviour
    {
        [SerializeField] private string itemId;
        [SerializeField] private Image icon;
        [SerializeField] private Texture2D defaultTexture;
        
        private Inventory _inventory;
        private ItemsDatabase _itemsDatabase;
        [CanBeNull] private Item _item;
        
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
            if (_itemsDatabase.TryGetItem(itemId, out _item))
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
                Debug.LogError("Cannot process item with id \"" + itemId + "\", no item with this id in the database.");
            }
            else
            {
                _inventory.AddItem(_item);
            }
        }

        private void SetTextureToIcon(Texture2D texture)
        {
            var rect = new Rect(0, 0, texture.width, texture.height);
            icon.sprite = Sprite.Create(texture, rect, icon.sprite.pivot);
        }
    }
}
