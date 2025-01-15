using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    public float maxTime = 1.0f;
    public float maxDistance = 10.0f;
    Animator animator;
    float timer = 0.0f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            float sqDistance = (target.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance * maxDistance)
            {
                agent.destination = target.position;
            }
            timer = maxTime;
            agent.destination = target.position;
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
