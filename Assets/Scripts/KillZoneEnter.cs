using System;
using UnityEngine;

public class KillZoneEnter : MonoBehaviour
{
    public GameObject gm;
    public EntityCount entityCount;
    public EntityIdentity entityID;


    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM");
        entityCount = gm.GetComponent<EntityCount>();
        entityID = GetComponent<EntityIdentity>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillZone"))
        {
            entityCount.CountUpdate(entityID, -1);
            Destroy(gameObject);
        }
    }
}
