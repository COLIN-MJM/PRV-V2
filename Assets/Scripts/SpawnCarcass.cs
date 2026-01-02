using System;
using UnityEngine;

public class SpawnCarcass : MonoBehaviour
{
    public EntityIdentity entityID;
    public GameObject carcass;

    public bool activated;
    // public MeshRenderer entityMesh;
    
    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
    }

    private void Update()
    {
        // Debug.Log(transform.position);
    }


    public void SpawnIDCorpse(MeshRenderer entityMesh)
    {
        if (activated)
        {
            GameObject currentCarcass = Instantiate(carcass, transform.position, transform.rotation);
            currentCarcass.GetComponent<CarcassState>().species = entityID.species;
            currentCarcass.GetComponentInChildren<MeshRenderer>().materials = entityMesh.materials;
        }
    }
    
}
