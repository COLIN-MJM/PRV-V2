using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityIdentity))]
public class Interact : MonoBehaviour
{
    public EntityIdentity entityID;
    public EntityFOV entityFOV;
    public IfMating ifMating;
    public Collider[] interactingRangeEntities;
    public GameObject child;
    public bool isInCollision;

    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
        entityFOV = GetComponent<EntityFOV>();
        ifMating = GetComponent<IfMating>();
        InvokeRepeating(nameof(InteractCheck), 0f, 0.2f);
        isInCollision = false;
    }

    private void InteractCheck()
    {
        interactingRangeEntities = Physics.OverlapSphere(transform.position, entityID.interactingRadius);
        
        foreach (var entity in interactingRangeEntities)
        {
            if (entity.CompareTag("Species"))
            {
                foreach (var prey in entityID.strengthAgainst)
                {
                    if (entity.GetComponent<EntityIdentity>().species == prey)
                    {
                        entityFOV.preysWithinFOV.Remove(entity.gameObject);
                        Destroy(entity.gameObject);
                        entityID.numberOfAllowedChildren++;
                    }
                    else
                    {
                        isInCollision = true;
                    }
                }

                if (entityID.gender == Gender.Female &&
                    entityID.state == State.Reproducing &&
                    entity.GetComponent<EntityIdentity>().species == entityID.species &&
                    entity.GetComponent<EntityIdentity>().gender != entityID.gender &&
                    ifMating.actualMate == null &&
                    entity.GetComponent<EntityIdentity>().numberOfAllowedChildren > 0 &&
                    entityID.numberOfAllowedChildren > 0)
                {
                    entity.GetComponent<IfMating>().actualMate = this.gameObject;
                    ifMating.actualMate = entity.gameObject;
                    entity.GetComponent<EntityIdentity>().numberOfAllowedChildren--;
                    entityID.numberOfAllowedChildren--;
                    ifMating.hasPriority = true;
                }
            }
            else if (entity.CompareTag("Food"))
            {
                entity.GetComponent<FoodEating>().eater = this.gameObject;
                
                entityFOV.foodWithinFOV.Remove(entity.gameObject);
                
            }
        }
    }
}
