using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] targetPoints;
    [SerializeField] private Transform enemyEye;
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private float checkRadius = 0.8f;

    int currentTarget = 0;

    private NavMeshAgent agent;

    public bool isIdle = true;
    public bool isPlayerFound;
    public bool isCloseToPlayer;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = targetPoints[currentTarget].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isIdle)
        {
            Idle();
        }
        else if(isPlayerFound)
        {
            if(isCloseToPlayer)
            {
                AttackPlayer();
            }
            else
            {
                FollowPlayer();
            }
        }
    }

    void Idle()
    {
        if(agent.remainingDistance < 0.5f)
        {
            currentTarget++;
            if(currentTarget >= targetPoints.Length)
                currentTarget = 0;               
            agent.destination = targetPoints[currentTarget].position;
        }

        //Check for player.
        if(Physics.SphereCast(enemyEye.position, checkRadius, transform.forward, out RaycastHit hit, playerCheckDistance))
        {
            if(hit.transform.CompareTag("Player"))
            {
                Debug.Log("Player found!");
                isIdle = false;
                isPlayerFound = true;
                player = hit.transform;
                agent.destination = player.position;
            }
        }
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.position) > 10)
            {
                isPlayerFound = false;
                isIdle = true;
            }

            //Set the attack
            if (Vector3.Distance(transform.position, player.position) < 2)
            {
                isCloseToPlayer = true;
            }
            else
            {
                isCloseToPlayer = false;
            }

            agent.destination = player.position;
        }
        else
        {
            isPlayerFound = false;
            isIdle = true;
            isCloseToPlayer = false;
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Attacking the player.");
        if (Vector3.Distance(transform.position, player.position) > 2)
        {
            isCloseToPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyEye.position, checkRadius);
        Gizmos.DrawWireSphere(enemyEye.position + enemyEye.forward * playerCheckDistance, checkRadius);

        Gizmos.DrawLine(enemyEye.position, enemyEye.position +  enemyEye.forward * playerCheckDistance);
    }
}
