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
    CarbonBlock,
    HydrogenBlock,
    HeliumBlock,
    OxygenBlock,
    IceBlock,
    SteamBlock,
    SaltBlock,
    IronBlock,
    MoltenIronBlock,
    CopperBlock,
    MoltenCopperBlock,
    GoldBlock,
    MoltenGoldBlock,
    SilverBlock,
    MoltenSilverBlock,
    AluminumBlock,
    MoltenAluminumBlock,
    NickelBlock,
    MoltenNickelBlock,

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

        Initialize();
    }

    private void Initialize()
    {
        RecipeDatas = ResourceLoader.GetRecipeDatasInFolder();
    }

    public RecipeData[] RecipeDatas;
    public GameObject Worktable;
    public float CombineRange = 2f;
}
