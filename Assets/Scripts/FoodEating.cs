using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodEating : MonoBehaviour
{
    public int spawningNumber = 1;
    public int percentageSpawningVariance = 5;
    public GameObject[] spawnables;
    public GameObject eater = null;
    public GameObject spawningSpecies = null;

    private void Update()
    {
        if (eater)
        {
            for (int j = 0; j < spawningNumber; j++)
            {
                Species eaterSpecies = eater.GetComponent<EntityIdentity>().species;
                GameObject spawningSpecies = null;
                Vector3 rdPos = UnityEngine.Random.insideUnitCircle * 4f;
                int rdPercentage = UnityEngine.Random.Range(0, 100);
            
                if (rdPercentage < percentageSpawningVariance)
                {
                    List<GameObject> newSpawnables = new List<GameObject>();
                    foreach (GameObject species in spawnables)
                    {
                        if (eaterSpecies != species.GetComponent<EntityIdentity>().species)
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
                        if (eaterSpecies == species.GetComponent<EntityIdentity>().species)
                        {
                            spawningSpecies = species;
                        }
                    }
                }
                
                Instantiate(spawningSpecies, transform.position + rdPos, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
