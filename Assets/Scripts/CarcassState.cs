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


    private void Start()
    {
        carcassHealth = maxCarcassHealth;
        eater = new List<GameObject>();
        if (species == Species.S3)
        {
            carcassHealth = maxCarcassHealth * maxHealthModifierS3;
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
