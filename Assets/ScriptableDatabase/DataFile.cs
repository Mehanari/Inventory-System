using UnityEngine;

namespace ScriptableDatabase
{
    public abstract class DataFile<T> : ScriptableObject
    {
        public abstract T Data { get; }
    }
}