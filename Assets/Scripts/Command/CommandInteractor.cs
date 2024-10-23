using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static Commandable;

public class CommandInteractor : Interactor
{
    Queue<Command> commands = new Queue<Command>();

    [SerializeField] private GameObject pointerPrefab;
    [SerializeField] private Camera cam;

    private Command currentCommand;
    private Commandable commandable;
    private NavMeshAgent agent = null;
    private Transform attachPoint = null;


    private void FixedUpdate()
    {
        ProcessCommands();
    }

    public override void Interact()
    {
        if (input.commandPressed)
        {
            Debug.Log("Q is pressed.");
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out var hitInfo))
            {
                Debug.Log("Hit object: " + hitInfo.transform.name);

                if (hitInfo.transform.CompareTag("BigRobot"))
                {
                    Debug.Log("Taking command.");
                    commandable = hitInfo.transform.GetComponent<Commandable>();
                    agent = hitInfo.transform.GetComponent<NavMeshAgent>();
                    attachPoint = hitInfo.transform.Find("AttachPoint");
                    commands.Enqueue(new ReadyCommand(commandable, agent, cam.transform));
                }
                else  if (hitInfo.transform.CompareTag("Ground") && agent != null)
                {
                    Debug.Log("Commanding Move");
                    GameObject pointer = Instantiate(pointerPrefab);
                    pointer.transform.position = hitInfo.point;
                    commands.Enqueue(new MoveCommand(agent, hitInfo.point, pointer));
                }
                else if (hitInfo.transform.CompareTag("BigCube") && agent != null && attachPoint != null)
                {
                    Debug.Log("Commanding Pickup");
                    Pickable pickable = hitInfo.transform.GetComponent<Pickable>();
                    commands.Enqueue(new PickupCommand(agent, pickable, attachPoint, commandable));
                }
                else if (hitInfo.transform.CompareTag("BigPad") && agent != null && attachPoint != null)
                {
                    Debug.Log("Commanding Place");
                    Transform placePoint = hitInfo.transform.Find("PlacePoint");
                    commands.Enqueue(new PlaceCommand(agent, commandable, placePoint));  // Pass commandable
                }
                else if (input.releasePressed)
                {
                    Debug.Log("Releasing from command.");
                    commands.Enqueue(new IdleCommand(commandable, agent));
                    commands.Clear();
                    agent = null;
                    attachPoint = null;
                }
                //else if (hitInfo.transform.CompareTag("Builder") && agent != null)
                //{
                //    Debug.Log("Commanding Build");
                //    Builder builder = hitInfo.transform.GetComponent<Builder>();
                //    commands.Enqueue(new BuildCommand(agent, builder));
                //}

            }
        }
    }

    void ProcessCommands()
    {
        if (commands.Count == 0 && currentCommand == null)
            return;

        if (currentCommand != null && !currentCommand.isComplete)
        { 
            return;
        }

        if (commands.Count > 0)
        {
            currentCommand = commands.Dequeue();
            currentCommand.Execute();
        }
    }
}