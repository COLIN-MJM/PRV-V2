using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(EntityFOV))]
public class StateChecker : MonoBehaviour
{
    public GameObject gm;
    public EntityCount entityCount;
    
    [Header("Entity Self Comps")]
    public EntityIdentity entityID;
    public EntityFOV entityFOV;
    public IfMatingSeason ifMatingSeason;
    public OnHungerFull onHungerFull;
    
    [Header("Entity Targets")]
    public List<GameObject> targetObjects;
    public Vector3 targetPos;

    [Header(("Entity Animation Params"))]
    // public Animator animator;
    
    private float t;
    public int addToCount = 1;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM");
        entityCount = gm.GetComponent<EntityCount>();
        entityID = GetComponent<EntityIdentity>();
        entityFOV = GetComponent<EntityFOV>();
        ifMatingSeason = GetComponent<IfMatingSeason>();
        onHungerFull = GetComponent<OnHungerFull>();

        // if (entityID.species == Species.S1)
        // {
        //     animator = GetComponentInChildren<Animator>();
        // }
        
        //Rajoute 1 au compteur d'entité total au spawn
        // entityCount.EntityCountMod(1);
        //
        // if (entityID.species == Species.S1)
        // {
        //     entityCount.VanilliCountMod(1);
        // }
        // else if (entityID.species == Species.S2)
        // {
        //     entityCount.EnXCountMod(1);
        // } 
        // else if (entityID.species == Species.S3)
        // {
        //     entityCount.HoloCountMod(1);
        // }
        // else if (entityID.species == Species.S6)
        // {
        //     entityCount.ToxiCountMod(1);
        // }
        
