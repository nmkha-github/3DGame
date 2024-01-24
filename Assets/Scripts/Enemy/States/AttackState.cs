using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shootTimer;
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
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shootTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            if (shootTimer > enemy.fireRate)
            {
                Shoot();
            }
            if (moveTimer > Random.Range(stateMachine.AS_moveTimeMin, stateMachine.AS_moveTimeMax))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * stateMachine.AS_randomMove));
                moveTimer = 0;
            }
            enemy.LastKnowPos = enemy.Player.transform.position;
        }
        else 
        {   if (stateMachine.SS_searchState)
            {
                losePlayerTimer += Time.deltaTime;
                if (losePlayerTimer > stateMachine.AS_losePlayerTime)
                {
                    //Change to the search state
                    stateMachine.ChangeState(new SearchState());
                }
            }
            else
            {
                //Change to the patrol state
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }
    
    public void Shoot()
    {
        //store reference to the gun barrel
        Transform gunBarrel = enemy.gunBarrel;

        //instantiate a new bullet
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunBarrel.position, enemy.transform.rotation);
        //calculate the direction to the player
        Vector3 shootDirection = (enemy.Player.transform.position - gunBarrel.transform.position).normalized;
        //add force rigidbody of the bullet
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-stateMachine.AS_differrence, stateMachine.AS_differrence), Vector3.up) * shootDirection * stateMachine.AS_speedBullet;
        shootTimer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
