using System;
using FMODUnity;
using UnityEngine;

public class TimerBeforeDestroy : MonoBehaviour
{
    public StudioEventEmitter eventEmitter;
    public float timer;
    public int touchedEntities = 0;
    private bool alreadyTouched = false;

    void Start()
    {
        eventEmitter = GetComponent<StudioEventEmitter>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.4 && !alreadyTouched)
        {
            if (touchedEntities > 4)
            {
                touchedEntities = 4;
            }
            eventEmitter.SetParameter("AlcoholState", touchedEntities);
            alreadyTouched = true;
        }
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
