using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    public LayerMask groundMask, playerMask;
    public bool canAttack, canSee;
    bool attacked;
    public float sightRange, attackRange;


    void Awake()
    {
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        
    }
}
