using UnityEngine;

public class BeforeKillZone : MonoBehaviour
{
    public float time;
    public GameObject killZone;
    
    void Start()
    {
        
    }

    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            Instantiate(killZone, transform.position, transform.rotation);
            
            Destroy(gameObject);
        }
    }
}
