using System.Collections;
using UnityEngine;

public class IngredientThermalConductivity : MonoBehaviour
{
    private IngredientBase IngBase = null;
    private IngredientData IngData = null;

    [SerializeField] float conductionMultiplier = 1f;

    private const float airCondLimit = 0.1f;

    private const float roomTemp = 22f;

    private float accuracy = 10f;

    public float Temperature = 0f;

    private void Awake()
    {
        IngBase = gameObject.GetComponent<IngredientBase>();

        if (IngBase != null)
        {
            IngData = IngBase.Data;
        }
        if (IngData != null)
        {
            Temperature = startTemperature;
        }
    }

    private void Start()
    {
        StartCoroutine(ThermalCycle());
    }

    private float startTemperature => IngData.StartTemperature;
    public float thermalConductivity => IngData.ThermalConductivity;

    private IEnumerator ThermalCycle()
    {
        float interval = 1f / accuracy;

        while (true)
        {
            yield return new WaitForSeconds(interval);

            GameObject[] objects = GetAllITCObjects();

            foreach (var obj in objects)
            {
                IngredientThermalConductivity itc = obj.GetComponent<IngredientThermalConductivity>();
                float temp = itc.Temperature;
                float thermCond = itc.thermalConductivity;
                float condRange = Mathf.Abs(Temperature * 0.1f);
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                float mediumCond = (thermalConductivity + thermCond) / 2f;

                if (distance <= condRange && temp < Temperature)
                {
                    float conduction = (1 - distance / condRange) * mediumCond * conductionMultiplier / accuracy;

                    Temperature -= conduction;
                    itc.Temperature += conduction;
                }
            }

            if (Temperature != roomTemp)
            {
                float difference = Mathf.Abs(roomTemp - Temperature);
                float conduction = thermalConductivity * Mathf.Clamp(difference, airCondLimit, float.MaxValue) * 0.005f / accuracy;

                if (Temperature > roomTemp)
                {
                    conduction *= -1f;
                }

                Temperature += conduction;
            }
        }
    }

    private GameObject[] GetAllITCObjects()
    {
        IngredientThermalConductivity[] itcComponents = Object.FindObjectsByType<IngredientThermalConductivity>(FindObjectsSortMode.None);

        GameObject[] result = new GameObject[itcComponents.Length];

        for (int i = 0; i < itcComponents.Length; i++)
        {
            result[i] = itcComponents[i].gameObject;
        }

        return result;
    }
}
