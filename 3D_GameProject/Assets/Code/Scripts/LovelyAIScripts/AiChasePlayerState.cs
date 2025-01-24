using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class AiChasePlayerState : AiState
{
    
    float timer = 0.0f;


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
            agent.animator.SetBool("IsWalking", true);
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
                    agent.animator.SetBool("IsWalking", true);
                }
            }
            timer = agent.config.maxTime;
        }
        if (Physics.Raycast(agent.targetTransform.position, agent.aimTransform.position + agent.aimTransform.forward * 50, out hit))
        {
            if (hit.collider.tag == "Player")
            {
                agent.Collider.gameObject.SetActive(true);
                Wait(agent.attackDelay);
                Debug.Log("hit");
                Debug.Log(agent.damage);
                agent.health.TakeDamage(agent.damage);
                agent.Collider.gameObject.SetActive(false);
            }
        }
    }
    public void OnTriggerEnter(Collider other, AiAgent agent)
    {
        if(agent)
        { }
        if(agent.Collider.tag == "Sword" && other.tag == "Player")
        {
            agent.health.TakeDamage(agent.damage);
            agent.Collider.gameObject.SetActive(false);
        }
    }
    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }
}
