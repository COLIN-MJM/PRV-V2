using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EntityIdentity))]
public class EntityFOV : MonoBehaviour
{
    public EntityIdentity entityID;
    public List<GameObject> predatorsWithinFOV;
    public List<GameObject> preysWithinFOV;
    public List<GameObject> fightsWithinFOV;
    public List<GameObject> matesWithinFOV;
    public List<GameObject> foodWithinFOV;
    private Collider[] _nearbyObjects;

    private void Start()
    {
        entityID = gameObject.GetComponent<EntityIdentity>();
        InvokeRepeating(nameof(CheckingSurroundings), 0f, 0.2f); //Optimisation?
    }

    private void CheckingSurroundings()
    {
        predatorsWithinFOV = new List<GameObject>();
        preysWithinFOV = new List<GameObject>();
        fightsWithinFOV = new List<GameObject>();
        matesWithinFOV = new List<GameObject>();
        foodWithinFOV = new List<GameObject>();
        _nearbyObjects = Physics.OverlapSphere(transform.position, entityID.fovRadius);
        
        for (int i = 0; i < _nearbyObjects.Length; i++)
        {
            //Debug.Log(_nearbyObjects[i].name);
            if (_nearbyObjects[i].CompareTag("Species"))
            {
                foreach (var species in entityID.strengthAgainst)
                {
                    if (species == _nearbyObjects[i].gameObject.GetComponent<EntityIdentity>().species)
                    {
                        preysWithinFOV.Add(_nearbyObjects[i].gameObject);
                    }
                }
                
                foreach (var species in entityID.weaknessAgainst)
                {
                    if (species == _nearbyObjects[i].gameObject.GetComponent<EntityIdentity>().species)
                    {
                        predatorsWithinFOV.Add(_nearbyObjects[i].gameObject);
                        //Debug.Log(_nearbyObjects[i].name);
                    }
                }
                
                foreach (var species in entityID.fightingUpperHandAgainst)
                {
                    if (species == _nearbyObjects[i].gameObject.GetComponent<EntityIdentity>().species)
                    {
                        fightsWithinFOV.Add(_nearbyObjects[i].gameObject);
                    }
                }
                
                foreach (var species in entityID.fightingLowerHandAgainst)
                {
                    if (species == _nearbyObjects[i].gameObject.GetComponent<EntityIdentity>().species)
                    {
                        fightsWithinFOV.Add(_nearbyObjects[i].gameObject);
                    }
                }

                if (_nearbyObjects[i].gameObject != this.gameObject && _nearbyObjects[i].gameObject.GetComponent<EntityIdentity>().species == entityID.species)
                {
                    matesWithinFOV.Add(_nearbyObjects[i].gameObject);
                }
            }
            else if (_nearbyObjects[i].CompareTag("Food"))
            {
                foodWithinFOV.Add(_nearbyObjects[i].gameObject);
            }
        }
    }
}
