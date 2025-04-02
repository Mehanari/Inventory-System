using UnityEngine;

namespace InventorySystem.Core
{
    public abstract class DataFile<T> : ScriptableObject
    {
        public abstract T Data { get; }
    }
}