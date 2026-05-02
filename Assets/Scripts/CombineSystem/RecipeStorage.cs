using UnityEngine;

public enum IngredientType
{
    RedBlock,
    BlueBlock,
    GreenBlock,
    PurpleBlock,
    YellowBlock,
    OrangeBlock,
    WhiteBlock,
    BlackBlock,
    GrayBlock,
    WaterBlock,
    AcidBlock,
    FireBlock,
    LightBlock,
    NeonBlock,
    SodiumBlock,
    HydrogenBlock,
    IceBlock,
    SteamBlock,
    SaltBlock,
}

public class RecipeStorage : MonoBehaviour
{
    public static RecipeStorage Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public RecipeData[] RecipeDatas;
    public GameObject Worktable;
    public float CombineRange = 2f;
}
