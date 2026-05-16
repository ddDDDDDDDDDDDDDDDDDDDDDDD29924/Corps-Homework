using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Hydroconverter : MonoBehaviour
{
    [SerializeField] private GameObject Required;
    [SerializeField] private GameObject Result;

    private void OnTriggerEnter(Collider other)
    {
        if (Required != null && Result != null)
        {
            GameObject obj = other.gameObject;

            IngredientBase reqBase = Required.GetComponent<IngredientBase>();
            IngredientBase othBase = obj.GetComponent<IngredientBase>();

            if (othBase.Data.Type == reqBase.Data.Type)
            {
                Vector3 pos = obj.transform.position;
                Quaternion rot = obj.transform.rotation;

                Instantiate(Result, pos, rot);
                Destroy(obj);
            }
        }
        else
        {
            Debug.LogError("gameObjects are null");
        }
    }
}
