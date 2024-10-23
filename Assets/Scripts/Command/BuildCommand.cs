using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildCommand : Command
{
    private NavMeshAgent agent;
    private Builder builder;

    public BuildCommand(NavMeshAgent _agent, Builder _builder)
    {
        this.agent = _agent;
        this.builder = _builder;
    }
    public override bool isComplete => BuildComplete();

    public override void Execute()
    {
        agent.SetDestination(builder.transform.position);
    }

    bool BuildComplete()
    {
        if(agent.remainingDistance > 0.5f)
            return false;

        if(builder != null)
            builder.Build();
        return true;
    }
}
