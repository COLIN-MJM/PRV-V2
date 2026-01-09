using System;
using UnityEngine;

public class ForceClamping : MonoBehaviour
{
    public GameObject ground;
    public Bounds groundBounds;

    private void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Ground");
        groundBounds = ground.GetComponent<Collider>().bounds;
    }

    private void Update()
    {
        Vector3 currentPos = transform.position;
        currentPos.y = ground.transform.position.y;

        if (!groundBounds.Contains(currentPos))
        {
            if (currentPos.x > groundBounds.max.x)
            {
                currentPos.x = groundBounds.max.x;
            }
            else if (currentPos.x < groundBounds.min.x)
            {
                currentPos.x = groundBounds.min.x;
            }
            
            if (currentPos.z > groundBounds.max.z)
            {
                currentPos.z = groundBounds.max.z;
            }
            else if (currentPos.z < groundBounds.min.z)
            {
                currentPos.z = groundBounds.min.z;
            }
            
            transform.position = currentPos;
        }
    }
}
