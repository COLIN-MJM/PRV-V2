using System;
using UnityEngine;

public class SpawnCorpse : MonoBehaviour
{
    public EntityIdentity entityID;
    
    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
    }


    public void SpawnIDCorpse()
    {
        //Logique
    }
    
}
