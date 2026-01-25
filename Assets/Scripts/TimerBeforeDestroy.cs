using System;
using FMODUnity;
using UnityEngine;

public class TimerBeforeDestroy : MonoBehaviour
{
    public StudioEventEmitter eventEmitter;
    public float timer;
    public int touchedEntities = 0;
    private bool alreadyTouched = false;
    private Collider collider;

    void Start()
    {
        eventEmitter = GetComponent<StudioEventEmitter>();
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
        
        if (timer <= 0.4 && !alreadyTouched)
        {
            if (touchedEntities > 4)
            {
                touchedEntities = 4;
            }
            eventEmitter.SetParameter("AlcoholState", touchedEntities);
            alreadyTouched = true;
        }
        
        if (timer <= 0.5f)
        {
            collider.enabled = true;
            float toScale = 0.1f + (timer / 0.5f);
            transform.localScale = new Vector3(toScale, toScale, toScale);
            
        }
    }
}
