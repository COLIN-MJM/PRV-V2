using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(EntityIdentity))]
public class EntityMovement : MonoBehaviour
{
    public EntityIdentity entityID;
    public StateChecker stateChecker;
    public NavMeshAgent agent;
    public GameObject ground;
    private Vector3 targetPos;
    private float t;
    private float timer;
    private bool recalculating;
    
    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
        stateChecker = GetComponent<StateChecker>();
        agent = GetComponent<NavMeshAgent>();
        ground = GameObject.FindGameObjectWithTag("Ground");
        InvokeRepeating(nameof(RefreshPath), 0f, 0.2f);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= entityID.refreshPathRate / 15)
        {
            recalculating = true;
        }
    }

    private void RefreshPath()
    {
        if (agent.isOnNavMesh)
        {
            switch (entityID.state)
            {
                case State.Idle:
                    agent.speed = entityID.nativeSpeed;
                    if((agent.pathPending != true && agent.remainingDistance < 0.5f) || recalculating)
                    {
                        targetPos = new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y).normalized * entityID.refreshPathRate;
                        ClampingOnGround(targetPos);
                        agent.SetDestination(targetPos);
                        timer = 0;
                        recalculating = false;
                    }
                    break;
                case State.Chasing:
                    Chase();
                    break;
                case State.Fleeing:
                    Flee();
                    break;
                case State.Fatigued:
                    Fatigue();
                    break;
                case State.Fighting:
                    agent.speed = 0f;
                    //todo
                    break;
                case State.Reproducing:
                    agent.speed = 0f;
                    //todo
                    break;
                default:
                    agent.speed = entityID.nativeSpeed;
                    if((agent.pathPending != true && agent.remainingDistance < 0.5f) || recalculating)
                    {
                        targetPos = new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y).normalized * entityID.refreshPathRate;
                        ClampingOnGround(targetPos);
                        agent.SetDestination(targetPos);
                        timer = 0;
                        recalculating = false;
                    }
                    break;
            }
        }
    }

    private void Flee()
    {
        agent.speed = entityID.nativeSpeed * entityID.speedModifierWhenFleeing;
        targetPos = stateChecker.targetPos; 
        agent.SetDestination(targetPos);
    }

    private void Chase()
    {
        agent.speed = entityID.nativeSpeed * entityID.speedModifierWhenChasing;
        targetPos = stateChecker.targetPos; 
        agent.SetDestination(targetPos);
    }

    private void Fatigue()
    {
        agent.speed = entityID.nativeSpeed * entityID.speedModifierWhenFatigued;
        if(agent.pathPending != true && agent.remainingDistance < 0.5f)
        {
            targetPos = new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y).normalized * entityID.refreshPathRate;
            ClampingOnGround(targetPos);
            agent.SetDestination(targetPos);
        }
    }

    private void ClampingOnGround(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, -ground.transform.localScale.x * 5f, ground.transform.localScale.x * 5f);
        pos.z = Mathf.Clamp(pos.z, -ground.transform.localScale.z * 5f, ground.transform.localScale.z * 5f);
    }

    private void OnDrawGizmos()
    {
        if (agent.isOnNavMesh)
        {
            switch (entityID.state)
            {
                case State.Idle:
                    Gizmos.color = Color.green;
                    break;
                case State.Chasing:
                    Gizmos.color = Color.red;
                    break;
                case State.Fleeing:
                    Gizmos.color = Color.yellow;
                    break;
                case State.Fatigued:
                    Gizmos.color = Color.blue;
                    break;
                case State.Fighting:
                    Gizmos.color = Color.black;
                    break;
                case State.Reproducing:
                    Gizmos.color = Color.magenta;
                    break;
            }
            Gizmos.DrawLine(transform.position, targetPos);
        }
    }
}
