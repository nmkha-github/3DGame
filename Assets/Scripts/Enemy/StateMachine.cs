using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;

    [Header("Patrol State")]
    public float PS_waitTime = 3f;
    public PatrolState patrolState;

    [Header("Attack State")]
    public float AS_speedBullet = 40f;
    public float AS_moveTimeMin = 3f;
    public float AS_moveTimeMax = 7f;
    public float AS_losePlayerTime = 2f;
    public float AS_randomMove = 5f;
    public float AS_differrence = 3;

    [Header("Search State")]
    public bool SS_searchState = true;
    public float SS_searchTime = 10f;
    public float SS_moveTimeMin = 3f;
    public float SS_moveTimeMax = 5f;
    public float SS_randomMove = 10f;

    public void Initialise()
    {
        ChangeState(new PatrolState());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        //check activeState != null
        if(activeState != null)
        {
            //run cleanup on activeState
            activeState.Exit();
        }
        //change to a new state
        activeState = newState;

        //fail-safe null check to make sure new state wasn't null
        if(activeState != null)
        {
            //Setup new state
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
