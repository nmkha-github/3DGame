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

            enemy.Agent.SetDestination(enemy.Player.transform.position + (enemy.transform.position - enemy.Player.transform.position).normalized * stateSoldier.distance + (Random.insideUnitSphere * stateSoldier.randomMove));
            if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) <= stateSoldier.distance + stateSoldier.randomMove + 3f)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer > enemy.fireRate)
                {
                    Attack();
                    attackTimer = 0;
                }
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
        //store reference to the gun barrel
        Transform gunBarrel = enemy.gunBarrel;

        //instantiate a new bullet
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/EnemyBullet") as GameObject, gunBarrel.position, enemy.transform.rotation);
        //calculate the direction to the player
        Vector3 shootDirection = (enemy.Player.transform.position - gunBarrel.transform.position).normalized;
        //add force rigidbody of the bullet
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-stateSoldier.differrence, stateSoldier.differrence), Vector3.up) * shootDirection * stateSoldier.speedBullet;
        attackTimer = 0;
    }
}
