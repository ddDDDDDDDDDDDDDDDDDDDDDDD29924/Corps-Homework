using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "ScriptableObjects/RecipeData")]
public class RecipeData : ScriptableObject
{
    public IngredientType IngredientA;
    public IngredientType IngredientB;

    public GameObject[] Result;

    [Header("Features and Properties")]
    public float ExplosionPower = 0f;
}
