using Unity.VisualScripting;
using UnityEngine;

public class Convection : MonoBehaviour
{
    [SerializeField] private float velocity = 1f;

    private Vector3 direction = Vector3.up;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (velocity > 0f && rb != null)
        {
            if (rb.useGravity)
                rb.useGravity = false;

            transform.Translate(direction * velocity * Time.deltaTime, Space.World);
        }
    }
}
