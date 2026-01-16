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
        if (other.gameObject.CompareTag("KillZone") && entityID.species != Species.S2)
        {
            entityCount.CountUpdate(entityID, -1);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("KillZone") && entityID.species == Species.S2 && GetComponent<MergeOnTrigger>().currentLevel == MergeOnTrigger.Level.LvlOne)
        {
            entityCount.CountUpdate(entityID, -1);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("KillZone") && entityID.species == Species.S2 && GetComponent<MergeOnTrigger>().currentLevel == MergeOnTrigger.Level.LvlTwo)
        {
            entityCount.CountUpdate(entityID, -2);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("KillZone") && entityID.species == Species.S2 && GetComponent<MergeOnTrigger>().currentLevel == MergeOnTrigger.Level.LvlThree)
        {
            entityCount.CountUpdate(entityID, -4);
            Destroy(gameObject);
        }
    }
}
