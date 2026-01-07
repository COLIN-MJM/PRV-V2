using System;
using UnityEngine;

public class OutlineGameZone : MonoBehaviour
{
    private Bounds bounds;

    private void Start()
    {
        bounds = GetComponent<MeshCollider>().bounds;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(new Vector3(bounds.min.x, 0, bounds.min.z), new Vector3(bounds.min.x, 0, bounds.max.z));
        Gizmos.DrawLine(new Vector3(bounds.min.x, 0, bounds.min.z), new Vector3(bounds.max.x, 0, bounds.min.z));
        Gizmos.DrawLine(new Vector3(bounds.max.x, 0, bounds.max.z), new Vector3(bounds.min.x, 0, bounds.max.z));
        Gizmos.DrawLine(new Vector3(bounds.max.x, 0, bounds.max.z), new Vector3(bounds.max.x, 0, bounds.min.z));
    }
}
