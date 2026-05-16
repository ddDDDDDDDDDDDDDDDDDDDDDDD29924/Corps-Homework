using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class ResourceLoader
{
    public static RecipeData[] GetRecipeDatasInFolder()
    {
        string[] guids = AssetDatabase.FindAssets("t:RecipeData", new[] { "Assets/ScriptableObject/Recipes" });
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

    public static GameObject GetLastAncestorWithLayerMask(GameObject obj, LayerMask layerMask)
    {
        GameObject lastAncestor = null;
        Transform current = obj.transform.parent;

        while (current != null)
        {
            if (((1 << current.gameObject.layer) & layerMask) != 0)
            {
                lastAncestor = current.gameObject;
            }
            current = current.parent;
        }

        return lastAncestor;
    }
}
