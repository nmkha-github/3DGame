using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int wayPointIndex;
    public float waitTimes;
    public override void Enter()
    {
    }
    public override void Perform()
    {
        PatrolCycle();
    }
    public override void Exit()
    {
    }

    public void PatrolCycle()
    {
        waitTimes += Time.deltaTime;
        if (waitTimes >= 3)
        {
            waitTimes = 0;

            if (enemy.Agent.remainingDistance < 0.2f)
            {
                if (wayPointIndex < enemy.path.waypoints.Count - 1)
                    wayPointIndex++;
                else
                    wayPointIndex = 0;
                enemy.Agent.SetDestination(enemy.path.waypoints[wayPointIndex].position);
            }
        }
    }
}
