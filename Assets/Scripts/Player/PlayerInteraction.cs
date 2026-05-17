using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private LayerMask interactionLayers;
    private float InteractionRange => playerData.dragRange;

    private void Update()
    {
        if (InputManager.Instance == null || GameManager.Instance.CurrentGameState != GameState.Playing)
            return;

        if (InputManager.Instance.IsInteractPressed() && playerData != null)
        {
            TryToInteract();
        }
    }

    private void TryToInteract()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, InteractionRange, interactionLayers))
        {
            Interaction interaction = hit.collider.gameObject.GetComponentInParent<Interaction>();

            if (interaction != null)
            {
                interaction.Interacted = true;
            }
        }
    }
}
