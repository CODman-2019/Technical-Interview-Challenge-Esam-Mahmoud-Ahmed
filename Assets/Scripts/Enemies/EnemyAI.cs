using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float          walkingSpeed;
    [SerializeField] private Transform[]    patrolPoints;
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
    private GameObject[] patrolsPointObjects;


    // Start is called before the first frame update
    void Start()
    {
        //setting up state machine and setting up counter for patrl routeing
        currentState = state.patrol;
        patrolCounter = 0;

        //looking for all patrol points and setting up index for position sets
        patrolsPointObjects = GameObject.FindGameObjectsWithTag("EnemyPatrol");
        patrolPoints = new Transform[patrolsPointObjects.Length];
        int index = patrolPoints.Length-1;

        //getting positions pof 
        foreach (GameObject p in patrolsPointObjects)
        {
            patrolPoints[index] = p.transform;
            index--;
        }
        
        playerTarget = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
