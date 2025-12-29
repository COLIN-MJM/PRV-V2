using UnityEngine;

public class DespawnAfterT : MonoBehaviour
{
    public float t = 10f;

    void Update()
    {
        t -= Time.deltaTime;
        if (t <= 0)
        {
            Destroy(gameObject);
        }
    }
}
