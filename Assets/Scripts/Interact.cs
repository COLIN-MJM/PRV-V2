using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityIdentity))]
public class Interact : MonoBehaviour
{
    public EntityIdentity entityID;
    public EntityFOV entityFOV;
    public Collider[] fightingRangeEntities;
    public GameObject child;
    public bool isInCollision;

    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
        entityFOV = GetComponent<EntityFOV>();
        InvokeRepeating(nameof(InteractCheck), 0f, 0.2f);
        isInCollision = false;
    }

    private void InteractCheck()
    {
        fightingRangeEntities = Physics.OverlapSphere(transform.position, entityID.interactingRadius);
        
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
                    else
                    {
                        isInCollision = true;
                    }
                }
            }
            else if (entity.CompareTag("Food"))
            {
                Vector3 rdPos = UnityEngine.Random.insideUnitCircle * 4f;
                
                entityFOV.foodWithinFOV.Remove(entity.gameObject);
                Instantiate(child, entity.transform.position + rdPos, Quaternion.identity);
                Destroy(entity.gameObject);
            }
        }
    }
}
