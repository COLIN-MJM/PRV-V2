using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class MergeOnTrigger : MonoBehaviour
{

    public EntityIdentity entityID;
    public GameObject nextLevel;
    

    public enum Level 
    {
        LvlOne,
        LvlTwo,
        LvlThree,
    }

    public Level currentLevel;

    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Species") && other.gameObject.GetComponent<EntityIdentity>().species == entityID.species 
                                                   && transform.position.x < other.transform.position.x && other.gameObject.GetComponent<MergeOnTrigger>().currentLevel == currentLevel && currentLevel != Level.LvlThree)
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Species") && other.gameObject.GetComponent<EntityIdentity>().species == entityID.species 
                                                        && transform.position.x > other.transform.position.x  && other.gameObject.GetComponent<MergeOnTrigger>().currentLevel == currentLevel && currentLevel != Level.LvlThree)
        {
            Instantiate(nextLevel, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
