using System;
using UnityEngine;

public class ShieldAndToxicGas : MonoBehaviour
{
    public float speedMultWhenQuickEscaping = 1.2f;
    public float timerMaxQuickEscaping = 2f;
    public GameObject toxicGas;
    public EntityIdentity entityID;
    
    public bool isShieldActivated = true;
    public bool isGasZoneInstanciated = false;
    public bool isInvincibilityStillRunning = true;
    private float nativeSpeed;
    private float t;
    

    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
        nativeSpeed = entityID.nativeSpeed;
    }

    private void Update()
    {
        if (isInvincibilityStillRunning)
        {
            if (isShieldActivated == false)
            {
                GetComponent<Light>().enabled = false;
                if (!isGasZoneInstanciated)
                {
                    Instantiate(toxicGas, transform.position, Quaternion.identity);
                    isGasZoneInstanciated = true;
                }
            
                entityID.nativeSpeed = nativeSpeed * speedMultWhenQuickEscaping;
                t += Time.deltaTime;

                if (t > timerMaxQuickEscaping)
                { 
                    isInvincibilityStillRunning = false;
                    entityID.nativeSpeed = nativeSpeed;
                    t = 0;
                }
            }
            else
            {
                GetComponent<Light>().enabled = true;
            }
        }
        
        
    }
}
