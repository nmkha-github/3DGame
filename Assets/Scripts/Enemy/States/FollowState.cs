using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowState : BaseState
{
    private float moveTimer;
    private float attackTimer;
    private bool sawEnemy = false;
    private float sawEnemyTimer;
    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            enemy.transform.LookAt(enemy.Player.transform);

            enemy.Agent.SetDestination(enemy.Player.transform.position + (enemy.transform.position - enemy.Player.transform.position).normalized * 5);
            if (enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer > 3)
                {
                    Attack();
                    attackTimer = 0;
                }
            }
            moveTimer += Time.deltaTime;
            if (moveTimer > Random.Range(stateSoldier.moveTimeMin, stateSoldier.moveTimeMax))
            {
                enemy.Agent.SetDestination(enemy.Player.transform.position);
                moveTimer = 0;
            }
            sawEnemy = true;
        }
        else
        {
            if (sawEnemy)
            {
                enemy.Agent.SetDestination(enemy.Player.transform.position);

                sawEnemyTimer += Time.deltaTime;
                if (sawEnemyTimer > 3)
                {
                    sawEnemyTimer = 0;
                    sawEnemy = false;
                }
            }
            if (enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance)
            {
                moveTimer += Time.deltaTime;
                if (moveTimer > Random.Range(stateSoldier.moveTimeMin, stateSoldier.moveTimeMax))
                {
                    enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * stateSoldier.randomMove));
                    moveTimer = 0;
                }
            }
        }
    }

    private void Attack()
    {
        
    }
}
