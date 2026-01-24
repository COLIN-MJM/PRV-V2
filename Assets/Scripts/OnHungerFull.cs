using System;
using UnityEngine;

public class OnHungerFull : MonoBehaviour
{
    public float hungerBar;
    public float maxHungerBar;

    public GameObject ownSpecies;
    
    private EntityIdentity entityID;
    // private Animator animator;

    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
    }

    void Update()
    {
        if (hungerBar > maxHungerBar)
        {
            hungerBar = 0;
            
            Vector3 rdPos = UnityEngine.Random.insideUnitCircle;
            rdPos = new Vector3(rdPos.x * 4, 0, rdPos.z * 4);
            int rdPercentage = UnityEngine.Random.Range(0, 100);
            
            if (entityID.species == Species.S6)
            {
                ReloadShield();
            }
            
            Instantiate(ownSpecies, transform.position + rdPos, transform.rotation);
            
        }
    }

    void ReloadShield()
    {
        ShieldAndToxicGas shieldScript = GetComponent<ShieldAndToxicGas>();
        // animator = GetComponentInChildren<Animator>();
        
        shieldScript.isShieldUsable = true;
        shieldScript.isInvincibilityStillRunning = true;
        shieldScript.isGasZoneInstanciated = false;
        // animator.SetBool("IsCharging", true);
    }
}
