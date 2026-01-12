using System;
using UnityEngine;

public class EggsBehavior : MonoBehaviour
{
    public GameObject[] eggs;
    public GameObject child;
    public float timeBeforeHatching;
    private int step = 0;
    private float t;

    private void Update()
    {
        t += Time.deltaTime;

        if (t >= timeBeforeHatching)
        {
            if (step < eggs.Length)
            {
                Instantiate(child, eggs[step].transform.position, Quaternion.identity);
                Destroy(eggs[step]);
                t = 0;
                step++;
            }
            else
            {
                Instantiate(child, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
