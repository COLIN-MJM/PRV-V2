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

    public GameObject gm;
    public EntityCount entityCount;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM");
        entityCount = gm.GetComponent<EntityCount>();
        hungerScript = gameObject.GetComponent<OnHungerFull>();
        entityID = GetComponent<EntityIdentity>();
        entityFOV = GetComponent<EntityFOV>();
        spawnCorpse = GetComponent<SpawnCarcass>();
        InvokeRepeating(nameof(InteractCheck), 0f, 0.2f);
    }

    private void InteractCheck()
    {
        interactingRangeEntities = Physics.OverlapSphere(transform.position, entityID.interactingRadius);
        
        foreach (var entity in interactingRangeEntities)
        {
            if (entity.CompareTag("Species") && entity.gameObject != this.gameObject)
            {
                foreach (var prey in entityID.strengthAgainst)
                {
                    if (entity.GetComponent<EntityIdentity>().species == prey)
                    {
                        switch (entity.GetComponent<EntityIdentity>().species)
                        {
                            case Species.S1:
                                KillAndSpawnCarcass(entity);
                                break;
                            case Species.S2:
                                SplitOrKill(entity);
                                break;
                            case Species.S3:
                                KillAndSpawnCarcass(entity);
                                break;
                            case Species.S4:
                                KillAndSpawnCarcass(entity);
                                break;
                            case Species.S5:
                                KillAndSpawnCarcass(entity);
                                break;
                            case Species.S6:
                                BreakShieldOrKill(entity);
                                break;
                        }
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

    private void BreakShieldOrKill(Collider entity)
    {
        if (entity.GetComponent<ShieldAndToxicGas>().isInvincibilityStillRunning)
        {
            entity.GetComponent<ShieldAndToxicGas>().isShieldActivated = false;
        }
        else
        {
            KillAndSpawnCarcass(entity);
        }
    }

    private void SplitOrKill(Collider entity)
    {
        if (entity.GetComponent<MergeOnTrigger>().currentLevel == MergeOnTrigger.Level.LvlThree)
        {
            entityCount.CountUpdate(entity.GetComponent<EntityIdentity>(), -1);
            SplitInFour(entity);
        }
        else if (entity.GetComponent<MergeOnTrigger>().currentLevel == MergeOnTrigger.Level.LvlTwo)
        {
            entityCount.CountUpdate(entity.GetComponent<EntityIdentity>(), -1);
            SplitInTwo(entity);
        }
        else if (entity.GetComponent<MergeOnTrigger>().currentLevel == MergeOnTrigger.Level.LvlOne && entity.GetComponent<MergeOnTrigger>().invincibilityT <= 0)
        {
            KillAndSpawnCarcass(entity);
        }
    }

    private static void SplitInTwo(Collider entity)
    {
        GameObject toSpawn = entity.GetComponent<MergeOnTrigger>().previousLevel;
        Vector3 rdPos = UnityEngine.Random.insideUnitCircle;
        rdPos = new Vector3(rdPos.x * 4, 0, rdPos.z * 4);
                                    
        Instantiate(toSpawn, entity.transform.position + rdPos, Quaternion.identity);
        Instantiate(toSpawn, entity.transform.position - rdPos, Quaternion.identity);
        Destroy(entity.gameObject);
    }

    private static void SplitInFour(Collider entity)
    {
        GameObject toSpawn = entity.GetComponent<MergeOnTrigger>().previousLevel;
        Vector3 rdPos = UnityEngine.Random.insideUnitCircle;
        rdPos = new Vector3(rdPos.x * 4, 0, rdPos.z * 4).normalized * 4;
                                    
        Instantiate(toSpawn, entity.transform.position + rdPos, Quaternion.identity);
        Instantiate(toSpawn, entity.transform.position - rdPos, Quaternion.identity);
        rdPos = new Vector3(rdPos.z, 0, rdPos.x).normalized * 4;
        Instantiate(toSpawn, entity.transform.position + rdPos, Quaternion.identity);
        Instantiate(toSpawn, entity.transform.position - rdPos, Quaternion.identity);
        Destroy(entity.gameObject);
    }

    private void KillAndSpawnCarcass(Collider entity)
    {
        entityFOV.preysWithinFOV.Remove(entity.gameObject);
        spawnCorpse.SpawnIDCorpse(entity.GetComponent<MeshRenderer>(), entity.GetComponent<EntityIdentity>().species);
        EntityIdentity thisEntitySpecies = entity.GetComponent<EntityIdentity>();
        
        // entityCount.EntityCountMod(-1);
        // if (thisEntitySpecies.species == Species.S1)
        // {
        //     entityCount.VanilliCountMod(-1);
        // }
        // else if (thisEntitySpecies.species == Species.S2)
        // {
        //     entityCount.EnXCountMod(-1);
        // } 
        // else if (thisEntitySpecies.species == Species.S3)
        // {
        //     entityCount.HoloCountMod(-1);
        // }
        // else if (thisEntitySpecies.species == Species.S6)
        // {
        //     entityCount.ToxiCountMod(-1);
        // }
        
        entityCount.CountUpdate(thisEntitySpecies, -1);
        
        Destroy(entity.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Carcass") && 
            (entityID.strengthAgainst.Contains(other.gameObject.GetComponent<CarcassState>().species) ||
            (entityID.species == Species.S3 && other.GetComponent<CarcassState>().species != Species.S3)))
        {
            entityID.state = State.Consuming;
            CarcassState consumedCarcass = other.gameObject.GetComponent<CarcassState>();
            consumedCarcass.eater.Add(this.gameObject);
            consumedCarcass.carcassHealth -= Time.deltaTime;
            hungerScript.hungerBar += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Carcass") && 
            (entityID.strengthAgainst.Contains(other.gameObject.GetComponent<CarcassState>().species) || 
             (entityID.species == Species.S3 && other.GetComponent<CarcassState>().species != Species.S3)))
        {
            entityID.state = State.Idle;
        }
    }
}
