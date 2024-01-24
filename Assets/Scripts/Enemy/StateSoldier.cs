using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSoldier : MonoBehaviour
{
    public BaseState activeState;

    public float moveTimeMin;
    public float moveTimeMax;
    public float randomMove;

    public void Initialise()
    {
        ChangeState(new FollowState());
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        //check activeState != null
        if (activeState != null)
        {
            //run cleanup on activeState
            activeState.Exit();
        }
        //change to a new state
        activeState = newState;

        //fail-safe null check to make sure new state wasn't null
        if (activeState != null)
        {
            //Setup new state
            activeState.stateSoldier = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
