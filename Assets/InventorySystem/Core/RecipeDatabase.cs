using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Core
{
    [CreateAssetMenu(fileName = "RecipeDatabase", menuName = "InventorySystem/RecipeDatabase")]
    public class RecipeDatabase : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<Recipe> recipes = new();

        private Dictionary<string, Recipe> _recipesDict = new();

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            ResetRecipesDict();
        }

        private void ResetRecipesDict()
        {
            _recipesDict.Clear();
            foreach (var recipe in recipes)
            {
                if (recipe is null)
                {
                    continue;
                }
                if (_recipesDict.ContainsKey(recipe.Id))
                {
                    Debug.LogError("Recipe with id \"" + recipe.Id + "\" already exists in the database. Another recipe with the same id will be ignored.");
                }
                else
                {
                    _recipesDict.Add(recipe.Id, recipe);
                }
            }
        }

        public bool TryGetRecipe(string recipeId, out Recipe recipe)
        {
            recipe = null;
            if (_recipesDict.TryGetValue(recipeId, out var value))
            {
                recipe = value;
                return true;
            }

            return false;
        }
    }
}