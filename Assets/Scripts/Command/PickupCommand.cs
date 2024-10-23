using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PickupCommand : Command
{
    private NavMeshAgent agent;
    private Pickable pickable;
    private Transform attachPoint;
    private Commandable commandable;

    private Vector3 interactionPoint;

    public PickupCommand(NavMeshAgent _agent, Pickable _pickable, Transform _attachPoint, Commandable _commandable)
    {
        this.agent = _agent;
        this.pickable = _pickable;
        this.attachPoint = _attachPoint;
        this.commandable = _commandable;

        interactionPoint = pickable.GetInteractionPoint(agent.transform, pickable.transform);
    }

    public override bool isComplete => PickupComplete();

    public override void Execute()
    {
        Debug.Log("Executing pickup command.");
        agent.SetDestination(interactionPoint);  // Move agent to the pickable object's location
    }

    bool PickupComplete()
    {
        //Debug.Log("Checking if Pickup is complete...");
        //Debug.Log("Remaining Distance: " + agent.remainingDistance + " Stopping Distance: " + agent.stoppingDistance);

        if (agent.remainingDistance > pickable.InteractionThreshold)
        {
            Debug.Log("Still too far away to pickup.");
            return false;
        }

        if (agent.remainingDistance <= pickable.InteractionThreshold)
        {
            Debug.Log("Close enough to pick up.");
            agent.isStopped = true;
            agent.ResetPath();
            pickable.PickUp(attachPoint);  // Pick up the object and attach it to the robot/unit
            Physics.SyncTransforms();
            commandable.StorePickable(pickable);
        }

        return true;
    }
    
}
