using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Commandable;

public class ReadyCommand : Command
{
    private NavMeshAgent agent;
    private Commandable commandable;
    private Transform player;

    public ReadyCommand(Commandable _commandable, NavMeshAgent _agent, Transform _player)
    {
        this.agent = _agent;
        this.commandable = _commandable;
        this.player = _player;
    }

    public override bool isComplete => ReadyComplete(); 

    public override void Execute()
    {
        commandable.FacePlayer(player);// Turn agent to face the player.
    }

    bool ReadyComplete()
    {
        //This is an instant action, the unit doesn't have to go anywhere.
        return true;
    }
}
