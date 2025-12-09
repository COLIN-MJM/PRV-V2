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
            rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        }
        
        if (clock >= timer)
        {
            // Rotation aléatoire
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
                    
                    // éviter l'obstacle

                    something = false;
                }
            }
        }
    }
}