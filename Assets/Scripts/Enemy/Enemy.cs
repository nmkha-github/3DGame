using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 100;

    private StateMachine stateMachine;
    private StateSoldier stateSoldier;
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 lastKnowPos;
    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }
    public Vector3 LastKnowPos { get => lastKnowPos; set => lastKnowPos = value; }

    public Path path;
    [Header("Sight Values")]
    public float sighDistance = 20f;
    public float fielOfView = 85f;
    public float eyeHeight;
    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate;
    //Just for debugging purposes
    [SerializeField]
    private string currentState;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        stateSoldier = GetComponent<StateSoldier>();
        agent = GetComponent<NavMeshAgent>();

        if (stateMachine != null)
        {
            stateMachine.Initialise();
        }
        if (stateSoldier != null)
        {
            stateSoldier.Initialise();
        }
        
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //CanSeePlayer();
        if (stateMachine != null)
        {
            currentState = stateMachine.activeState.ToString();
        }
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < sighDistance)
            {
                Vector3 targetDirection = player.transform.position - (transform.position + Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fielOfView && angleToPlayer <= fielOfView)
                {
                    Ray ray = new Ray(transform.position + Vector3.up * eyeHeight, targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo, sighDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sighDistance);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(Resources.Load("Prefabs/Explosion") as GameObject, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
