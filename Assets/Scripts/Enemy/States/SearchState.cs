using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.LastKnowPos);
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
        if (enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance)
        {
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;
            if (moveTimer > Random.Range(stateMachine.SS_moveTimeMin, stateMachine.SS_moveTimeMax))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * stateMachine.SS_randomMove));
                moveTimer = 0;
            }
            if (searchTimer > stateMachine.SS_searchTime)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }
}
