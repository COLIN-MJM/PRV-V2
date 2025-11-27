using System;
using UnityEngine;

public class AmbianceSound : MonoBehaviour
{
    private FMODUnity.StudioEventEmitter emitter;
    private SeasonManager seasonManager;
    private float seasonCount;
    private float velocity = 1f;

    private void Start()
    {
        seasonManager = GetComponent<SeasonManager>();
        seasonCount = seasonManager.seasonCount;
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    private void Update()
    {
        seasonCount = seasonManager.seasonCount;
        AmbianceSoundDamping();
        //float live;
        //emitter.EventInstance.getParameterByName("SeasonState", out live);
        //Debug.Log("LIVE: " + live + "    EDITOR: " + emitter.Params[0].Value);
    }

    private void AmbianceSoundDamping()
    {
        if (emitter.Params[0].Value >= 3.9f)
        {
            emitter.Params[0].Value = 0;
            emitter.SetParameter("SeasonState", emitter.Params[0].Value);
            return;
        }
        
        if (emitter.Params[0].Value >= 2.8f && emitter.Params[0].Value < 4 && seasonCount == 0)
        {
            emitter.Params[0].Value = Mathf.SmoothDamp(emitter.Params[0].Value, 4f, ref velocity, 1.5f);
            emitter.SetParameter("SeasonState", emitter.Params[0].Value);
            return;
        }
        
        if (emitter.Params[0].Value < 3)
        {
            emitter.Params[0].Value = Mathf.SmoothDamp(emitter.Params[0].Value, seasonCount, ref velocity, 1.5f);
            if (emitter.Params[0].Value > seasonCount)
            {
                emitter.Params[0].Value = seasonCount;
            }
            emitter.SetParameter("SeasonState", emitter.Params[0].Value);
        }
    }
}
