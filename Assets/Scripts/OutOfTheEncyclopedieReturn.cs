using FMODUnity;
using UnityEngine;

public class OutOfTheEncyclopedieReturn : MonoBehaviour
{
    private Camera cam;
    private Collider collider;
    [SerializeField] private GameObject book;
    [SerializeField] private EncyclopedieSelecter encyclopedieSelecter;
    [SerializeField] private StudioEventEmitter eventEmitter;
    void Start()
    {
        cam = Camera.main;
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        eventEmitter.SetParameter("EncyclopedieSounds", 0);
        
        if (VerifyClickingOutside())
        {
            book.SetActive(false); 
            encyclopedieSelecter.isOpened = false;
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
            if (collider.bounds.Contains(new Vector3 (hitPoint.x, collider.bounds.max.y, hitPoint.z)))
            {
                return true;
            }
        }
        return false;
    }

    private bool VerifyClickingOutside()
    {
        if (!VerifyHovering() && Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }
}
