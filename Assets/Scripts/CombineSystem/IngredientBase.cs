using UnityEngine;

public class IngredientBase : MonoBehaviour
{
    public IngredientData Data;

    private void OnCollisionEnter(Collision collision)
    {
        IngredientBase otherObject = collision.gameObject.GetComponent<IngredientBase>();

        if (otherObject != null)
        {
            TryCombine(this.gameObject, otherObject.gameObject);
        }
    }

    private void FixedUpdate()
    {
        PhysicalState ingState = Data.State;

        GameObject solid = Data.SolidObject;
        GameObject liquid = Data.LiquidObject;
        GameObject gas = Data.GasObject;

        float meltingPoint = Data.MeltingPoint;
        float boilingPoint = Data.BoilingPoint;

        float currentTemp = GetComponent<IngredientThermalConductivity>().Temperature;

        if (ingState == PhysicalState.Solid && currentTemp >= meltingPoint)
        {
            TurnInto(liquid, meltingPoint);
        }
        else if (ingState == PhysicalState.Liquid && currentTemp >= boilingPoint)
        {
            TurnInto(gas, boilingPoint);
        }
        else if (ingState == PhysicalState.Liquid && currentTemp < meltingPoint)
        {
            TurnInto(solid, meltingPoint - 0.001f);
        }
        else if (ingState == PhysicalState.Gas && currentTemp < boilingPoint)
        {
            TurnInto(liquid, boilingPoint - 0.001f);
        }
    }

    private void TurnInto(GameObject newObj, float temp)
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        
        GameObject obj = Instantiate(newObj, position, rotation);
        obj.GetComponent<IngredientThermalConductivity>().Temperature = temp;

        Destroy(gameObject);
    }

    private void TryCombine(GameObject obj1, GameObject obj2)
    {
        if (obj1.GetInstanceID() > obj2.GetInstanceID()) return;

        foreach (var recipe in RecipeStorage.Instance.RecipeDatas)
        {
            if (CheckMatch(recipe, obj1, obj2) && CheckGameState() && CheckWorktable(obj1, obj2))
            {
                SpawnResult(recipe, obj1, obj2);
                break;
            }
        }
    }

    private bool CheckMatch(RecipeData recipe, GameObject a, GameObject b)
    {
        IngredientType RBaseA = recipe.IngredientA;
        IngredientType RBaseB = recipe.IngredientB;
        IngredientBase ABase = a.GetComponent<IngredientBase>();
        IngredientBase BBase = b.GetComponent<IngredientBase>();

        return (RBaseA == ABase.Data.Type && RBaseB == BBase.Data.Type) ||
               (RBaseA == BBase.Data.Type && RBaseB == ABase.Data.Type);
    }

    private bool CheckGameState()
    {
        return GameManager.Instance.CurrentGameState == GameState.Playing;
    }

    private bool CheckWorktable(GameObject a, GameObject b)
    {
        GameObject worktable = RecipeStorage.Instance.Worktable;
        float combineRange = RecipeStorage.Instance.CombineRange;
        return Vector3.Distance(a.transform.position, worktable.transform.position) <= combineRange &&
               Vector3.Distance(b.transform.position, worktable.transform.position) <= combineRange;
    }

    private void SpawnResult(RecipeData recipe, GameObject a, GameObject b)
    {
        Vector3 spawnPos = (a.transform.position + b.transform.position) / 2f;
        foreach (var result in recipe.Result) 
        { 
            Instantiate(result.gameObject, spawnPos, Quaternion.identity);
        }

        Destroy(a);
        Destroy(b);
    }
}
