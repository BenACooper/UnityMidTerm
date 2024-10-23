using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyState currentState;

    public Transform[] targetPoints;
    public Transform enemyEye;
    public float playerCheckDistance;
    public float checkRadius = 0.8f;

    [HideInInspector] public NavMeshAgent agent;

    [HideInInspector] public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = new EnemyIdleState(this);
        currentState.OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate();
    }

    public void ChangeState(EnemyState state)
    {
        currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyEye.position, checkRadius);
        Gizmos.DrawWireSphere(enemyEye.position + enemyEye.forward * playerCheckDistance, checkRadius);

        Gizmos.DrawLine(enemyEye.position, enemyEye.position + enemyEye.forward * playerCheckDistance);
    }
}
