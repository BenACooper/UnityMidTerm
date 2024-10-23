using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    private NavMeshAgent agent;
    private Vector3 destinaton;
    private GameObject pointerPrefabInstance;

    public MoveCommand(NavMeshAgent _agent, Vector3 _destinaton, GameObject _pointerPrefabInstance)
    {
        this.agent = _agent;
        this.destinaton = _destinaton;
        this.pointerPrefabInstance = _pointerPrefabInstance;
    }

    public override bool isComplete => ReachedDestination();

    public override void Execute()
    {
        agent.SetDestination(destinaton);
    }

    bool ReachedDestination()
    {
        if (agent.remainingDistance > 0.5f)
            return false;

        GameObject.Destroy(pointerPrefabInstance);

        return true;
    }
}
