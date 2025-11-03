using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityIdentity))]
public class FightAndKill : MonoBehaviour
{
    public EntityIdentity entityID;
    public EntityFOV entityFOV;
    public Collider[] fightingRangeEntities;

    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
        entityFOV = GetComponent<EntityFOV>();
        InvokeRepeating(nameof(FightCheck), 0f, 0.2f);
    }

    private void FightCheck()
    {
        fightingRangeEntities = Physics.OverlapSphere(transform.position, entityID.fightingRadius);
        
        foreach (var entity in fightingRangeEntities)
        {
            if (entity.CompareTag("Species"))
            {
                foreach (var prey in entityID.strengthAgainst)
                {
                    if (entity.GetComponent<EntityIdentity>().species == prey)
                    {
                        entityFOV.preysWithinFOV.Remove(entity.gameObject);
                        Destroy(entity.gameObject);
                    }
                }
            }
        }
    }
}
