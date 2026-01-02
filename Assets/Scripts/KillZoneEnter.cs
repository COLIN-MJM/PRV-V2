using System;
using UnityEngine;

public class KillZoneEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillZone"))
        {
            Destroy(gameObject);
        }
    }
}
