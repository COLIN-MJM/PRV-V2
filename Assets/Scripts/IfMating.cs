using System.Collections.Generic;
using UnityEngine;


public class IfMating : MonoBehaviour
{
    private EntityIdentity entityID;
    private StateChecker stateChecker;
    public GameObject[] spawnables;
    public GameObject actualMate;
    public bool hasPriority = false;
    private GameObject spawningSpecies = null;

    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
        entityID.gender = (Gender)Random.Range(0, 2);
        stateChecker = GetComponent<StateChecker>();
    }

    private void Update()
    {
        if (hasPriority)
        {
            Spawn(actualMate, spawnables, entityID.spawningNumber, entityID.percentageSpawningVariance);
            actualMate.GetComponent<IfMating>().actualMate = null;
            actualMate.GetComponent<EntityIdentity>().state = State.Fatigued;
            actualMate.GetComponent<StateChecker>().timeToStayFatigued = entityID.reproductionCooldown;
            actualMate = null;
            entityID.state = State.Fatigued;
            stateChecker.timeToStayFatigued = entityID.reproductionCooldown;
            hasPriority = false;
        }
    }
    
    public void Spawn(GameObject spawner, GameObject[] spawnables, int spawningNumber, int percentageSpawningVariance)
    {
        for (int j = 0; j < spawningNumber; j++)
        {
            Species spawnerSpecies = spawner.GetComponent<EntityIdentity>().species;
            GameObject spawningSpecies = null;
            Vector3 rdPos = UnityEngine.Random.insideUnitCircle * 4f;
            int rdPercentage = UnityEngine.Random.Range(0, 100);
            
            if (rdPercentage < percentageSpawningVariance)
            {
                List<GameObject> newSpawnables = new List<GameObject>();
                foreach (GameObject species in spawnables)
                {
                    if (spawnerSpecies != species.GetComponent<EntityIdentity>().species)
                    {
                        newSpawnables.Add(species);
                    }
                }
                
                int rdSpecies = UnityEngine.Random.Range(0, newSpawnables.Count);
                spawningSpecies = newSpawnables[rdSpecies];
            }
            else
            {
                foreach (GameObject species in spawnables)
                {
                    if (spawnerSpecies == species.GetComponent<EntityIdentity>().species)
                    {
                        spawningSpecies = species;
                    }
                }
            }
                
            Instantiate(spawningSpecies, transform.position + rdPos, Quaternion.identity);
        }
    }
}
