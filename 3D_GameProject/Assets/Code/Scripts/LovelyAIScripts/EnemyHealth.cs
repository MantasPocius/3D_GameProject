using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;

    [HideInInspector] public float currentHealth;
    AiAgent agent;
    SkinnedMeshRenderer SkinnedMeshRenderer;


    void Start()
    {
        agent = GetComponent<AiAgent>();
        SkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        currentHealth = maxHealth;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach(var rigidBody in rigidBodies)
        {
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.health = this;
        }
    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            Die(direction);
        }
    }

    public void Die(Vector3 direction)
    {
        AiDeathState deathState = agent.stateMachine.GetState(AiStateId.Death) as AiDeathState;
        deathState.direction = direction;
        agent.stateMachine.ChangeState(AiStateId.Death);
    }
}
