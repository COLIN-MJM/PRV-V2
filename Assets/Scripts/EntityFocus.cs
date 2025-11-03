using System;
using UnityEngine;

public class EntityFocus : MonoBehaviour
{
    public GameObject camFocus;
    private CameraController camController;
    private Light _light;

    private void Start()
    {
        _light = GetComponent<Light>();
        camFocus = GameObject.FindGameObjectWithTag("MainCamera");
        camController = camFocus.GetComponent<CameraController>();
    }

    private void Update()
    {
        if (camController.specificFocus == this.gameObject)
        {
            _light.enabled = true;
        }
        else
        {
            _light.enabled = false;
        }
    }
}
