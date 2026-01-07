using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class MergeOnTrigger : MonoBehaviour
{

    public EntityIdentity entityID;
    public GameObject nextLevel;
    public GameObject previousLevel;
    

    public enum Level 
    {
        LvlOne,
        LvlTwo,
        LvlThree,
    }

    public Level currentLevel;
    public float tBeforeMerge = 2f;
    public float invincibilityT = 1f;

    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
    }

    private void Update()
    {

        if (tBeforeMerge > 0)
        {
            tBeforeMerge -= Time.deltaTime;
        }

        if (invincibilityT > 0)
        {
            invincibilityT -= Time.deltaTime;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Species") && other.gameObject.GetComponent<EntityIdentity>().species == entityID.species 
                                                   && transform.position.x < other.transform.position.x && other.gameObject.GetComponent<MergeOnTrigger>().currentLevel == currentLevel && currentLevel != Level.LvlThree && tBeforeMerge <= 0)
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Species") && other.gameObject.GetComponent<EntityIdentity>().species == entityID.species 
                                                        && transform.position.x > other.transform.position.x  && other.gameObject.GetComponent<MergeOnTrigger>().currentLevel == currentLevel && currentLevel != Level.LvlThree && tBeforeMerge <= 0)
        {
            Instantiate(nextLevel, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
