using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EntitiesWhenClicking : MonoBehaviour, IPointerClickHandler
{
    public GameObject camFocus;

    private void Start()
    {
        camFocus = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(gameObject.name);
        if (eventData.button == PointerEventData.InputButton.Left)
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
