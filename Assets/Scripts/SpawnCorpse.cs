using System;
using UnityEngine;

public class SpawnCorpse : MonoBehaviour
{
    public EntityIdentity entityID;
    
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
        //Logique
    }
    
}
