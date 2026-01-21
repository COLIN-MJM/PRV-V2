using System;
using UnityEngine;

public class SpawnCarcass : MonoBehaviour
{
    public EntityIdentity entityID;
    public GameObject carcass;
    public GameObject gameManager;

    // public bool activated;
    // public MeshRenderer entityMesh;
    
    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
        gameManager = GameObject.FindGameObjectWithTag("GM");
        
    }

    private void Update()
    {
        // Debug.Log(transform.position);
    }


    public void SpawnIDCorpse(Species theSpecies)
    {
        if (gameManager.GetComponent<PlayerChoice>().carcassOn)
        {
            GameObject currentCarcass = Instantiate(carcass, transform.position, transform.rotation);
            // currentCarcass.GetComponent<CarcassState>().species = entityID.species;
            // currentCarcass.GetComponent<CarcassState>().species = theSpecies;
            CarcassState currentCarcassState = currentCarcass.GetComponent<CarcassState>();
            currentCarcassState.species = theSpecies;
            // currentCarcass.GetComponentInChildren<MeshRenderer>().materials = entityMesh.materials;
            
        }
    }
    
}
