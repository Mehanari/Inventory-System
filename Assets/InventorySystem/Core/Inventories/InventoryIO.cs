using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace InventorySystem.Core.Inventories
{
    public static class InventoryIO
    {
        private static readonly string DefaultPath = Path.Combine(
            Application.persistentDataPath,
            "inventory.json"
        );
        
        public static void Save(DictInventory inventory)
        {
            Save(inventory, DefaultPath);
        }
        
        public static void Save(DictInventory inventory, string path)
        {
            try
            {
                string directory = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                string jsonString = JsonConvert.SerializeObject(inventory.Items);
                
                File.WriteAllText(path, jsonString);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error saving inventory: {ex.Message}");
                throw;
            }
        }
        
        public static DictInventory Load()
        {
            return Load(DefaultPath);
        }
        
        public static DictInventory Load(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return new DictInventory();
                }
                
                string jsonString = File.ReadAllText(path);
                
                var items = JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonString);
                
                var inventory = new DictInventory();
                if (items != null)
                {
                    inventory = new DictInventory(items);
                }
    
                return inventory;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error loading inventory: {ex.Message}");
                throw;
            }
        }
    }
}