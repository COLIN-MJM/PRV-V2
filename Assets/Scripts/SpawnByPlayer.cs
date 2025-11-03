using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnByPlayer : MonoBehaviour, IPointerClickHandler
{
    public GameObject gm;
    public GameObject objectToSpawn;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM");
        objectToSpawn = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Instantiate(objectToSpawn, eventData.pointerPressRaycast.worldPosition, Quaternion.identity);
        }
    }
}
