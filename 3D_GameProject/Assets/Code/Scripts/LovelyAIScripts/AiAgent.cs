using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public Ragdoll ragdoll;
    public SkinnedMeshRenderer mesh;
    public Transform target;
    public Animator animator;
    public GameObject sphere;
    public GameObject effect;
    public GameObject CasePrefab;
    public Transform shellEjectPoint;
    private float shellEjectDelay = 0.6f;
    public ParticleSystem muzzleFlash;
    public int maxAmmo = 10;
    public int currentAmmo;
    private float fireRate = 1f;
    public float damage = 10;
    public bool isReadyToFire = false;
    public Transform targetTransform;
    public Transform aimTransform;
    public float attackDelay;
    public Collider Collider;
    public Health health;
    public Collider attackRange;

    void Start()
    {
        animator = GetComponent<Animator>();
        ragdoll = GetComponent<Ragdoll>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiShootPlayerState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
