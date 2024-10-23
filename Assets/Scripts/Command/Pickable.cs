using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] private float interactionThreshold;  // Serialized to set in the Inspector for objects of different size.
    private bool isPicked = false;  // To track if the object is already picked up
    private Rigidbody bigCubeRB;

    public float InteractionThreshold => interactionThreshold;

    private void Start()
    {
        bigCubeRB = GetComponent<Rigidbody>();
    }

    // Method to calculate interaction point between the robot and the object
    public Vector3 GetInteractionPoint(Transform agentTransform, Transform targetPoint)
    {
        // Get the direction from the agent (robot) to the pickable object (or placement point)
        Vector3 directionToTarget = (targetPoint.position - agentTransform.position).normalized;

        // Calculate the interaction point at the specified threshold
        Vector3 interactionPoint = targetPoint.position - directionToTarget * interactionThreshold;

        Debug.Log($"Calculated interaction point: {interactionPoint}");
        return interactionPoint;
    }

    public void PickUp(Transform attachPoint)
    {
        if (isPicked) return;  // If it's already picked, don't pick it up again

        // Debug log to check attachPoint
        if (attachPoint != null)
        {
            Debug.Log($"AttachPoint provided: {attachPoint.name}, Position: {attachPoint.position}");
        }
        else
        {
            Debug.LogError("No attachPoint provided!");
        }

        // Attach the object to the unit's attach point
        transform.position = attachPoint.position;
        transform.rotation = attachPoint.rotation;
        transform.SetParent(attachPoint);

        bigCubeRB.isKinematic = true;
        bigCubeRB.useGravity = false;
        isPicked = true;

        Debug.Log($"{gameObject.name} has been picked up.");
    }

    public void PlaceDown(Transform placePoint)
    {
        if (!isPicked) return;

        // Re-enable physics and place the object on the pad
        transform.position = placePoint.position;
        transform.rotation = placePoint.rotation;  // Optional, match rotation if necessary
        transform.SetParent(null);  // Detach from the robot

        bigCubeRB.isKinematic = false;
        bigCubeRB.useGravity = true;
        isPicked = false;

        Debug.Log($"{gameObject.name} has been placed on the pad.");
    }
}
