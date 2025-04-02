using UnityEngine;

namespace InventorySystem.Core.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "InventorySystem/Item")]

    public class ItemFile : DataFile<ItemData>
    {
        [SerializeField] private ItemData data;
        public override ItemData Data => data;
    }
}
