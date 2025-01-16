using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class AiChasePlayerState : AiState
{
    public Transform target;
    float timer = 0.0f;


    public void Enter(AiAgent agent)
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void Exit(AiAgent agent)
    {
        
    }

    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }

    public void Update(AiAgent agent)
    {
        if (!agent.navMeshAgent.enabled)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = target.position;
        }
        if (timer < 0.0f)
        {
            Vector3 direction = (target.position - agent.navMeshAgent.destination);
            direction.y = 0;
            if (direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
            {
                if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = target.position;
                }
            }
            timer = agent.config.maxTime;
        }
    }
}
