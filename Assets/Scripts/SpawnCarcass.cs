using System;
using UnityEngine;

public class SpawnCarcass : MonoBehaviour
{
    public EntityIdentity entityID;
    public GameObject carcass;
    
    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
    }

    private void Update()
    {
        // Debug.Log(transform.position);
    }


    public void SpawnIDCorpse()
    {
        GameObject currentCarcass = Instantiate(carcass, transform.position, transform.rotation);
        currentCarcass.GetComponent<CarcassState>().species = entityID.species;
    }
    
}
