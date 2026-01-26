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

        if (t <= 2)
        {
            float toScale = (t / 2);
            transform.localScale = new Vector3(toScale, toScale, toScale);
        }
        
    }
}
