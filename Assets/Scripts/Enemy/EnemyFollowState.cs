using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : EnemyState
{
    float distanceToPlayer;
    public EnemyFollowState(EnemyController enemy) : base(enemy)
    {

    }

    public override void OnStateEnter()
    {
        Debug.Log("Enemy started following the player.");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy stopped following the player.");        
    }

    public override void OnStateUpdate()
    {
        if (_enemy.player != null)
        {
            distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy.player.position);

            if (distanceToPlayer > 10)
            {
                //Going back to idle
                _enemy.ChangeState(new EnemyIdleState(_enemy));
            }

            //Set the attack
            if (distanceToPlayer < 2)
            {
                _enemy.ChangeState(new EnemyAttackState(_enemy));
            }

            _enemy.agent.destination = _enemy.player.position;
        }
        else
        {
            //Going back to the idle state.
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }
    }
}
