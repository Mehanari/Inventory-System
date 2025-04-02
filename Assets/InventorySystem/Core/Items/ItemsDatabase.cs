using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem.Core.Items
{
    /// <summary>
    /// This class defines a set of itemFiles that can be used in the game.
    /// </summary>
    [CreateAssetMenu(fileName = "ItemsDatabase", menuName = "InventorySystem/ItemsDatabase")]
    public class ItemsDatabase : Database<ItemData> { }
}