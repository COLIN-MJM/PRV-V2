using System;
using UnityEngine;

public class KillZoneEnter : MonoBehaviour
{
    public GameObject gm;
    public EntityCount entityCount;
    public EntityIdentity entityID;
    public TimerBeforeDestroy killZoneScript = null;


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
            killZoneScript = other.gameObject.GetComponent<TimerBeforeDestroy>();
            
            if (entityID.species != Species.S2)
            {
                entityCount.CountUpdate(entityID, -1);
                killZoneScript.touchedEntities++;
                Destroy(gameObject);
            }
            else if (entityID.species == Species.S2 && GetComponent<MergeOnTrigger>().currentLevel == MergeOnTrigger.Level.LvlOne)
            {
                entityCount.CountUpdate(entityID, -1);
                killZoneScript.touchedEntities++;
                Destroy(gameObject);
            }
            else if (entityID.species == Species.S2 && GetComponent<MergeOnTrigger>().currentLevel == MergeOnTrigger.Level.LvlTwo)
            {
                entityCount.CountUpdate(entityID, -2);
                killZoneScript.touchedEntities += 2;
                Destroy(gameObject);
            }
            else if (entityID.species == Species.S2 && GetComponent<MergeOnTrigger>().currentLevel == MergeOnTrigger.Level.LvlThree)
            {
                entityCount.CountUpdate(entityID, -4);
                killZoneScript.touchedEntities += 4;
                Destroy(gameObject);
            }
        }
    }
}
