using System;
using UnityEngine;

public class SeasonManager : MonoBehaviour
{
    public Season currentSeason;
    public float timer = 5f;
    public int seasonCount;
    public float maxTimer = 5f;


    private void Start()
    {
        timer = maxTimer;
    }


    private void Update()
    {
        timer -= Time.deltaTime;
    
        if (seasonCount > 3)
        {
            seasonCount = 0;
        }
    
        if (timer <= 0 && seasonCount < 3)
        {
            timer = maxTimer;
            seasonCount++;
            currentSeason++;
            Debug.Log("current season is : " + currentSeason);
        }
        else if (timer <= 0 && seasonCount >= 3)
        {
            timer = maxTimer;
            seasonCount = 0;
            currentSeason = Season.Spring;
            Debug.Log("current season is : " + currentSeason);
        }
    
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentSeason = Season.Spring;
            timer = maxTimer;
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentSeason = Season.Summer;
            timer = maxTimer;
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentSeason = Season.Autumn;
            timer = maxTimer;
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentSeason = Season.Winter;
            timer = maxTimer;
        }
    }

    
    
}
