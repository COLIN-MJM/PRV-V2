using System;
using UnityEngine;

public class GasZoneBehavior : MonoBehaviour
{
    public float slowDownMultiplier = 0.8f;
    public float gasScale = 2f;
    public float timerMax = 2f;
    private float t;

    private void Start()
    {
        transform.localScale = new Vector3(gasScale, transform.localScale.y, gasScale);
    }

    private void Update()
    {
        t += Time.deltaTime;

        if (t > timerMax)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Species") &&
            other.gameObject.GetComponent<EntityIdentity>().species != Species.S6)
        {
            other.gameObject.GetComponent<EntityIdentity>().nativeSpeed *= slowDownMultiplier;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Species") &&
            other.gameObject.GetComponent<EntityIdentity>().species != Species.S6)
        {
            other.gameObject.GetComponent<EntityIdentity>().nativeSpeed /= slowDownMultiplier;
        }
    }
}
