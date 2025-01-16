using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    Animator animator;
    public NavMeshAgent agent;


    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        
        if (agent.hasPath)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }
}
