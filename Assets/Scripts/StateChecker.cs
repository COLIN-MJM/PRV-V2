using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EntityFOV))]
public class StateChecker : MonoBehaviour
{
    public EntityIdentity entityID;
    public EntityFOV entityFOV;

    public IfMatingSeason ifMatingSeason;
    
    public GameObject gm;
    public List<GameObject> targetObjects;
    public Vector3 targetPos;
    private float t;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM");
        entityID = GetComponent<EntityIdentity>();
        entityFOV = GetComponent<EntityFOV>();
        ifMatingSeason = GetComponent<IfMatingSeason>();
        InvokeRepeating(nameof(StateChecking), 0f, 0.2f);
    }

    private void StateChecking()
    {
        if (entityID.state == State.Idle)
        {
            if (entityFOV.predatorsWithinFOV.Count > 0)
            {
                entityID.state = State.Fleeing;
                targetObjects = entityFOV.predatorsWithinFOV;
            }
            else if (entityFOV.preysWithinFOV.Count > 0)
            {
                entityID.state = State.Chasing;
                targetObjects = entityFOV.preysWithinFOV;
            }
            else if (entityFOV.fightsWithinFOV.Count > 0)
            {
                entityID.state = State.Fighting;
                targetObjects = entityFOV.fightsWithinFOV;
            }
            else if (entityFOV.matesWithinFOV.Count > 0 && gm.GetComponent<SeasonManager>().currentSeason == entityID.matingSeason)
            {
                entityID.state = State.Reproducing;
                targetObjects = entityFOV.matesWithinFOV;
            }
        }

        if (entityID.state == State.Fleeing)
        {
            t += 0.2f;
            
            if (t <= entityID.enduranceWhenFleeing &&  entityFOV.predatorsWithinFOV.Count > 0)
            {
                targetObjects = entityFOV.predatorsWithinFOV;
                Vector3 fleeingDirection = Vector3.zero;
                foreach (var predator in targetObjects)
                {
                    if (predator != null)
                    {
                        fleeingDirection += predator.transform.position;
                    }
                }
                fleeingDirection = -fleeingDirection.normalized;
                targetPos = fleeingDirection * 5f;
            }
            else if (entityFOV.predatorsWithinFOV.Count == 0)
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

        if (entityID.state == State.Chasing)
        {
            t += 0.2f;

            if (t <= entityID.enduranceWhenChasing && entityFOV.preysWithinFOV.Count > 0)
            {
                targetObjects = entityFOV.preysWithinFOV;
                Vector3 targetToChase = new Vector3(1000f, 0f, 1000f);
                foreach (var prey in targetObjects)
                {
                    if (prey.gameObject != null && (prey.gameObject.transform.position - transform.position).magnitude < (targetToChase - transform.position).magnitude)
                    {
                        targetToChase = prey.gameObject.transform.position;
                    }
                }
                targetPos = targetToChase;
            }
            else if (entityFOV.preysWithinFOV.Count == 0)
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
            t += 0.2f;

            if (t > entityID.recoveryTime)
            {
                t = 0;
                entityID.state = State.Idle;
            }
        }
    }
}
