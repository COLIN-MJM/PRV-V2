using System;
using UnityEngine;

public class IfMatingSeason : MonoBehaviour
{
    private EntityIdentity entityID;
    public Season currentSeason;
    public GameObject gm;

    private void Start()
    {
        // currentSeason = gm.GetComponent<SeasonManager>().currentSeason;
    }

    private void Update()
    {
        // Debug.Log(currentSeason);
    }
}
