using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class ResourceLoader
{
    public static RecipeData[] GetRecipeDatasInFolder()
    {
        string[] guids = AssetDatabase.FindAssets("t:RecipeData", new[] { "Assets/ScriptableObjects/Recipes" });
        List<RecipeData> recipeDatas = new List<RecipeData>();
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            RecipeData recipeData = AssetDatabase.LoadAssetAtPath<RecipeData>(path);
            if (recipeData != null)
            {
                recipeDatas.Add(recipeData);
            }
        }
        return recipeDatas.ToArray();
    }
}
