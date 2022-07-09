using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementGhost : MonoBehaviour
{
    private Transform visual;
    private InventoryItem SO;

    private void Start()
    {
        RefreshVisual();
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = GridPlacementSystem.Instance.GetMouseWorldSnappedPosition();
        targetPosition.y = 1f;
        transform.position =
            Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 0.15f);
        transform.rotation = Quaternion.Lerp(transform.rotation, GridPlacementSystem.Instance.GetPlacedObjectRotation(),Time.deltaTime * 0.15f );
    }

    private void RefreshVisual()
    {
        if (visual != null)
        {
            Destroy(visual.gameObject);
            visual = null;
        }

        if (SO != null)
        {
            visual = Instantiate(SO.GetVisual(), Vector3.zero, Quaternion.identity);
            visual.parent = transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
            
            
        }
    }
    
}
