using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Heater : MonoBehaviour
{
    [SerializeField] private float power = 1000f;
    [SerializeField] private Interaction interaction;
    [SerializeField] private ParticleSystem particles;

    private float accuracy = IngredientThermalConductivity.accuracy;

    public bool isHeating = false;

    private List<IngredientThermalConductivity> ITCs = new List<IngredientThermalConductivity>();

    private void Start()
    {
        StartCoroutine(HeatCoroutine());
    }

    private void FixedUpdate()
    {
        if (interaction != null && interaction.Interacted)
        {
            isHeating = !isHeating;
            if (particles != null)
            {
                if (isHeating)
                    particles.Play();
                else
                    particles.Stop();
            }
            interaction.Interacted = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IngredientThermalConductivity itc = other.GetComponent<IngredientThermalConductivity>();

        if (itc != null)
        {
            ITCs.Add(itc);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IngredientThermalConductivity itc = other.GetComponent<IngredientThermalConductivity>();

        if (itc != null)
        {
            ITCs.Remove(itc);
        }
    }

    private IEnumerator HeatCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / accuracy);

            if (isHeating)
            {
                IngredientThermalConductivity[] itcArray = ITCs.ToArray();

                foreach (var itc in itcArray)
                {
                    float difference = Mathf.Abs(itc.Temperature - power);
                    float heatAmount = Mathf.Clamp(difference, 10f, float.MaxValue) * 0.05f / accuracy;

                    itc.Temperature += heatAmount;
                }
            }
        }
    }
}
