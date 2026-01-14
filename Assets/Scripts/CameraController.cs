using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InputReader))]
public class CameraController : MonoBehaviour
{
    public Collider fakeGround;
    public Camera cam;
    public InputReader inputReader;
    public float cameraSpeed;
    public float zoomSensibility;
    public float closestZoom = 5f;
    public float furthestZoom = 15f;
    public float dragSensibility = 1.1f;
    public GameObject specificFocus = null;
    public Vector3 velocity = new Vector3(0,0,2);
    
    //Camera Drag
    private Vector3 previousPosition;
    private Vector3 nextPosition;
    private Vector3 delta;
    private bool initialized = false;

    private void Start()
    {
        inputReader = GetComponent<InputReader>();
        cam = GetComponentInChildren<Camera>();
        fakeGround = GetComponentInChildren<Collider>();
    }

    private void Update()
    {
        if (inputReader.MiddleClick != Vector3.zero)
        {
            if (initialized == false)
            {
                previousPosition = inputReader.MiddleClick;
                initialized = true;
            }
            else
            {
                nextPosition = inputReader.MiddleClick;
                delta = nextPosition - previousPosition;
                transform.Translate (new Vector3(-delta.x * dragSensibility, 0f, -delta.y * dragSensibility));
                previousPosition = nextPosition;
            }
        }
        else
        {
            initialized = false;
        }
        
        if (!specificFocus)
        {
            transform.Translate(new Vector3(cameraSpeed * Time.deltaTime * inputReader.HorizontalMove, 0f, cameraSpeed * Time.deltaTime * inputReader.VerticalMove));
        }
        else
        {
            // transform.position = specificFocus.transform.position;
            transform.position = Vector3.SmoothDamp(transform.position, specificFocus.transform.position, ref velocity, 0.5f );
        }
        
        // cam.orthographicSize += inputReader.Mousewheel * zoomSensibility;
        // cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, closestZoom, furthestZoom);
    }
}
