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
    public float StartTemperature = 22f;
    public float ThermalConductivity = 1f;

    public GameObject SolidObject;
    public GameObject LiquidObject;
    public GameObject GasObject;

}
