using System;
using FMODUnity;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    private StudioEventEmitter eventEmitter;

    void Start()
    {
        eventEmitter = GetComponent<StudioEventEmitter>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            eventEmitter.Play();
        }
    }
}
