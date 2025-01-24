using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using JetBrains.Annotations;

public class AiChasePlayerState : AiState
{
    
    float timer = 0.0f;
    public Health health;


    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.destination = agent.target.position;
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
        RaycastHit hit;
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
        if(!agent.navMeshAgent.hasPath && Physics.Raycast(agent.aimTransform.position, agent.aimTransform.position + agent.aimTransform.forward * 50, out hit))
        {
            if(hit.collider.tag == "Player")
            {
                WaitForSecondsRealtime(2);
                Debug.Log("hit");
                Debug.Log(agent.damage);
                health.TakeDamage(agent.damage);
            }
        }
        
    }
    public IEnumerator WaitForSecondsRealtime(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }
}
