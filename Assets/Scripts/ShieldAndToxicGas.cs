using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ShieldAndToxicGas : MonoBehaviour
{
    public float speedMultWhenQuickEscaping = 1.2f;
    public float timerMaxQuickEscaping = 2f;
    public GameObject toxicGas;
    // private Light light;
    public EntityIdentity entityID;

    
    public bool isShieldUsable = true;
    public bool isGasZoneInstanciated = false;
    public bool isInvincibilityStillRunning = true;
    private float nativeSpeed;
    private float t;
    

    private void Start()
    {
        entityID = GetComponent<EntityIdentity>();
        nativeSpeed = entityID.nativeSpeed;
        // light = GetComponent<Light>();
    }

    private void Update()
    {
        if (isInvincibilityStillRunning)
        {
            if (!isShieldUsable)
            {
                // light.enabled = false;
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
            // else
            // {
                // light.enabled = true;
            // }
        }
        
        
    }
}
