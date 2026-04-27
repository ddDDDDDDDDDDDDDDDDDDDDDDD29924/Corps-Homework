using UnityEngine;

public enum PhysicalState
{
    Solid,
    Liquid,
    Gas,
}

[CreateAssetMenu(fileName = "IngredientData", menuName = "ScriptableObjects/IngredientData")]
public class IngredientData : ScriptableObject
{
    public IngredientType Type;
    public PhysicalState State;
    public float BoilingPoint = 100f;
    public float MeltingPoint = 0f;

    public GameObject SolidObject;
    public GameObject LiquidObject;
    public GameObject GasObject;

}
