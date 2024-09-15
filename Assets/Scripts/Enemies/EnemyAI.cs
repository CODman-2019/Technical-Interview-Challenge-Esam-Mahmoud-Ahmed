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
    [SerializeField] private float          playerDetectionDistance;
    [SerializeField] private float          playerEscapeDistance;
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

    // Start is called before the first frame update
    void Start()
    {
        //setting up state machine
        currentState = state.patrol;
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
        if (!GameManager.gameManager.IsGamePause())
        {
            switch (currentState)
            {
                case state.patrol:
                    if (Vector3.Distance(transform.position, patrolsPointObjects[patrolCounter].transform.position) < patrolPointDistance)
                    {
                        ChangeToNextPatrolPoint();
                    }

                    if(Vector3.Distance(transform.position, playerTarget.transform.position) < playerDetectionDistance)
                    {
                        currentState = state.chase;
                    }

                    break;
                case state.chase:
                    agent.SetDestination(playerTarget.transform.position);
                    if(Vector3.Distance(transform.position, playerTarget.transform.position) > playerEscapeDistance)
                    {
                        Debug.Log("player escaped");
                        currentState= state.patrol;
                        agent.SetDestination(patrolsPointObjects[patrolCounter].transform.position);
                    }
                    break;
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
