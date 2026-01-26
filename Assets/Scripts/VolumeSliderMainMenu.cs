using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class VolumeSliderMainMenu : MonoBehaviour
{
    private VCA masterVCA;

    private void Start()
    {
        masterVCA = RuntimeManager.GetVCA("VCA:/Master");
    }

    public void ChangeVolume(float value)
    {
        masterVCA.setVolume(value);
    }
}
