using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Commandable;

public class IdleCommand : Command
{
    private NavMeshAgent agent;
    private Commandable commandable;

    public IdleCommand(Commandable _commandable, NavMeshAgent _agent)
    {
        this.agent = _agent;
        this.commandable = _commandable;
    }

    public override bool isComplete => IdleComplete();

    public override void Execute()
    {
        // Use Commandable to move the robot back to its original position
        commandable.ReturnToOriginalPosition();
    }

    bool IdleComplete()
    {
        //This is an instant action, and empties the queue.
        return true;
    }
}
