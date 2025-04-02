using UnityEngine;

namespace InventorySystem.Core
{
    [CreateAssetMenu(fileName = "Item", menuName = "InventorySystem/Item")]

    public class Item : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private string itemName;
        [SerializeField, TextArea] private string description;
        [SerializeField] private Texture2D texture;
        
        public string Id => id;
        public string ItemName => itemName;
        public string Description => description;
        public Texture2D Texture => texture;
    }
}
