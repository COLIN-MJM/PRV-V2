using UnityEngine;

public class RotationAnneau : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 50);
    }
}
