using System;
using UnityEngine;

public class CarcassState : MonoBehaviour
{
    public Species species;

    public float carcassHealth;
    public float maxCarcassHealth;


    private void Start()
    {
        carcassHealth = maxCarcassHealth;
    }


    private void Update()
    {
        Debug.Log(carcassHealth);
        
        float toScale = 0.2f + (carcassHealth / maxCarcassHealth);
        transform.localScale = new Vector3(toScale, toScale, toScale);

        Debug.Log("toScale = " + toScale);
        
        if (carcassHealth <= 0)
        {
            Destroy(gameObject);
        }

        
    }

    
}
