using UnityEngine;

public class SteeringBehavior : MonoBehaviour
{
    public float speed = 5f;
    public float detectionRange = 5f;
    public float avoidanceRotationSpeed = 120f;

    private Vector3 targetDirection;
    private float rdAngle;
    private int leftOrRight = 0;


    private void Start()
    {
        targetDirection = transform.forward;
        InvokeRepeating(nameof(ChangeDirection), 0f, 0.5f);
    }
    
    void ChangeDirection()
    {
        leftOrRight = Random.Range(-1, 2);
        rdAngle = leftOrRight * Random.value * 30f;
        targetDirection = Quaternion.AngleAxis(rdAngle, Vector3.up) * targetDirection;
    }
    
    
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * detectionRange, Color.blue);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, detectionRange))
        {
            targetDirection = (transform.forward + hit.normal).normalized;
        }
        
        targetDirection = Quaternion.AngleAxis(rdAngle * Time.deltaTime, Vector3.up) * targetDirection;
        
        Vector3 currentDir = Vector3.RotateTowards(transform.forward, targetDirection,
            Mathf.Deg2Rad * avoidanceRotationSpeed * Time.deltaTime, 1f);
        transform.forward = currentDir;
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    void Repeat()
    {
        
    }
}
