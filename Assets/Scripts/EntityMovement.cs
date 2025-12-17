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
    public Rigidbody rb;
    public GameObject ground;
    private Vector3 targetPos;
    private float t;
    private float timer;
    private Vector2 randomVect;
    private Vector3 randomDir;
    
    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
        stateChecker = GetComponent<StateChecker>();
        rb = GetComponent<Rigidbody>();
        ground = GameObject.FindGameObjectWithTag("Ground");
        
        //Rotation aléatoire au start
        Vector2 randPos = Random.insideUnitCircle;
        Vector3 initialDir = new Vector3(randPos.x + transform.position.x, transform.position.y, randPos.y + transform.position.z);
        transform.forward = (initialDir - transform.position).normalized;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            switch (entityID.state)
            {
                case State.Idle:
                    RandomMovement(1f);
                    break;
                case State.Chasing:
                    StateCheckedMovement(entityID.speedModifierWhenChasing);
                    break;
                case State.Fleeing:
                    StateCheckedMovement(entityID.speedModifierWhenFleeing);
                    break;
                case State.Fatigued:
                    RandomMovement(entityID.speedModifierWhenFatigued);
                    break;
                case State.Fighting:
                    //todo
                    break;
                case State.Reproducing:
                    //todo
                    break;
                default:
                    RandomMovement(1f);
                    break;
            }
        }
    }

    private void StateCheckedMovement(float speedMult)
    {
        rb.linearVelocity = transform.forward.normalized * (entityID.nativeSpeed * speedMult);
        targetPos = stateChecker.targetPos;
        targetPos = ClampingOnGround(targetPos);
        transform.forward = Vector3.Lerp(transform.forward, targetPos - transform.position, Time.fixedDeltaTime * entityID.rotationSpeed);
    }

    private void RandomMovement(float speedMult)
    {
        if (timer >= entityID.refreshPathRate)
        {
            timer = 0;

            float sectorAngle = entityID.fovAngle;  // tranche = FOV angle
            float halfAngle = sectorAngle / 2f; // Gauche Droite
            
            float randomAngle = Random.Range(-halfAngle, halfAngle);   // Angle Random

            float distance = entityID.fovRadius;
            
            float worldAngle = Vector3.Angle(Vector3.forward, transform.forward);
            
            if (transform.forward.x < 0)
            {
                worldAngle = 360 - worldAngle;
            }
            
            // transformation radian de l'enfer
            float x = distance * Mathf.Sin((randomAngle + worldAngle) * Mathf.Deg2Rad);
            float z = distance * Mathf.Cos((randomAngle + worldAngle) * Mathf.Deg2Rad);
            
            randomDir = new Vector3(x, 0f, z).normalized * distance;
            //targetPos = ClampingOnGround(transform.position + randomDir);
        }
        rb.linearVelocity = transform.forward.normalized * (entityID.nativeSpeed * speedMult);
        transform.forward = Vector3.Lerp(transform.forward, randomDir, Time.fixedDeltaTime * entityID.rotationSpeed);
    }

    private Vector3 ClampingOnGround(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, -ground.transform.localScale.x * 5f, ground.transform.localScale.x * 5f);
        pos.z = Mathf.Clamp(pos.z, -ground.transform.localScale.z * 5f, ground.transform.localScale.z * 5f);
        return new Vector3(pos.x, 0, pos.z);
    }

    private void FindNewPos()
    {
        randomVect = Random.insideUnitCircle.normalized;
        targetPos = new Vector3(randomVect.x, 0, randomVect.y) * entityID.refreshPathRate;
        targetPos.x += transform.position.x;
        targetPos.z += transform.position.z;
        ClampingOnGround(targetPos);
    }

    private void OnDrawGizmos()
    {
        if (rb)
        {
            switch (entityID.state)
            {
                case State.Idle:
                    Gizmos.color = Color.green;
                    Gizmos.DrawRay(transform.position, randomDir);
                    break;
                case State.Chasing:
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(transform.position, targetPos);
                    break;
                case State.Fleeing:
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(transform.position, targetPos);
                    break;
                case State.Fatigued:
                    Gizmos.color = Color.blue;
                    Gizmos.DrawRay(transform.position, randomDir);
                    break;
                case State.Fighting:
                    Gizmos.color = Color.black;
                    break;
                case State.Reproducing:
                    Gizmos.color = Color.magenta;
                    break;
            }
            //Gizmos.DrawLine(transform.position, targetPos);
        }
    }
}
