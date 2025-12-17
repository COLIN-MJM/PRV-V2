using System;
using UnityEditor;
using UnityEngine;
using Input = UnityEngine.Input;
using Random = UnityEngine.Random;

public class Movement_01 : MonoBehaviour
{
    private EntityIdentity entityID;
    private Vector3 randomDir;

    private float clock;
    private float waitClock;
    public float coolDown;

    private bool isRotating;
    private bool isWaiting;

    private Rigidbody rb;

    void Start()
    {
        entityID = GetComponent<EntityIdentity>();
        rb = GetComponent<Rigidbody>();

        clock = 0;
    }

    void Update()
    {
        if (!isWaiting && !isRotating)
            clock += Time.deltaTime;

        if (isWaiting)
            waitClock += Time.deltaTime;

        if (isRotating)
        {
            transform.forward = Vector3.Lerp(transform.forward, randomDir, Time.deltaTime * entityID.rotationSpeed);
        }
    }

    private void FixedUpdate()
    {
        if (clock >= entityID.refreshPathRate)
        {
            clock = 0;
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;


            float sectorAngle = entityID.fovAngle; // tranche = FOV angle
            float halfAngle = sectorAngle / 2f; // Gauche Droite
            float randomAngle = Random.Range(-halfAngle, halfAngle); // Angle Random
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

            // transform.forward = randomDir;  // l'appeler dans une Coroutine, puis add la force
            rb.isKinematic = false;
            isRotating = true;
            // isRotating = false;
            isWaiting = true;
        }

        // COROUTINES FDP
        if (waitClock >= coolDown)
        {
            waitClock = 0;
            isRotating = false;
            rb.AddForce(transform.forward.normalized * entityID.nativeSpeed, ForceMode.Impulse);
            isWaiting = false;
        }
    }
}