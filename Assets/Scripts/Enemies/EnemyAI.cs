using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float           patrolPointDistance;
    [SerializeField] private float           playerDetectionDistance;
    [SerializeField] private float           playerEscapeDistance;
    [SerializeField] private float           playerAttackDistance;
    [SerializeField] private bool            patrolreset;
    [SerializeField] private NavMeshAgent    agent;
    [SerializeField] private GameObject      playerTarget;
    [SerializeField] private GameObject[]    patrolsPointObjects;

    private Animator anim;

    private enum state
    {
        patrol,
        chase,
        attack
    };

    private state                             currentState;
    private int                              patrolCounter;
    private int                              patrolDirection;
    private bool                             canAttack;
    

    // Start is called before the first frame update
    void Start()
    {
        //setting up state machine
        currentState = state.patrol;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //patrol setup
        patrolCounter = 0;
        patrolDirection = 1;
        
        //finding player character and setting destination to first patrol point
        playerTarget = GameObject.Find("Player");
        agent.SetDestination(patrolsPointObjects[patrolCounter].transform.position);

        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameManager.IsGamePause() || !GameManager.gameManager.IsGameOver())
        {
            if (agent.isStopped) { agent.isStopped = false; }

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
                    else if (Vector3.Distance(transform.position,playerTarget.transform.position) < playerAttackDistance)
                    {
                        currentState = state.attack;
                    }
                    break;
                case state.attack:

                    agent.SetDestination(playerTarget.transform.position);
                    if (playerTarget.GetComponent<PlayerCharacter>().GetHealth() > 2)
                    {
                        if (canAttack)
                        {
                            anim.SetTrigger("Basic");
                            canAttack = false;
                        }
                        else
                        {
                            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
                            {
                                canAttack = true;
                            }
                        }
                    }
                    else
                    {
                        if (canAttack)
                        {
                            anim.SetTrigger("Finisher");
                            canAttack = false;
                        }
                        else
                        {
                            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
                            {
                                canAttack = true;
                            }
                        }
                    }

                    if (Vector3.Distance(transform.position, playerTarget.transform.position) > playerAttackDistance)
                    {
                        currentState = state.chase;
                    }
                    break;
            }
        }
        else { agent.isStopped = true; }
    }

    private void ChangeToNextPatrolPoint()
    {
        patrolCounter += patrolDirection;
        if (patrolCounter == patrolsPointObjects.Length || patrolCounter == -1)
        {
            if (patrolreset && patrolCounter == patrolsPointObjects.Length)
            {
                patrolCounter = 0;
            }
            else
            {
                patrolDirection *= -1;
                patrolCounter += patrolDirection;
            }
        }
        agent.SetDestination(patrolsPointObjects[patrolCounter].transform.position);
    }

    private void playSound(int type)
    {
        switch(type)
        {
            case 0:
                AudioManager.sound.TriggerSound("EnemyBasic");
                break;
            case 1:
                AudioManager.sound.TriggerSound("EnemyFinisher");
            break;
        }
    }

}
