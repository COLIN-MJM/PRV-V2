using System;
using FMODUnity;
using UnityEngine;

public class TimerBeforeDestroy : MonoBehaviour
{
    public StudioEventEmitter eventEmitter;
    public float timer;
    public int touchedEntities = 0;
    private int touchedEntitiesClamped = 0;
    private bool alreadyTouched = false;
    private Collider collider;

    private GameObject gm;
    private PlayerKillCount pkc;

    void Start()
    {
        collider = GetComponent<Collider>();
        gm = GameObject.FindGameObjectWithTag("GM");
        pkc = gm.GetComponent<PlayerKillCount>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            pkc.killCount += touchedEntities;
            Destroy(gameObject);
        }
        
        if (timer <= 0.4 && !alreadyTouched)
        {
            touchedEntitiesClamped = touchedEntities;
            
            if (touchedEntitiesClamped > 4)
            {
                touchedEntitiesClamped = 4;
            }
            eventEmitter.SetParameter("AlcoholState", touchedEntitiesClamped);
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
