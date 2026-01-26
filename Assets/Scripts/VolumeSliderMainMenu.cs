using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderMainMenu : MonoBehaviour
{
    private VCA masterVCA;
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        masterVCA = RuntimeManager.GetVCA("VCA:/Master");
    }

    private void Update()
    {
        float value;
        masterVCA.getVolume(out value);
        volumeSlider.value = value;
    }

    public void ChangeVolume(float value)
    {
        masterVCA.setVolume(value);
    }
}
