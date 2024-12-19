using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }


    void Update()
    {
        if(agent.enabled)
        {
            agent.SetDestination(target.position);
        }
        if(agent.remainingDistance < 30f)
        {

        }
    }
}
