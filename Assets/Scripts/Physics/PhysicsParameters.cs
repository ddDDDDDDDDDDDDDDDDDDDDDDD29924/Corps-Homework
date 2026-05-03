using UnityEngine;

public class PhysicsParameters : MonoBehaviour
{
    public static PhysicsParameters Instance { get; private set; }

    [Header("Drag")]
    public bool isDragging = false;
}
