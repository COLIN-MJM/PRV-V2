using System;
using UnityEngine;

public class CarcassState : MonoBehaviour
{
    public Species species;

    public float carcassHealth;

    private void Update()
    {
        if (carcassHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    
}
