using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float distanceToPlayer;
    Health playerHealth;
    float healthDmgPerSecond = 20f;

    public EnemyAttackState(EnemyController enemy) : base(enemy)
    {
        playerHealth = enemy.player.GetComponent<Health>();
        Debug.Log("Player health = " + playerHealth);
    }

    public override void OnStateEnter()
    {
        Debug.Log("Enemy will attack the player.");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy will stop attacking the player.");
    }

    public override void OnStateUpdate()
    {
        Attack();

        if (_enemy.player != null)
        {
            distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy.player.position);

            if (distanceToPlayer > 2)
            {
                //Going back to follow state.
                _enemy.ChangeState(new EnemyFollowState(_enemy));
            }

            _enemy.agent.destination = _enemy.player.position;
        }
        else
        {
            //Going back to the idle state.
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }
    }

    void Attack()
    {
        if(playerHealth != null)
        {
            playerHealth.DeductHealth(healthDmgPerSecond * Time.deltaTime);
        }
    }
}
