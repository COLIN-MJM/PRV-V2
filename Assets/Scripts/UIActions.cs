using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIActions : MonoBehaviour
{
    public int choiceIndex;
    public GameObject ground;
    public MeshRenderer rotatingMark;
    public Collider collider;
    public Camera cam;
    public bool isSelected = false;
    private SpawnByPlayer spawnByPlayer;
    private bool isHighlighted = false;

    private void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Ground");
        spawnByPlayer = ground.GetComponent<SpawnByPlayer>();
        collider = GetComponent<Collider>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (VerifyClicking())
        {
            isSelected = true;
        }
        
        if (isSelected)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
            spawnByPlayer.choiceIndex = choiceIndex;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }

        if (VerifyHovering() || isSelected)
        {
            isHighlighted = true;
        }
        else
        {
            isHighlighted = false;
        }
        
        
        if (isHighlighted)
        {
            rotatingMark.enabled = true;
        }
        else
        {
            rotatingMark.enabled = false;
        }
    }
    
    private bool VerifyHovering()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            if (collider.bounds.Contains(new Vector3 (hitPoint.x, collider.bounds.max.y, hitPoint.z)))
            {
                return true;
            }
        }
        return false;
    }

    private bool VerifyClicking()
    {
        if (VerifyHovering() && Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }
}
