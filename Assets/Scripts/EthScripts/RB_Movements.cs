using System;
using UnityEditor;
using UnityEngine;
using Input = UnityEngine.Input;
using Random = UnityEngine.Random;

public class RB_Movement : MonoBehaviour
{
    private EntityIdentity entityID;
    private Vector3 randomDir;
    
    private float clock;
    private Rigidbody rb;

    void Start()
    {
        entityID = GetComponent<EntityIdentity>();
        rb = GetComponent<Rigidbody>();

        rb.maxLinearVelocity = entityID.nativeSpeed;
        clock = 0;
    }

    void Update()
    {
        clock += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.forward.normalized * entityID.nativeSpeed;
        transform.forward = Vector3.Lerp(transform.forward, randomDir, Time.fixedDeltaTime * entityID.rotationSpeed);
        
        if (clock >= entityID.refreshPathRate)
        {
            clock = 0;

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
        }
    }
}