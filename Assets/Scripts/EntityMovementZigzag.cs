using System;
using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(EntityIdentity))]
public class EntityMovementZigzag : MonoBehaviour
{
    public float zigzagPower = 1f;
    public float zigzagAmplitude = 1f;
    public float zigzagTendency = 1f;
    
    public EntityIdentity entityID;
    public StateChecker stateChecker;
    public EntityFOV entityFOV;
    public Rigidbody rb;
    public GameObject ground;
    private Bounds groundBounds;
    private Vector3 targetPos;
    private float timer;
    private Vector3 randomDir;
    
    // Variables de vérification d'avoidance des murs
    private Vector3 refVector = Vector3.zero;
    private float rotationOrientation = 0f;
    private bool hasAvoidingVector = false;

    private Vector3 zigzagVector;
    private bool zigzagBase = true;
    private float timerZigzag = 0f;
    
    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
        stateChecker = GetComponent<StateChecker>();
        entityFOV = GetComponent<EntityFOV>();
        rb = GetComponent<Rigidbody>();
        ground = GameObject.FindGameObjectWithTag("Ground");
        groundBounds = ground.GetComponent<Collider>().bounds;
        
        //Rotation aléatoire au start
        Vector2 randPos = Random.insideUnitCircle;
        Vector3 initialDir = new Vector3(randPos.x + transform.position.x, transform.position.y, randPos.y + transform.position.z);
        transform.forward = (initialDir - transform.position).normalized;

        zigzagVector = transform.right;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timerZigzag += Time.deltaTime;
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
        targetPos = stateChecker.targetPos;
        randomDir = (targetPos - transform.position).normalized;
        ClampingOnGround();
        transform.forward = Vector3.Lerp(transform.forward, randomDir, Time.fixedDeltaTime * entityID.rotationSpeed);
        ZigzagControl();
        rb.linearVelocity = (transform.forward/zigzagPower + zigzagVector).normalized * (entityID.nativeSpeed * speedMult * zigzagPower);
    }

    private void RandomMovement(float speedMult)
    {
        ClampingOnGround();

        if (timer >= entityID.refreshPathRate)
        {
            timer = 0;

            float sectorAngle = entityID.fovAngle;  // tranche = FOV angle
            float halfAngle = sectorAngle / 2f; // Gauche Droite
            
            float randomAngle = Random.Range(-halfAngle, halfAngle);   // Angle Random

            randomDir = Quaternion.Euler(0f, randomAngle, 0f) * transform.forward;
            randomDir.Normalize();
        }
        
        float angle = Vector3.SignedAngle(transform.forward, randomDir, transform.up);
        if (Mathf.Abs(angle) >= 0.1f)
        {
            float maxStep = entityID.rotationSpeed;
            float step = Mathf.Clamp(angle, -maxStep, maxStep);
            
            transform.Rotate(transform.up, step);
        }
        
        ZigzagControl();
        rb.linearVelocity = (transform.forward/zigzagPower + zigzagVector).normalized * (entityID.nativeSpeed * speedMult * zigzagPower);
    }

    private void ZigzagControl()
    {
        if (zigzagBase)
        {
            zigzagVector = Vector3.Lerp(zigzagVector, -transform.right, Time.fixedDeltaTime * zigzagTendency);
        }
        else
        {
            zigzagVector = Vector3.Lerp(zigzagVector, transform.right, Time.fixedDeltaTime * zigzagTendency);
        }

        if (timerZigzag >= zigzagAmplitude)
        {
            zigzagBase = !zigzagBase;
            timerZigzag = 0;
        }
    }

    private void ClampingOnGround()
    {
        Vector3 aheadPos = transform.position + transform.forward * entityID.fovRadius;
        if (!groundBounds.Contains(new Vector3(aheadPos.x, ground.transform.position.y, aheadPos.z)) && !hasAvoidingVector)
        {
            hasAvoidingVector = true;
            timer = 0;
            SearchClampingParameters(aheadPos);
            float avoidingAngle = (Vector3.Angle(transform.forward, refVector) + 2f) * rotationOrientation;
            randomDir = Quaternion.Euler(0f, avoidingAngle, 0f) * transform.forward;
            randomDir.Normalize();
        }

        if (hasAvoidingVector)
        {
            timer = 0;
            if (Vector3.Angle(transform.forward, randomDir) < 1f)
            {
                hasAvoidingVector = false;
            }
        }
    }

    private void SearchClampingParameters(Vector3 aheadPos)
    {
        if (aheadPos.x < groundBounds.min.x)
        {
            if (aheadPos.z >= transform.position.z)
            {
                refVector = Vector3.forward;
                rotationOrientation = 1f;
            }
            else
            {
                refVector = -Vector3.forward;
                rotationOrientation = -1f;
            }
        }
        else if (aheadPos.x > groundBounds.max.x)
        {
            if (aheadPos.z >= transform.position.z)
            {
                refVector = Vector3.forward;
                rotationOrientation = -1f;
            }
            else
            {
                refVector = -Vector3.forward;
                rotationOrientation = 1f;
            }
        }
        else if (aheadPos.z < groundBounds.min.z)
        {
            if (aheadPos.x >= transform.position.x)
            {
                refVector = Vector3.right;
                rotationOrientation = -1f;
            }
            else
            {
                refVector = -Vector3.right;
                rotationOrientation = 1f;
            }
        }
        else if (aheadPos.z > groundBounds.max.z)
        {
            if (aheadPos.x >= transform.position.x)
            {
                refVector = Vector3.right;
                rotationOrientation = 1f;
            }
            else
            {
                refVector = -Vector3.right;
                rotationOrientation = -1f;
            }
        }
    }

    // Coroutine (pas encore appelée)
    private IEnumerator ChangeDir()
    {
        while (true)
        {
            float angle = Vector3.SignedAngle(transform.forward, randomDir, transform.up);
            if (Mathf.Abs(angle) <= 0.1f)
                // todo mettre la valeur à ce qu'elle est réelement 
                break;
            
            float maxStep = entityID.rotationSpeed * Time.fixedDeltaTime;
            float step = Mathf.Clamp(angle, -maxStep, maxStep);
            
            transform.Rotate(transform.up, step);
            yield return null;
        }
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
