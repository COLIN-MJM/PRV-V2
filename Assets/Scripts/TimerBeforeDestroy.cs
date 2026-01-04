using UnityEngine;

public class TimerBeforeDestroy : MonoBehaviour
{
    public float timer;
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
