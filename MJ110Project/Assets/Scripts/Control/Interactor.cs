using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Camera mainCamera;

    [Header("Interaction Parameters")]
    [SerializeField] private float interactionDistance = 1.0f;
    [SerializeField] private LayerMask interactableLayers;

    public void Interact()
    {
        Ray interactionRay =
            mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;

        if (Physics.Raycast(interactionRay, out hit, interactionDistance,
            interactableLayers))
        {
            // TODO: Change to get item data + display it
            if (hit.collider != null)
            {
                Debug.Log(string.Format("{0} was interacted with!", 
                    hit.collider.name));
            }
        }
    }
}
