#if false

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CommandInteractorOld : Interactor
{
    Queue<Command> commands = new Queue<Command>();

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject pointerPrefab;
    [SerializeField] private Camera cam;

    private Command currentCommand;

    public override void Interact()
    {
        if (input.commandPressed)
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out var hitInfo))
            {
                if (hitInfo.transform.CompareTag("Ground"))
                {
                    GameObject pointer = Instantiate(pointerPrefab);
                    pointer.transform.position = hitInfo.point;

                    commands.Enqueue(new MoveCommand(agent, hitInfo.point));
                }
                else if (hitInfo.transform.CompareTag("Builder"))
                {
                    Builder builder = hitInfo.transform.GetComponent<Builder>();
                    commands.Enqueue(new BuildCommand(agent, builder));
                }
            }

            ProcessCommands();
        }
    }

    void ProcessCommands()
    {
        if (currentCommand != null && !currentCommand.isComplete)
            return;

        if (commands.Count == 0)
        {
            return;
        }

        currentCommand = commands.Dequeue();
        currentCommand.Execute();
    }
}
#endif