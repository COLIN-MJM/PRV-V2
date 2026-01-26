using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class EncyclopedieSelecter : MonoBehaviour
{
    private Camera cam;
    private Collider collider;
    private StudioEventEmitter eventEmitter;
    [SerializeField] private GameObject book;
    public bool isOpened = false;
    private VCA masterVCA;
    
    void Start()
    {
        cam = Camera.main;
        collider = GetComponent<BoxCollider>();
        eventEmitter = GetComponent<StudioEventEmitter>();
        masterVCA = RuntimeManager.GetVCA("VCA:/Master");
    }

    // Update is called once per frame
    void Update()
    {
        eventEmitter.SetParameter("EncyclopedieSounds", 0);
        
        if (VerifyClicking())
        {
            book.SetActive(true); 
            isOpened = true;
            masterVCA.setVolume(0.2f);
            eventEmitter.SetParameter("EncyclopedieSounds", 1);
        }
    }
    
    private bool VerifyHovering()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            if (collider.bounds.Contains(new Vector3 (hitPoint.x, collider.bounds.max.y, hitPoint.z)) && !isOpened)
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
