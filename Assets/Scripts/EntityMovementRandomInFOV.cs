using System;
using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(EntityIdentity))]
public class EntityMovementRandomInFOV : MonoBehaviour
{
    public EntityIdentity entityID;
    public StateChecker stateChecker;
    public EntityFOV entityFOV;
    public Rigidbody rb;
    public GameObject ground;
    private Vector3 targetPos;
    private float t;
    private float timer;
    private Vector2 randomVect;
    private Vector3 randomDir;
    public bool isSkippingWall;
    private int test;
    
    private float worldAngle = 0;
    private float angleNormal = 0;
    
    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
        stateChecker = GetComponent<StateChecker>();
        entityFOV = GetComponent<EntityFOV>();
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
        transform.forward = Vector3.Lerp(transform.forward, targetPos - transform.position, Time.fixedDeltaTime * entityID.rotationSpeed);
    }

    private void RandomMovement(float speedMult)
    {
        if (timer >= entityID.refreshPathRate && !isSkippingWall)
        {
            timer = 0;

            float sectorAngle = entityID.fovAngle;  // tranche = FOV angle
            float halfAngle = sectorAngle / 2f; // Gauche Droite
            
            float randomAngle = Random.Range(-halfAngle, halfAngle);   // Angle Random

            float distance = entityID.fovRadius;

            worldAngle = WorldAngle(transform.forward);

            // transformation radian de l'enfer
            float x = distance * Mathf.Sin((randomAngle + worldAngle) * Mathf.Deg2Rad);
            float z = distance * Mathf.Cos((randomAngle + worldAngle) * Mathf.Deg2Rad);
            
            randomDir = (new Vector3(x, 0f, z).normalized + new Vector3(0f, transform.position.y, 0f)) * distance;
            // Coroutine rotation
        }
        
        if ((ClampingWallsNormals() != Vector3.zero) && (isSkippingWall == false) && Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, entityID.fovRadius))
        {
            isSkippingWall = true;
            angleNormal = WorldAngle(hit.normal);
        }
        else if (ClampingWallsNormals() == Vector3.zero)
        {
            isSkippingWall = false;
        }
        
        if (isSkippingWall)
        {
            if (Mathf.Abs(angleNormal - worldAngle) <= 180)
            {
                // randomDir = hit.normal;
                randomDir = transform.right;
                // StopCoroutine(ChangeDir());
                // StartCoroutine(ChangeDir());
            }
            else
            {
                // randomDir = hit.normal;
                randomDir = -transform.right;
                // StopCoroutine(ChangeDir());
                // StartCoroutine(ChangeDir());
            }
        } 
        
        float angle = Vector3.SignedAngle(transform.forward, randomDir, transform.up);
        if (Mathf.Abs(angle) >= 0.1f)
        {
            float maxStep = entityID.rotationSpeed * Time.fixedDeltaTime;
            float step = Mathf.Clamp(angle, -maxStep, maxStep);
            
            transform.Rotate(transform.up, step);
        }
        
        rb.linearVelocity = transform.forward.normalized * (entityID.nativeSpeed * speedMult);
    }

    private float WorldAngle(Vector3 dir)
    {
        float worldAngle = Vector3.Angle(Vector3.forward, dir);

        if (dir.x < 0)
        {
            worldAngle = 360 - worldAngle;
        }

        return worldAngle;
    }

    private IEnumerator ChangeDir()
    {
        while (true)
        {
            float angle = Vector3.SignedAngle(transform.forward, randomDir, transform.up);
            if (Mathf.Abs(angle) <= 0.1f)
                break;
            
            float maxStep = entityID.rotationSpeed * Time.fixedDeltaTime;
            float step = Mathf.Clamp(angle, -maxStep, maxStep);
            
            transform.Rotate(transform.up, step);
            yield return null;
        }
    }

    private Vector3 ClampingWallsNormals()
    {
        Vector3 totalNormals = Vector3.zero;
        
        foreach (Vector3 wall in entityFOV.wallsWithinFOV)
        {
            totalNormals += wall;
        }
        

        return totalNormals;
    }

    private void FindNewPos()
    {
        randomVect = Random.insideUnitCircle.normalized;
        targetPos = new Vector3(randomVect.x, 0, randomVect.y) * entityID.refreshPathRate;
        targetPos.x += transform.position.x;
        targetPos.z += transform.position.z;
        //ClampingOnGround();
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
