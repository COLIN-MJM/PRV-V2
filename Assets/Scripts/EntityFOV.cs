using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EntityIdentity))]
public class EntityFOV : MonoBehaviour
{
    // private Rigidbody rb;
    public EntityIdentity entityID;
    public List<GameObject> predatorsWithinFOV;
    public List<GameObject> preysWithinFOV;
    public List<GameObject> fightsWithinFOV;
    public List<GameObject> matesWithinFOV;
    public List<GameObject> foodWithinFOV;
    public List<Vector3> wallsWithinFOV;
    private Collider selfCollider;
    private Collider[] _nearbyObjects;
    public float currentFOV;

    private void Start()
    {
        // rb = GetComponent<Rigidbody>();
        entityID = gameObject.GetComponent<EntityIdentity>();
        selfCollider = GetComponent<Collider>();
        InvokeRepeating(nameof(CheckingSurroundings), 0f, 0.2f); //Optimisation?
        wallsWithinFOV = new List<Vector3>();
        currentFOV = entityID.fovAngle;
    }

    private void CheckingSurroundings()
    {
        predatorsWithinFOV = new List<GameObject>();
        preysWithinFOV = new List<GameObject>();
        fightsWithinFOV = new List<GameObject>();
        matesWithinFOV = new List<GameObject>();
        foodWithinFOV = new List<GameObject>();
        wallsWithinFOV.Clear();
        
        
        
        _nearbyObjects = Physics.OverlapSphere(transform.position, entityID.fovRadius);
        
        for (int i = 0; i < _nearbyObjects.Length; i++)
        {
            float signedAngle = Vector3.Angle(transform.forward, _nearbyObjects[i].transform.position - transform.position);
            
            if (_nearbyObjects[i].CompareTag("Carcass"))
            {
                if (entityID.species == Species.S3)
                {
                    preysWithinFOV.Add(_nearbyObjects[i].gameObject);
                }
                else
                {
                    foreach (var species in entityID.strengthAgainst)
                    {
                        if (species == _nearbyObjects[i].gameObject.GetComponent<CarcassState>().species)
                        {
                            preysWithinFOV.Add(_nearbyObjects[i].gameObject);
                        }
                        // Debug.Log(_nearbyObjects[i].gameObject.GetComponentInParent<CarcassState>().species);
                        // preysWithinFOV.Add(_nearbyObjects[i].gameObject);
                        //
                    }
                }
            }
            else if (Mathf.Abs(signedAngle) < currentFOV / 2f && _nearbyObjects[i] != selfCollider)
            {
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
                            // Debug.Log(_nearbyObjects[i].name);
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

                    if (_nearbyObjects[i].gameObject != this.gameObject &&
                        _nearbyObjects[i].gameObject.GetComponent<EntityIdentity>().species == entityID.species)
                    {
                        matesWithinFOV.Add(_nearbyObjects[i].gameObject);
                    }
                }
                else if (_nearbyObjects[i].CompareTag("Food"))
                {
                    preysWithinFOV.Add(_nearbyObjects[i].gameObject);
                }
                else if (_nearbyObjects[i].CompareTag("Scarecrow"))
                {
                    predatorsWithinFOV.Add(_nearbyObjects[i].gameObject);
                }
            }
            // else if (Mathf.Abs(signedAngle) < entityID.fovAngle / 5f && _nearbyObjects[i] != selfCollider)
            // {
            //     if (_nearbyObjects[i].CompareTag("Wall"))
            //     {
            //         if (Physics.Raycast(transform.position, _nearbyObjects[i].gameObject.transform.position, out RaycastHit hit, 1000f))
            //         {
            //             wallsWithinFOV.Add(hit.normal);
            //         }
            //     }
            // }
        }
    }

    // private Vector3 WallRaycast()
    // {
    //     
    //
    //     else return Vector3.zero;
    // }
}
