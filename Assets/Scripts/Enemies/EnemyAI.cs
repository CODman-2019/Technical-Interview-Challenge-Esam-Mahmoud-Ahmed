using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float          walkingSpeed;
    [SerializeField] private float          patrolPointDistance;
    [SerializeField] private NavMeshAgent   agent;
    [SerializeField] private GameObject     playerTarget;

    private enum state
    {
        patrol,
        chase,
        attack,
        finish
    };

    private state                           currentState;
    private int                             patrolCounter;
    private GameObject[]                    patrolsPointObjects;
    private Rigidbody                       rb;


    // Start is called before the first frame update
    void Start()
    {
        //setting up state machine
        currentState = state.patrol;
        //getting rigidbody component
        rb = GetComponent<Rigidbody>();

        agent = GetComponent<NavMeshAgent>();

        //patrol setup
        patrolsPointObjects = GameObject.FindGameObjectsWithTag("EnemyPatrol");
        patrolCounter = patrolsPointObjects.Length - 1;
        
        //finding player character and setting destination to first patrol point
        playerTarget = GameObject.Find("Player");
        agent.SetDestination(patrolsPointObjects[patrolCounter].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == state.patrol)
        {
            if(Vector3.Distance(transform.position, patrolsPointObjects[patrolCounter].transform.position) < patrolPointDistance)
            {
                ChangeToNextPatrolPoint();
            }
        }
    }

    private void ChangeToNextPatrolPoint()
    {
        patrolCounter--;
        if (patrolCounter < 0) patrolCounter = patrolsPointObjects.Length - 1;
        Debug.Log("changing target");
        agent.SetDestination(patrolsPointObjects[patrolCounter].transform.position);
    }
}
