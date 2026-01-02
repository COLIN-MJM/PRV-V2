using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EntityIdentity))]
public class Interact : MonoBehaviour
{
    [Header("Entity Self Comps")]
    public EntityIdentity entityID;
    public EntityFOV entityFOV;
    
    [Header("Interacting Entities")]
    public Collider[] interactingRangeEntities;
    public GameObject child;
    public bool isInCollision;

    public SpawnCarcass spawnCorpse;
    public OnHungerFull hungerScript;

    private void Start()
    {
        hungerScript = gameObject.GetComponent<OnHungerFull>();
        entityID = GetComponent<EntityIdentity>();
        entityFOV = GetComponent<EntityFOV>();
        spawnCorpse = GetComponent<SpawnCarcass>();
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
                        spawnCorpse.SpawnIDCorpse();
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
                entity.GetComponent<FoodEating>().eater = this.gameObject;
                
                entityFOV.foodWithinFOV.Remove(entity.gameObject);
                
            }
            
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Carcass") && entityID.strengthAgainst.Contains(other.gameObject.GetComponent<CarcassState>().species))
        {
            other.gameObject.GetComponent<CarcassState>().carcassHealth -= Time.deltaTime;
            hungerScript.hungerBar += Time.deltaTime;
        }
    }
    
}