        entityCount.CountUpdate(entityID, addToCount);
        
        
        InvokeRepeating(nameof(StateChecking), 0f, 0.1f);
    }

    private void StateChecking()
    {
        if (entityID.state == State.Idle)
        {
            entityFOV.currentFOV = entityID.fovAngle;
            if (entityFOV.predatorsWithinFOV.Count > 0)
            {
                entityID.state = State.Fleeing;
                targetObjects = entityFOV.predatorsWithinFOV;
                
                // HERE
                // if (entityID.species == Species.S1)
                // {
                //     animator.SetBool("IsFleeing", true);
                // }
                
                // if (entityID.species == Species.S6)
                // {
                //     animator.SetBool("IsAttacking", true);
                // }
            }
            else if (entityFOV.preysWithinFOV.Count > 0)
            {
                entityID.state = State.Chasing;
                targetObjects = entityFOV.preysWithinFOV;
                
                // HERE
                // if (entityID.species == Species.S1)
                // {
                //     animator.SetBool("IsAttacking", true);
                // }
                
            }
            else if (entityFOV.fightsWithinFOV.Count > 0)
            {
                entityID.state = State.Fighting;
                targetObjects = entityFOV.fightsWithinFOV;
            }
            // else if (entityFOV.matesWithinFOV.Count > 0 && gm.GetComponent<SeasonManager>().currentSeason == entityID.matingSeason)
            // {
            //     entityID.state = State.Reproducing;
            //     targetObjects = entityFOV.matesWithinFOV;
            // }
            else if (entityFOV.scarecrowsWithinFOV.Count > 0)
            {
                entityID.state = State.Afraid;
                targetObjects = entityFOV.scarecrowsWithinFOV;
            }
        }

        if (entityID.state == State.Fleeing)
        {
            entityFOV.currentFOV = 360;
            t += 0.2f;
            
            if (t <= entityID.enduranceWhenFleeing && entityFOV.predatorsWithinFOV.Count > 0)
            {
                targetObjects = entityFOV.predatorsWithinFOV;
                Vector3 fleeingDirection = new Vector3(transform.forward.x, 0, transform.forward.z);
                foreach (GameObject predator in targetObjects)
                {
                    // if (predator != null)
                    // {
                    //     fleeingDirection += new Vector3(predator.transform.forward.x, 0, predator.transform.forward.z);
                    // }

                    if (predator != null && (predator.gameObject.transform.position - transform.position).magnitude <
                        fleeingDirection.magnitude)
                    {
                        fleeingDirection = transform.position - predator.gameObject.transform.position;
                    }
                }
                // fleeingDirection = -fleeingDirection;
                // targetPos = (fleeingDirection * 5f) + transform.position;
                targetPos = fleeingDirection.normalized * 5f;
                // Debug.Log(fleeingDirection);
            }
            else if (entityFOV.predatorsWithinFOV.Count == 0)
            {
                t = 0;
                targetPos = transform.position;
                entityID.state = State.Idle;
                
                // HERE
                // if (entityID.species == Species.S1)
                // {
                //     animator.SetBool("IsFleeing", false);
                // }
            }
            else
            {
                t = 0;
                targetPos = transform.position;
                entityID.state = State.Fatigued;
            }
        }

        if (entityID.state == State.Chasing)
        {
            entityFOV.currentFOV = entityID.fovAngle;
            t += 0.2f;

            if (t <= entityID.enduranceWhenChasing && entityFOV.preysWithinFOV.Count > 0)
            {
                targetObjects = entityFOV.preysWithinFOV;
                Vector3 targetToChase = new Vector3(1000f, 0f, 1000f);
                foreach (var prey in targetObjects)
                {
                    if (prey.gameObject != null && (prey.gameObject.transform.position - transform.position).magnitude < (targetToChase - transform.position).magnitude)
                    {
                        targetToChase = new Vector3(prey.gameObject.transform.position.x, transform.position.y, prey.gameObject.transform.position.z);
                    }
                }
                targetPos = targetToChase - transform.position;
            }
            else if (entityFOV.preysWithinFOV.Count == 0)
            {
                t = 0;
                targetPos = transform.position;
                entityID.state = State.Idle;
                
                // HERE
                // if (entityID.species == Species.S1)
                // {
                //     animator.SetBool("IsAttacking", false);
                // }
            }
            else
            {
                t = 0;
                targetPos = transform.position;
                entityID.state = State.Fatigued;
            }
        }
        
        if (entityID.state == State.Afraid)
        {
            entityFOV.currentFOV = entityID.fovAngle;
            t += 0.2f;

            if (entityFOV.scarecrowsWithinFOV.Count > 0)
            {
                targetObjects = entityFOV.scarecrowsWithinFOV;
                Vector3 targetToBeAfraidOf = new Vector3(1000f, 0f, 1000f);
                foreach (var scarecrow in targetObjects)
                {
                    if (scarecrow.gameObject != null && (scarecrow.gameObject.transform.position - transform.position).magnitude < (targetToBeAfraidOf - transform.position).magnitude)
                    {
                        targetToBeAfraidOf = new Vector3(scarecrow.gameObject.transform.position.x, transform.position.y, scarecrow.gameObject.transform.position.z);
                    }
                }
                targetPos = transform.position - targetToBeAfraidOf;
            }
            else if (entityFOV.scarecrowsWithinFOV.Count == 0)
            {
                t = 0;
                targetPos = transform.position;
                entityID.state = State.Idle;
            }
            else
            {
                t = 0;
                targetPos = transform.position;
                entityID.state = State.Fatigued;
            }
        }

        if (entityID.state == State.Reproducing)
        {
            //todo
        }

        if (entityID.state == State.Fighting)
        {
            //todo
        }

        if (entityID.state == State.Fatigued)
        {
            entityFOV.currentFOV = entityID.fovAngle;
            t += 0.2f;

            if (t > entityID.recoveryTime)
            {
                t = 0;
                entityID.state = State.Idle;
            }
        }

        if (entityID.state == State.Consuming)
        {
            t += 0.2f;

            if (t > onHungerFull.maxHungerBar)
            {
                t = 0;
                entityID.state = State.Idle;
            }
        }
    }
}
