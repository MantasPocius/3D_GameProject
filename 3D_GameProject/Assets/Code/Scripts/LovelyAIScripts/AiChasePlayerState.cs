using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class AiChasePlayerState : AiState
{
    
    float timer = 0.0f;


    public void Enter(AiAgent agent)
    {

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
            agent.navMeshAgent.destination = agent.target.position;
        }
        if (timer < 0.0f)
        {
            Vector3 direction = (agent.target.position - agent.navMeshAgent.destination);
            direction.y = 0;
            if (direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
            {
                if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = agent.target.position;
                }
            }
            timer = agent.config.maxTime;
        }
    }
}
