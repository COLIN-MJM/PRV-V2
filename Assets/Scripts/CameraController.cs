using System;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class CameraController : MonoBehaviour
{
    public Camera cam;
    public InputReader inputReader;
    public float cameraSpeed;
    public float zoomSensibility;
    public float closestZoom = 5f;
    public float furthestZoom = 15f;
    public GameObject specificFocus = null;

    private void Start()
    {
        inputReader = GetComponent<InputReader>();
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        //Debug.Log(inputReader.Zoom);
        //Debug.Log(inputReader.VerticalMove);
        if (!specificFocus)
        {
            transform.Translate(new Vector3(cameraSpeed * Time.deltaTime * inputReader.HorizontalMove, 0f, cameraSpeed * Time.deltaTime * inputReader.VerticalMove));
        }
        else
        {
            transform.position = specificFocus.transform.position;
        }
        cam.orthographicSize += inputReader.Zoom * zoomSensibility;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, closestZoom, furthestZoom);
    }
}
