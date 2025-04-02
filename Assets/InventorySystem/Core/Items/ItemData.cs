using System;
using UnityEngine;

namespace InventorySystem.Core.Items
{
    [Serializable]
    public class ItemData: IUnique
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