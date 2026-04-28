using System.Collections;
using UnityEngine;

public class IngredientThermalConductivity : MonoBehaviour
{
    private IngredientBase IngBase = null;
    private IngredientData IngData = null;

    [SerializeField] float conductionMultiplier = 1f;

    public float Temperature = 0f;

    private void Awake()
    {
        IngBase = gameObject.GetComponent<IngredientBase>();

        if (IngBase != null)
        {
            IngData = IngBase.Data;
        }
    }

    private void Start()
    {
        if (IngData != null)
        {
            Temperature = startTemperature;
        }
        StartCoroutine(ThermalCycle());
    }

    private float startTemperature => IngData.StartTemperature;
    public float thermalConductivity => IngData.ThermalConductivity;

    private IEnumerator ThermalCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            GameObject[] objects = GetAllITCObjects();

            foreach (var obj in objects)
            {
                IngredientThermalConductivity itc = obj.GetComponent<IngredientThermalConductivity>();
                float temp = itc.Temperature;
                float condRange = Mathf.Abs(temp * 0.1f);
                float distance = Vector3.Distance(transform.position, obj.transform.position);

                if (distance <= condRange && Temperature < temp)
                {
                    float conduction = (1 - distance / condRange) * thermalConductivity * conductionMultiplier;

                    Temperature += conduction;
                    itc.Temperature -= conduction;
                }
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
