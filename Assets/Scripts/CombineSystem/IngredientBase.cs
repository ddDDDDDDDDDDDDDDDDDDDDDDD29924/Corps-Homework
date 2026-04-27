using UnityEngine;

public class IngredientBase : MonoBehaviour
{
    public IngredientData Data;

    public float Temperature = 22f;

    private void OnCollisionEnter(Collision collision)
    {
        IngredientBase otherObject = collision.gameObject.GetComponent<IngredientBase>();

        if (otherObject != null)
        {
            TryCombine(this.gameObject, otherObject.gameObject);
        }
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
