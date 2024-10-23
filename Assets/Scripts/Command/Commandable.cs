using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Commandable : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 originalPosition;
    private Pickable currentPickable;  // Store the picked-up object

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;  // Store the initial position
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.WakeUp();  // Wake the Rigidbody
        rb.sleepThreshold = 0f;  // Prevent it from going back to sleep
    }

    // Command to make the unit face the player
    public void FacePlayer(Transform player)
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;  // Keep the rotation on the horizontal plane because our robot doesn't have a head LOL
        transform.rotation = Quaternion.LookRotation(direction);
        Debug.Log("Robot is ready to recieve commands.");
    }

    // Command to return the unit to its original position
    public void ReturnToOriginalPosition()
    {
        agent.SetDestination(originalPosition);
        Debug.Log("Robot returning to standby mode.");
    }

    // Store the picked-up object
    public void StorePickable(Pickable pickable)
    {
        currentPickable = pickable;
    }

    // Retrieve the stored pickable object
    public Pickable GetPickable()
    {
        return currentPickable;
    }

    public void ClearPickable(Pickable pickable)
    {
        currentPickable = null;
    }
}
