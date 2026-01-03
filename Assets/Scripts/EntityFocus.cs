using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EntityFocus : MonoBehaviour, IPointerClickHandler
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
    
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(gameObject.name);
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (camFocus.GetComponent<CameraController>().specificFocus != this.gameObject)
            {
                camFocus.GetComponent<CameraController>().specificFocus = this.gameObject;
                GetComponent<Light>().enabled = true;
            }
            else
            {
                camFocus.GetComponent<CameraController>().specificFocus = null;
                GetComponent<Light>().enabled = false;
            }
        }
    }
}
