using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptableDatabase
{
    public abstract class Database<T> : ScriptableObject, ISerializationCallbackReceiver where T : IUnique
    {
#if UNITY_EDITOR
        [SerializeField] private List<DataFile<T>> dataFiles = new List<DataFile<T>>();
#else
        [System.NonSerialized] private List<ItemFile> dataFiles = new List<ItemFile>();
#endif
        
        [SerializeField, HideInInspector] private List<T> data = new();
        
        private Dictionary<string, T> _dict = new();

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            ResetDictionary();
        }

        private void OnValidate()
        {
            FillData();
        }

        private void FillData()
        {
            data.Clear();
            var ids = new HashSet<string>();
            for (int i = 0; i < dataFiles.Count; i++)
            {
                var file = dataFiles[i];
                if (file is null)
                {
                    continue;
                }

                if (file.Data is null)
                {
                    Debug.LogWarning("Data in the item file is null. This file will be ignored.\nFile number in list: " + (i+1) + ".");
                    continue;
                }
                if (ids.Contains(file.Data.Id))
                {
                    Debug.LogWarning("Data with id \"" + file.Data.Id +
                                     "\" is already added to the database. Files with repeating data ids will be ignored.\nFile number in list: " +
                                     (i + 1) + ".");
                    continue;
                }

                ids.Add(file.Data.Id);
                data.Add(file.Data);
            }
        }

        private void ResetDictionary()
        {
            _dict.Clear();
            foreach (var entry in data)      
            {
                if (entry is null)
                {
                    Debug.LogError("Null entry encountered in the data list while resetting data dictionary.");
                    continue;
                }
                if (string.IsNullOrEmpty(entry.Id))
                {
                    Debug.LogError("Entry with empty id encountered while resetting data dictionary.");
                    continue;
                }
                if (_dict.ContainsKey(entry.Id))
                {
                    Debug.LogError("Entry with repeating id encountered while resetting data dictionary. Entry id: " + entry.Id + ".");
                    continue;
                }
                _dict.Add(entry.Id, entry);
            }
        }

        public string[] GetItemIds()
        {
            return _dict.Keys.ToArray();
        }
        
        public T GetData(string id)
        {
            return _dict[id];
        }
        
        public bool ContainsData(string id)
        {
            return _dict.ContainsKey(id);
        }

        public bool TryGetData(string id, out T itemFile)
        {
            itemFile = default(T);
            if (_dict.TryGetValue(id, out var value))
            {
                itemFile = value;
                return true;
            }

            return false;
        }
    }
}