using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AiDeathState : AiState
{
    public Vector3 direction;
    public static int KeyEnemyKilled = 0;
    public void Enter(AiAgent agent)
    {
        agent.health.currentHealth += 5;
        if (agent.tag == "KeyEnemy" && agent.animator.enabled)
        {
            agent.effect.SetActive(false);
            KeyEnemyKilled++;
            Debug.Log(KeyEnemyKilled);
        }
        if (KeyEnemyKilled == 4)
        {
            agent.sphere.SetActive(false);
        }
        //keyEnemyCheck.OnEnemyKilled(agent);
        agent.ragdoll.ActivateRagdoll();
        direction.y = 1;
        agent.ragdoll.ApplyForce(direction * agent.config.dieForce);
    }

    public void Exit(AiAgent agent)
    {
        
    }

    public AiStateId GetId()
    {
        return AiStateId.Death;
    }

    public void Update(AiAgent agent)
    {
        
    }
}
