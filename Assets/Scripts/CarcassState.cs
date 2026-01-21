using System;
using System.Collections.Generic;
using UnityEngine;

public class CarcassState : MonoBehaviour
{
    public Species species;

    public float carcassHealth;
    public float maxCarcassHealth;
    public float maxHealthModifierS3;
    public List<GameObject> eater;

    public GameObject vanilliiBody;
    public GameObject entityXBody;
    public GameObject holoBody;
    public GameObject toxiBody;


    private void Start()
    {
        carcassHealth = maxCarcassHealth;
        eater = new List<GameObject>();
        if (species == Species.S3)
        {
            carcassHealth = maxCarcassHealth * maxHealthModifierS3;
        }

        if (species == Species.S1)
        {
            vanilliiBody.SetActive(true);
        }
        else if (species == Species.S2)
        {
            entityXBody.SetActive(true);
        }
        else if (species == Species.S3)
        {
            holoBody.SetActive(true);
        }
        else if (species == Species.S6)
        {
            toxiBody.SetActive(true);
        }
        
        
    }


    private void Update()
    {
        float toScale = 0.2f + (carcassHealth / maxCarcassHealth);
        transform.localScale = new Vector3(toScale, toScale, toScale);

        // Debug.Log("toScale = " + toScale);
        
        if (carcassHealth <= 0)
        {
            if (eater.Count > 0)
            {
                foreach (GameObject obj in eater)
                {
                    if (obj != null)
                    {
                        obj.GetComponent<EntityIdentity>().state = State.Idle;
                    }
                }
            }
            Destroy(gameObject);
        }
        
        
    }
}
