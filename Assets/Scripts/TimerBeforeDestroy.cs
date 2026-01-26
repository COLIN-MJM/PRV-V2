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
        
        if (timer <= 0.7f)
        {
            collider.enabled = true;
        }
        
        if (timer <= 0.3f)
        {
            float toScale = ((timer / 0.3f)*4);
            transform.localScale = new Vector3(toScale, toScale, toScale);
        }
        
    }
}
