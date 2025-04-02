using InventorySystem.Core;
using InventorySystem.Core.Inventories;
using InventorySystem.Core.Items;
using InventorySystem.Core.Recipes;
using UnityEngine;
using Zenject;

namespace InventorySystem.Examples.Scripts
{
    public class SampleGameInstaller : MonoInstaller
    {
        [SerializeField] private ItemsDatabase itemsDatabase;
        [SerializeField] private RecipeDatabase recipeDatabase;
        
        public override void InstallBindings()
        {
            //Loading inventory from save file (empty inventory if file does not exist).
            Container.Bind<Inventory>().FromInstance(InventoryIO.Load());
            Container.Bind<ItemsDatabase>().FromInstance(itemsDatabase);
            Container.Bind<RecipeDatabase>().FromInstance(recipeDatabase);
        }
    }
}