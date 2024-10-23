using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlaceCommand : Command
{
    private NavMeshAgent agent;
    private Commandable commandable;  // Reference to the robot
    private Transform placePoint;  // Where the object will be placed
    private Pickable pickable;  // Store the picked-up object for reuse

    private Vector3 interactionPoint;

    public PlaceCommand(NavMeshAgent _agent, Commandable _commandable, Transform _placePoint)
    {
        this.agent = _agent;
        this.commandable = _commandable;
        this.placePoint = _placePoint;

        pickable = commandable.GetPickable();
        interactionPoint = pickable.GetInteractionPoint(agent.transform, placePoint);
    }

    public override bool isComplete => PlaceComplete();

    public override void Execute()
    {
        agent.SetDestination(interactionPoint);  // Move robot to the placePoint
    }

    bool PlaceComplete()
    {
        // Check if the robot has reached the interaction point
        if (agent.remainingDistance > pickable.InteractionThreshold && pickable != null)
        {
            Debug.Log("Still too far away to place.");
            return false;
        }
     

        if (agent.remainingDistance <= pickable.InteractionThreshold && pickable != null)
        {
            Debug.Log("Close enough to place.");
            agent.isStopped = true;
            agent.ResetPath();
            pickable.PlaceDown(placePoint);  // Place the cube on the pressure pad
            Physics.SyncTransforms();
            commandable.ClearPickable(pickable);
        }

        return true;
    }
}

