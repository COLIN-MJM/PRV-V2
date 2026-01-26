using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private GlobalVolumeManager globalVolumeManager;
    [SerializeField] private Slider slider;
    private VCA masterVCA;

    private void Start()
    {
        slider = GetComponent<Slider>();
        masterVCA = RuntimeManager.GetVCA("VCA:/Master");
        float value;
        masterVCA.getVolume(out value);
        slider.value = value;
    }

    public void ChangeVolume(float value)
    {
        globalVolumeManager.volumeBeforeEncyclopedie = value;
    }
}
