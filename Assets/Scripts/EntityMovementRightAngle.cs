using System;
using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(EntityIdentity))]
public class EntityMovementRightAngle : MonoBehaviour
{
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
        targetPos = stateChecker.targetPos;
        randomDir = (targetPos - transform.position).normalized;
        ClampingOnGroundWithRightAngle();
        
        int randomN = Random.Range(0, 2);
        float angle = Vector3.SignedAngle(transform.forward, randomDir, transform.up);
        float randomAngle = 0f;
        if (angle >= 0f && angle < 90f)
        {
            if (randomN == 1)
            {
                randomAngle = 90f;
            }
        }
        else if (angle >= 90f && angle < 180f)
        {
            randomAngle = 90f;
        }
        else if (angle >= -90f && angle < 0f)
        {
            if (randomN == 1)
            {
                randomAngle = -90f;
            }
        }
        else if (angle >= -180f && angle < -90f)
        {
            randomAngle = -90f;
        }
            
        randomDir = Quaternion.Euler(0f, randomAngle, 0f) * transform.forward;
        randomDir.Normalize();
        transform.forward = Vector3.Lerp(transform.forward, randomDir, Time.fixedDeltaTime * entityID.rotationSpeed);
        rb.linearVelocity = transform.forward.normalized * (entityID.nativeSpeed * speedMult);
    }

    private void RandomMovement(float speedMult)
    {
        ClampingOnGroundWithRightAngle();

        if (timer >= entityID.refreshPathRate)
        {
            timer = 0;
            
            int randomN = Random.Range(0, 3);
            float randomAngle = 0f;
            if (randomN == 1)
                randomAngle = -90f;
            else if (randomN == 2)
                randomAngle = 90f;

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
        
        rb.linearVelocity = transform.forward * (entityID.nativeSpeed * speedMult);
    }

    private void ClampingOnGroundWithRightAngle()
    {
        Vector3 aheadPos = transform.position + transform.forward * entityID.fovRadius;
        if (!groundBounds.Contains(new Vector3(aheadPos.x, ground.transform.position.y, aheadPos.z)) && !hasAvoidingVector)
        {
            hasAvoidingVector = true;
            timer = 0;
            SearchClampingParameters(aheadPos);
            float avoidingAngle = 90 * rotationOrientation;
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

