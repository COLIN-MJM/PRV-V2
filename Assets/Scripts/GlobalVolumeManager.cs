using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class GlobalVolumeManager : MonoBehaviour
{
    [SerializeField] private EncyclopedieSelecter encyclopedieSelecter;
    private float originalVolume;
    private VCA masterVCA;
    public float volumeBeforeEncyclopedie;
    public float actualVolume;
   
    private void Start()
    {
        masterVCA = RuntimeManager.GetVCA("VCA:/Master");
        masterVCA.getVolume(out originalVolume);
        volumeBeforeEncyclopedie = originalVolume;
        actualVolume = originalVolume;
    }

    private void Update()
    {
        if (encyclopedieSelecter.isOpened)
        {
            actualVolume = volumeBeforeEncyclopedie / 5;
        }
        else
        {
            actualVolume = volumeBeforeEncyclopedie;
        }
        masterVCA.setVolume(actualVolume);
    }
}
