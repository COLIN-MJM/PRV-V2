using System;
using UnityEditor;
using UnityEngine;
using Input = UnityEngine.Input;
using Random = UnityEngine.Random;

public class RB_Movement : MonoBehaviour
{
    public float speed;
    public float FOV;
    public float angleFOV;
    private bool something;
    private Collider selfCollider;
    public Collider[] colliderInFOV;
    private Rigidbody rb;
    public Vector3 randomDir;
    public float rotateSpeed;

    private float clock;
    public float timer;

    private void OnDrawGizmos()
    {
        Handles.color = something ? new Color(1,0,0,0.5f) : new Color(0,1,0,0.5f);
        Handles.DrawSolidArc(transform.position, transform.up, Quaternion.AngleAxis(-angleFOV/2f, transform.up) * transform.forward, angleFOV, FOV);
        Handles.color = new Color(0,1,0,0.5f);
        FOV = Handles.ScaleValueHandle(FOV, transform.position + transform.forward * FOV, transform.rotation, 3, Handles.SphereHandleCap, 1);
    }

    void Start()
    {
        clock = 0;
        selfCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        rb.maxLinearVelocity = speed;
    }

    void Update()
    {
        clock += Time.deltaTime;
        colliderInFOV = Physics.OverlapSphere(transform.position, FOV);
    }

    private void FixedUpdate()
    {
        if (!something)
        {
            // rb.AddForce(transform.forward.normalized * speed, ForceMode.Acceleration);
            rb.linearVelocity = transform.forward.normalized * speed;
            transform.forward = Vector3.Lerp(transform.forward, randomDir, Time.fixedDeltaTime * rotateSpeed);
        }
        
        if (clock >= timer)
        {
            clock = 0;
            // rb.linearVelocity = Vector3.zero;

            float sectorAngle = angleFOV;  // tranche = FOV angle
            float halfAngle = sectorAngle / 2f; // Gauche Droite
            
            float randomAngle = Random.Range(-halfAngle, halfAngle);   // Angle Random

            float distance = FOV;
            
            float worldAngle = Vector3.Angle(Vector3.forward, transform.forward);
            
            if (transform.forward.x < 0)
            {
                worldAngle = 180 + (180 - worldAngle);
            }
            
            // Maths (randomAngle, randomDistance) to Cartesian (x, z)
            float x = distance * Mathf.Sin((randomAngle + worldAngle) * Mathf.Deg2Rad);
            float z = distance * Mathf.Cos((randomAngle + worldAngle) * Mathf.Deg2Rad);
            
            randomDir = new Vector3(x, 0f, z).normalized * distance;
        }
        
        foreach (Collider collide in colliderInFOV)
        {
            if (collide != selfCollider)
            {
                float signedAngle = Vector3.Angle(transform.forward, collide.transform.position - transform.position);
                if (Mathf.Abs(signedAngle) < angleFOV / 2f)
                {
                    something = true;
                    
                    rb.linearVelocity = Vector3.zero;

                    something = false;
                }
            }
        }
    }
}