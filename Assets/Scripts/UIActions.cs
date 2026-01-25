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
    
    private Transform fbx;
    private Material[] childMatList;
    private Material secondMat;
    private Color selfColor;

    private void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Ground");
        spawnByPlayer = ground.GetComponent<SpawnByPlayer>();
        collider = GetComponent<Collider>();
        cam = Camera.main;
        
        fbx = transform.GetChild(0);
        childMatList = new Material[fbx.childCount];
        for (int i = 0; i < fbx.childCount; i++)
        {
            Transform actualChild = fbx.transform.GetChild(i);
            childMatList[i] = actualChild.GetComponent<MeshRenderer>().materials[1];
        }
        selfColor = childMatList[0].GetColor("_Outline_Color");
        // Debug.Log(selfColor);
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
            // GetComponent<MeshRenderer>().material.color = Color.green;
            for (int i = 0; i < childMatList.Length; i++)
            {
                secondMat = childMatList[i];
                secondMat.SetColor("_Outline_Color", selfColor);
            }
            spawnByPlayer.choiceIndex = choiceIndex;
        }
        else
        {
            // GetComponent<MeshRenderer>().material.color = Color.white;
            for (int i = 0; i < childMatList.Length; i++)
            {
                secondMat = childMatList[i];
                secondMat.SetColor("_Outline_Color", Color.white);
            }
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
            for (int i = 0; i < childMatList.Length; i++)
            {
                secondMat = childMatList[i];
                secondMat.SetFloat("_Outline_Opacity", 1f);
            }
        }
        else
        {
            for (int i = 0; i < childMatList.Length; i++)
            {
                secondMat = childMatList[i];
                secondMat.SetFloat("_Outline_Opacity", 0f);
            }
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
