using UnityEngine;

public class EnablingSoundPop : MonoBehaviour
{
    private int enabler = 0;
    void Start()
    {
        Invoke("Initialize", 0.2f);
    }

    private void Initialize()
    {
        var emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        Debug.Log(emitter.Params[0].Value + " avant");
        //emitter.SetParameter("SpawnSoundIsEnabled", enabler);
        emitter.Params[0].Value = enabler;
        emitter.SetParameter("SeasonState", emitter.Params[0].Value);
        Debug.Log(emitter.Params[0].Value);
    }
}
