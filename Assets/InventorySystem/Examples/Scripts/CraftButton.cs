using InventorySystem.Core;
using InventorySystem.Core.Inventories;
using InventorySystem.Core.Items;
using InventorySystem.Core.Recipes;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventorySystem.Examples.Scripts
{
    [RequireComponent(typeof(Button))]
    public class CraftButton : MonoBehaviour
    {
        [SerializeField] private string recipeId;
        [SerializeField] private string craftedItemId;
        
        private Inventory _inventory;
        private ItemsDatabase _itemsDatabase;
        private RecipeDatabase _recipeDatabase;
        [CanBeNull] private RecipeData _recipe;
        [CanBeNull] private ItemData _craftedItem;

        private Button _button;
        private CraftingController _controller;
        
        [Inject]
        private void Inject(Inventory inventory, ItemsDatabase itemsDatabase, RecipeDatabase recipeDatabase)
        {
            _inventory = inventory;
            _itemsDatabase = itemsDatabase;
            _recipeDatabase = recipeDatabase;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _controller = new CraftingController(_itemsDatabase);
        }

        private void Start()
        {
            _itemsDatabase.TryGetData(craftedItemId, out _craftedItem);
            _recipeDatabase.TryGetData(recipeId, out _recipe);
            if (_craftedItem is null)
            {
                Debug.LogError("Cannot process itemFile with id \"" + craftedItemId + "\" because this itemFile is not in items database.");
                return;
            }
            if (_recipe is null)
            {
                Debug.LogError("Cannot process recipeFile with id \"" + recipeId + "\" because this recipeFile is not in recipes database.");
                return;
            }
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (_controller.Craft(_inventory, _recipe))
            {
                if (_craftedItem is not null)
                {
                    _inventory.AddItem(_craftedItem.Id);
                }
            }
        }
    }
}